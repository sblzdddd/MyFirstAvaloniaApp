using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using Avalonia;
using Avalonia.Media.Imaging;
using MyFirstAvaloniaApp.Components.YtpCore.Midi;
using MyFirstAvaloniaApp.misc;
using Avalonia.Controls;

namespace MyFirstAvaloniaApp.Components.YtpCore;

// 暂时没用
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
    private readonly bool _isOverlay;
    private static int _zIndex;
    
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
    public override bool IsOverlay
    {
        get => _isOverlay;
    }
    public YtpImage(string midiPath, int width=320, int height=200, double latency=0, bool isOverlay=false)
    {
        Width = Viewport.GetWindowSize().Width;
        Height = Viewport.GetWindowSize().Height;
        CWidth = Width;
        CHeight = Height;
        GWidth = width;
        GHeight = height;
        _isOverlay = isOverlay;
        
        InitializeComponent();
        MidiDriver = new MidiDriver(latency);
        MidiDriver.Load(midiPath);
        DataContext = this;
        MidiDriver.OnNoteOn += TriggerAnimation;
        MidiDriver.OnPlayEnd += PlayEnd;
    }
    
    public override void Play()
    {
        _playbackCts?.Cancel();
        
        _playbackCts = new CancellationTokenSource();
        Task.Run(() => MidiDriver.Play(_playbackCts.Token));
    }

    public override void Stop()
    {
        _playbackCts?.Cancel();
    }
    public async void PlayEnd(double timeMs)
    {
        if (_isOverlay)
        {
            await Task.Delay(100); // Add 100ms delay
            
            Dispatcher.UIThread.Post(() =>
            {
                // overlay播放完毕隐藏
                OpacityEnd = 0;
                AnimationOn = true;
            });
        }
    }

    public override void TriggerAnimation(double timeMs, int note, int velocity)
    {
        Dispatcher.UIThread.Post(() =>
        {
            // 翻转
            AnimationOn = false;
            ScaleXStart = -ScaleXStart;
            ScaleXEnd = -ScaleXEnd;
            RotateStart = -RotateStart;
            
            // overlay声成在最前面
            if (_isOverlay)
            {
                _zIndex++;
                SetValue(ZIndexProperty, _zIndex);
            }
            
            AnimationOn = true;
        }, DispatcherPriority.Background);
    }
}