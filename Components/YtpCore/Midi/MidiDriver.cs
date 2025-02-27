using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MidiParser;
using System.Collections.Generic;

namespace MyFirstAvaloniaApp.Components.YtpCore.Midi;

public class MidiDriver
{
    private int _bpm;
    private int _ticksPerQuarterNote;
    private Stopwatch _stopwatch = new();
    private string _path = "Assets/midi/lead.mid";
    
    public delegate void NoteOnEventHandler(double timeMs, int note, int velocity);
    public delegate void NoteOffEventHandler(double timeMs, int note, int velocity);
    public delegate void PlayEndEventHandler(double timeMs);
    
    public event NoteOnEventHandler? OnNoteOn;
    public event NoteOffEventHandler? OnNoteOff;
    public event PlayEndEventHandler? OnPlayEnd;

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
        
        double microsecondsPerQuarterNote = (60.0 * 1_000_000) / _bpm;
        double microsecondsPerTick = microsecondsPerQuarterNote / _ticksPerQuarterNote;
        
        _stopwatch.Start();
        
        // Pre-calculate all event timings
        var events = new List<(long targetMicroseconds, MidiEvent midiEvent)>();
        foreach (var track in midiFile.Tracks)
        {
            foreach (var midiEvent in track.MidiEvents)
            {
                if (midiEvent.MidiEventType == MidiEventType.NoteOn)
                {
                    events.Add(((long)(midiEvent.Time * microsecondsPerTick), midiEvent));
                }
            }
        }
        
        // Sort events by time
        events.Sort((a, b) => a.targetMicroseconds.CompareTo(b.targetMicroseconds));
        
        foreach (var (targetMicroseconds, midiEvent) in events)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _stopwatch.Stop();
                return;
            }

            long currentMicros;
            while ((currentMicros = _stopwatch.ElapsedTicks * 1_000_000L / Stopwatch.Frequency) - Latency < targetMicroseconds)
            {
                if (targetMicroseconds - currentMicros > 1000)
                {
                    await Task.Delay(1, cancellationToken);
                }
                else
                {
                    Thread.SpinWait(10);
                }
            }

            double currentTimeMs = _stopwatch.ElapsedMilliseconds;
            OnNoteOn?.Invoke(currentTimeMs, midiEvent.Arg2, midiEvent.Arg3);
            Console.WriteLine("NoteOn: {0}", midiEvent.Arg2);
        }
        
        _stopwatch.Stop();
        Console.WriteLine("Playback of {0} Completed.", _path);
        OnPlayEnd?.Invoke(_stopwatch.ElapsedMilliseconds);
    }
}