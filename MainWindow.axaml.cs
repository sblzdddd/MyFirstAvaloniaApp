using Avalonia.Controls;
using Avalonia.Interactivity;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using Avalonia;
using MyFirstAvaloniaApp.Components.YtpCore;
using MyFirstAvaloniaApp.misc;

namespace MyFirstAvaloniaApp;

public partial class MainWindow : Window
{
    private WaveOutEvent? _outputDevice;
    private AudioFileReader? _audioFile;
    private bool _isPlaying;
    private readonly System.Timers.Timer _timer = new (10);

    private List<YtpComponentBase> _ytps = new();
    

    public MainWindow()
    {
        var windowSize = Viewport.GetWindowSize();
        var width = windowSize.Width;
        var height = windowSize.Height;
        Width = width;
        Height = height;
        InitializeComponent();
        if (TransparencyLevelHint.Contains(WindowTransparencyLevel.Mica))
        {
            Console.WriteLine(114514);
            return;
        }

        TransparencyLevelHint = [WindowTransparencyLevel.Mica];
        
        _timer.Elapsed += Timer_Elapsed;
        
        _audioFile = new AudioFileReader("Assets/firstytpmv.wav");
        _outputDevice = new WaveOutEvent();
        _outputDevice.Init(_audioFile);
        
        Opened += OnOpened;
        
        var space = 10;
        
        // 史上最史
        // 限于avalonia实在是太局限了，我还不会写shader，所以暂时不是很想写data serialization/deserialization
        var lead = AddDynamicImage("Assets/images/YMN-small.png", "Assets/midi/lead.mid");
        lead.StartX = width / 2 - space - 320 - 15;
        lead.EndX = width / 2 - space - 320;
        lead.StartY = height/2 - space - 200 - 15;
        lead.EndY = height/2 - space - 200;
        var chord1 = AddDynamicImage("Assets/images/FYW.png", "Assets/midi/chord.mid");
        chord1.ScaleXStart = -2;
        chord1.ScaleXEnd = -1;
        chord1.StartX = width / 2 + space + 15;
        chord1.EndX = width / 2 + space;
        chord1.StartY = height/2 - space - 200 - 15;
        chord1.EndY = height/2 - space - 200;
        var chord2 = AddDynamicImage("Assets/images/FYW.png", "Assets/midi/chord.mid");
        chord2.StartX = width / 2 - space - 320 - 15;
        chord2.EndX = width / 2 - space - 320;
        chord2.StartY = height/2 + space + 15;
        chord2.EndY = height/2 + space;
        var bass = AddDynamicImage("Assets/images/NIU-small.png", "Assets/midi/bass.mid");
        bass.StartX = width / 2 + space + 15;
        bass.EndX = width / 2 + space;
        bass.StartY = height/2 + space + 15;
        bass.EndY = height/2 + space;

        for (int i = 0; i <= 7; ++i)
        {
            var arp = AddDynamicImage("Assets/images/ddd-small.png", "Assets/midi/arp.mid", 80, 80, (i)*87719.298);
            arp.StartX = width / 2 - 390 * -(2*(i % 2)-1) - 40 * -(2*(i % 2)-1)-40;
            arp.EndX = width / 2 - 390 * -(2*(i % 2)-1)-40 * -(2*(i % 2)-1)-40;
            arp.StartY = 85 + (i / 2)*100;
            arp.EndY = 85 + (i / 2)*100;
            arp.OpacityStart = 1;
            arp.OpacityEnd = 0;
            arp.ScaleXStart = 1.3;
            arp.ScaleYStart = 1.3;
            arp.BorderRadiusStart = new CornerRadius(26);
            arp.BorderRadiusEnd = new CornerRadius(16);
            arp.AnimationDuration = new TimeSpan(8000000);
        }
        var crashOverlay = AddDynamicImage("Assets/images/crash.png", "Assets/midi/crash.mid", width, height, 0);
        crashOverlay.StartY = 0;
        crashOverlay.StartX = 0;
        crashOverlay.EndX = 0;
        crashOverlay.EndY = 0;
        crashOverlay.OpacityStart = 1;
        crashOverlay.OpacityEnd = 0;
        crashOverlay.BorderRadiusEnd = new CornerRadius(0);
        crashOverlay.BorderRadiusStart = new CornerRadius(0);
        crashOverlay.RotateStart = 5;
        crashOverlay.AnimationDuration = new TimeSpan(30000000);

        var kickOverlay = AddDynamicImage("Assets/images/kick.jpg", "Assets/midi/kick.mid", width, height, 0, true);
        kickOverlay.StartY = 0;
        kickOverlay.StartX = 0;
        kickOverlay.EndX = 0;
        kickOverlay.EndY = 0;
        kickOverlay.OpacityStart = 1;
        kickOverlay.OpacityEnd = 1;
        kickOverlay.BorderRadiusStart = new CornerRadius(0);
        kickOverlay.BorderRadiusEnd = new CornerRadius(0);
        kickOverlay.AnimationDuration = new TimeSpan(5000000);

        var hatOverlay = AddDynamicImage("Assets/images/hat.png", "Assets/midi/hat.mid", width, height, 0, true);
        hatOverlay.StartY = 0;
        hatOverlay.StartX = 0;
        hatOverlay.EndX = 0;
        hatOverlay.EndY = 0;
        hatOverlay.OpacityStart = 1;
        hatOverlay.OpacityEnd = 1;
        hatOverlay.BorderRadiusStart = new CornerRadius(0);
        hatOverlay.BorderRadiusEnd = new CornerRadius(0);
        hatOverlay.AnimationDuration = new TimeSpan(5000000);

        var snareOverlay = AddDynamicImage("Assets/images/snare.png", "Assets/midi/snare.mid", width, height, 0, true);
        snareOverlay.StartY = 0;
        snareOverlay.StartX = 0;
        snareOverlay.EndX = 0;
        snareOverlay.EndY = 0;
        snareOverlay.OpacityStart = 1;
        snareOverlay.OpacityEnd = 1;
        snareOverlay.BorderRadiusStart = new CornerRadius(0);
        snareOverlay.BorderRadiusEnd = new CornerRadius(0);
        snareOverlay.AnimationDuration = new TimeSpan(5000000);
        
#if DEBUG
        this.AttachDevTools();
#endif
        
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        // 我他妈真是个小丑
        // Position = Viewport.GetWindowCenterPos(Viewport.GetWindowSize());
    }

    private async void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        _outputDevice?.Play();

        foreach (var ytpGraphic in _ytps) 
        {
            ytpGraphic.Play();
        }
        
        _isPlaying = true;
        _timer.Start();
        
        await Task.Delay(700);
        BlackOverlay.Opacity = 0;
    }

    private YtpImage AddDynamicImage(string imagePath, string midiPath, int width=320, int height=200, double latency=0, bool isOverlay=false)
    {
        var dynamicImage = new YtpImage(midiPath, width, height, latency, isOverlay);
        dynamicImage.ImageSource = imagePath;
        
        ImageContainer.Children.Add(dynamicImage);

        _ytps.Add(dynamicImage);

        return dynamicImage;
    }

    private void PauseButton_Click(object sender, RoutedEventArgs e)
    {
        if (!_isPlaying) return;
        _outputDevice?.Pause();
        _isPlaying = false;
        _timer.Stop();
    }

    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        foreach (var ytpGraphic in _ytps) 
        {
            ytpGraphic.Stop();
            if(ytpGraphic.IsOverlay)
            {
                Console.WriteLine(111);
                ytpGraphic.OpacityStart = 1;
                ytpGraphic.OpacityEnd = 1;
                ytpGraphic.AnimationOn = false;
            }
            
        }
        BlackOverlay.Opacity = 1;
        if (_outputDevice == null) return;
        _outputDevice.Stop();
        _isPlaying = false;
        _timer.Stop();
        if (_audioFile == null) return;
        _audioFile.Position = 0;
        UpdateTimeDisplay();
    }

    private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        if (_audioFile != null)
        {
            Dispatcher.UIThread.Post(UpdateTimeDisplay);
        }
    }

    private void UpdateTimeDisplay()
    {
        
        if (_audioFile != null)
        {
            TimeDisplay.Text = $"{_audioFile.CurrentTime} / {_audioFile.TotalTime}";
        }
    }

    private void DisposeAudio()
    {
        if (_outputDevice != null)
        {
            _outputDevice.Stop();
            _outputDevice.Dispose();
            _outputDevice = null;
        }
        if (_audioFile != null)
        {
            _audioFile.Dispose();
            _audioFile = null;
        }
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        base.OnClosing(e);
        DisposeAudio();
        _timer.Dispose();
    }
}