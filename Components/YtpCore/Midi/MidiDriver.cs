using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MidiParser;

namespace MyFirstAvaloniaApp.Components.YtpCore.Midi;

public class MidiDriver
{
    private int _bpm;
    private int _ticksPerQuarterNote;
    private Stopwatch _stopwatch = new();
    private string _path = "Assets/midi/lead.mid";
    
    // Add this delegate definition
    public delegate void NoteOnEventHandler(double timeMs, int note, int velocity);
    
    // Add this event or Action property
    public event NoteOnEventHandler? OnNoteOn;
    // Alternative using Action: public Action<double, int, int>? OnNoteOn;

    public double Latency;

    public MidiDriver(double latency = 0)
    {
        Latency = latency;
    }

    public void Load(string path)
    {
        _path = path;
        Console.WriteLine("Initializing: {0}\n", _path);

        var midiFile = new MidiFile(_path);

        Console.WriteLine("Format: {0}", midiFile.Format);
        Console.WriteLine("TicksPerQuarterNote: {0}", midiFile.TicksPerQuarterNote);
        _ticksPerQuarterNote = midiFile.TicksPerQuarterNote;
        Console.WriteLine("TracksCount: {0}", midiFile.TracksCount);

        foreach (var track in midiFile.Tracks)
        {
            foreach (var midiEvent in track.MidiEvents)
            {
                if (midiEvent.MidiEventType != MidiEventType.MetaEvent) continue;
                if (midiEvent.MetaEventType != MetaEventType.Tempo || _bpm != 0) continue;
                Console.WriteLine("BPM: "+ midiEvent.Arg2);
                _bpm = midiEvent.Arg2;
            }
        }

        Console.WriteLine("Midi Initialization End.");
    }

    public async Task Play(CancellationToken cancellationToken = default)
    {
        _stopwatch = new Stopwatch();
        var midiFile = new MidiFile(_path);
        
        // Calculate microseconds per tick
        double microsecondsPerQuarterNote = (60.0 * 1_000_000) / _bpm;
        double microsecondsPerTick = microsecondsPerQuarterNote / _ticksPerQuarterNote;
        
        _stopwatch.Start();
        
        foreach (var track in midiFile.Tracks)
        {
            foreach (var midiEvent in track.MidiEvents)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _stopwatch.Stop();
                    return;
                }

                // Calculate when this event should trigger
                long targetMicroseconds = (long)(midiEvent.Time * microsecondsPerTick);
                
                // Wait until it's time to play this event
                while (_stopwatch.ElapsedTicks * 1_000_000L / Stopwatch.Frequency - Latency < targetMicroseconds)
                {
                    await Task.Delay(1, cancellationToken);
                }

                if (midiEvent.MidiEventType != MidiEventType.NoteOn) continue;
                double currentTimeMs = _stopwatch.ElapsedMilliseconds;
                
                // Keep the console output for debugging
                Console.WriteLine($"{_path}: NoteOn at {currentTimeMs:F2}ms - Note: {midiEvent.Arg2}, Velocity: {midiEvent.Arg3}");
                
                // Call the callback function if it's set
                OnNoteOn?.Invoke(currentTimeMs, midiEvent.Arg2, midiEvent.Arg3);
                
            }
        }
        
        _stopwatch.Stop();
        Console.WriteLine("Playback of {0} Completed.", _path);
    }
}