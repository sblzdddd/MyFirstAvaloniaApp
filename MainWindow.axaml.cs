using Avalonia.Controls;
using Avalonia.Interactivity;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    private readonly System.Timers.Timer _timer;
    private Stopwatch _stopwatch;

    private List<YtpImage> _ytps = new();

    public MainWindow()
    {
        var Size = Viewport.GetWindowSize();
        var width = Size.Width;
        var height = Size.Height;
        Width = width;
        Height = height;
        InitializeComponent();
        
        _timer = new System.Timers.Timer(10);
        _timer.Elapsed += Timer_Elapsed;
        _stopwatch = new Stopwatch();
        
        _audioFile = new AudioFileReader("Assets/firstytpmv.wav");
        _outputDevice = new WaveOutEvent();
        _outputDevice.Init(_audioFile);
        
        Opened += OnOpened;

        
        
        var lead = AddDynamicImage("Assets/images/YMN-small.png", "Assets/midi/lead.mid");
        lead.StartX = width / 2 - 25 - 320 - 15;
        lead.EndX = width / 2 - 25 - 320;
        lead.StartY = height/2 - 25 - 200 - 15;
        lead.EndY = height/2 - 25 - 200;
        var chord1 = AddDynamicImage("Assets/images/FYW.png", "Assets/midi/chord.mid");
        chord1.ScaleXStart = -2;
        chord1.ScaleXEnd = -1;
        chord1.StartX = width / 2 + 25 + 15;
        chord1.EndX = width / 2 + 25;
        chord1.StartY = height/2 - 25 - 200 - 15;
        chord1.EndY = height/2 - 25 - 200;
        var chord2 = AddDynamicImage("Assets/images/FYW.png", "Assets/midi/chord.mid");
        chord2.StartX = width / 2 - 25 - 320 - 15;
        chord2.EndX = width / 2 - 25 - 320;
        chord2.StartY = height/2 + 25 + 15;
        chord2.EndY = height/2 + 25;
        var bass = AddDynamicImage("Assets/images/NIU-small.png", "Assets/midi/bass.mid");
        bass.StartX = width / 2 + 25 + 15;
        bass.EndX = width / 2 + 25;
        bass.StartY = height/2 + 25 + 15;
        bass.EndY = height/2 + 25;

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
            arp.AnimationDuration = new TimeSpan(4000000);
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
    }

    private void OnOpened(object? sender, EventArgs e)
    {
        Position = Viewport.GetWindowCenterPos(Viewport.GetWindowSize());
    }

    private void PlayButton_Click(object sender, RoutedEventArgs e)
    {
        _outputDevice?.Play();

        foreach (var ytpGraphic in _ytps) 
        {
            ytpGraphic.Play();
        }
        
        _isPlaying = true;
        _timer.Start();
        _stopwatch.Start();
    }

    private YtpImage AddDynamicImage(string imagePath, string midiPath, int width=320, int height=200, double latency=0)
    {
        var dynamicImage = new YtpImage(midiPath, width, height, latency);
        dynamicImage.ImageSource = imagePath;
        

        // Add the new DynamicImage to a parent container
        ImageContainer.Children.Add(dynamicImage);

        _ytps.Add(dynamicImage);

        return dynamicImage;
    }

    private void PauseButton_Click(object sender, RoutedEventArgs e)
    {
        _stopwatch.Stop();
        if (!_isPlaying) return;
        _outputDevice?.Pause();
        _isPlaying = false;
        _timer.Stop();
        _stopwatch.Stop();
    }

    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        foreach (var ytpGraphic in _ytps) 
        {
            ytpGraphic.Stop();
        }
        if (_outputDevice == null) return;
        _outputDevice.Stop();
        _isPlaying = false;
        _timer.Stop();
        _stopwatch.Stop();
        _stopwatch.Reset();
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
            TimeDisplay.Text = $"{_stopwatch.Elapsed.TotalMilliseconds} / {_audioFile.CurrentTime} / {_audioFile.TotalTime}";
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