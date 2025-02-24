using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using Avalonia;
using Avalonia.Media.Imaging;
using MyFirstAvaloniaApp.Components.YtpCore.Midi;
using MyFirstAvaloniaApp.misc;

namespace MyFirstAvaloniaApp.Components.YtpCore;

public class BasicTransformEvent {
    public List<double> Position = [100, 100];
    public double Rotation = 2;
    public Vector2 Scale = new Vector2(2, 2);
    public double Opacity = 1;
}

public partial class YtpImage : YtpComponentBase
{
    public readonly MidiDriver MidiDriver;
    private CancellationTokenSource? _playbackCts;
    
    public static readonly StyledProperty<string> ImageSourceProperty =
        AvaloniaProperty.Register<YtpImage, string>(nameof(ImageSource));
    public string ImageSource
    {
        get => GetValue(ImageSourceProperty);
        set
        {
            SetValue(ImageSourceProperty, value);
            if (!string.IsNullOrEmpty(value))
            {
                YtpmvImg.Source = new Bitmap(value);
            }
        }
    }
    public YtpImage(string midiPath, int width=320, int height=200, double latency=0)
    {
        Width = Viewport.GetWindowSize().Width;
        Height = Viewport.GetWindowSize().Height;
        CWidth = Width;
        CHeight = Height;
        GWidth = width;
        GHeight = height;
        
        InitializeComponent();
        MidiDriver = new MidiDriver(latency);
        MidiDriver.Load(midiPath);
        DataContext = this;
        MidiDriver.OnNoteOn += TriggerAnimation;
    }
    
    public void Play()
    {
        // Cancel any existing playback
        _playbackCts?.Cancel();
        
        // Create new cancellation token source
        _playbackCts = new CancellationTokenSource();
        Task.Run(() => MidiDriver.Play(_playbackCts.Token));
    }

    public void Stop()
    {
        // Cancel any existing playback
        _playbackCts?.Cancel();
    }

    // Assuming your Image has x:Name="MyImage"
    public void TriggerAnimation(double timeMs, int note, int velocity)
    {
        // Post to UI thread to avoid freezing
        Dispatcher.UIThread.Post(async void () =>
        {
            try
            {
                AnimationOn = false;
                ScaleXStart = -ScaleXStart;
                ScaleXEnd = -ScaleXEnd;
                RotateStart = -RotateStart;
                await Task.Delay(10);
                AnimationOn = true;
            }
            catch (Exception e)
            {
                throw; // TODO handle exception
            }
        }, DispatcherPriority.Background);
    }
}