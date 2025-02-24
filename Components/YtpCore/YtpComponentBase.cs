using System;
using Avalonia;
using Avalonia.Controls;

namespace MyFirstAvaloniaApp.Components.YtpCore;

public class YtpComponentBase: UserControl
{

    public static readonly StyledProperty<bool> AnimationOnProperty =
        AvaloniaProperty.Register<YtpComponentBase, bool>(nameof(AnimationOn));
    public bool AnimationOn
    {
        get => GetValue(AnimationOnProperty);
        set => SetValue(AnimationOnProperty, value);
    }
    
    public static readonly StyledProperty<double> StartXProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(StartX), 100);
    public static readonly StyledProperty<double> StartYProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(StartY), 100);
    public static readonly StyledProperty<double> EndXProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(EndX), 100);
    public static readonly StyledProperty<double> EndYProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(EndY), 100);
    public static readonly StyledProperty<double> ScaleXStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ScaleXStart), 2);
    public static readonly StyledProperty<double> ScaleYStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ScaleYStart), 2);
    public static readonly StyledProperty<double> ScaleXEndProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ScaleXEnd), 1);
    public static readonly StyledProperty<double> ScaleYEndProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ScaleYEnd), 1);
    public static readonly StyledProperty<double> RotateStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(RotateStart), 10);
    public static readonly StyledProperty<double> RotateEndProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(RotateEnd), 0);
    public static readonly StyledProperty<double> OpacityStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(OpacityStart), 1);
    public static readonly StyledProperty<double> OpacityEndProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(OpacityEnd), 1);
    public static readonly StyledProperty<double> CWidthProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(CWidth), 1024);
    public static readonly StyledProperty<double> CHeightProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(CHeight), 176);
    public static readonly StyledProperty<int> GWidthProperty =
        AvaloniaProperty.Register<YtpComponentBase, int>(nameof(GWidth), 320);
    public static readonly StyledProperty<int> GHeightProperty =
        AvaloniaProperty.Register<YtpComponentBase, int>(nameof(GHeight), 200);
    public static readonly StyledProperty<CornerRadius> BorderRadiusStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, CornerRadius>(nameof(BorderRadiusStart), new CornerRadius(50));
    public static readonly StyledProperty<CornerRadius> BorderRadiusEndProperty = 
        AvaloniaProperty.Register<YtpComponentBase, CornerRadius>(nameof(BorderRadiusEnd), new CornerRadius(20));
    public static readonly StyledProperty<TimeSpan> AnimationDurationProperty = 
        AvaloniaProperty.Register<YtpComponentBase, TimeSpan>(nameof(AnimationDuration), new TimeSpan(8000000));

    public TimeSpan AnimationDuration
    {
        get => GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }
    
    public CornerRadius BorderRadiusStart
    {
        get => GetValue(BorderRadiusStartProperty);
        set => SetValue(BorderRadiusStartProperty, value);
    }
    public CornerRadius BorderRadiusEnd
    {
        get => GetValue(BorderRadiusEndProperty);
        set => SetValue(BorderRadiusEndProperty, value);
    }
    
    
    public double CWidth
    {
        get => GetValue(CWidthProperty);
        set => SetValue(CWidthProperty, value);
    }
    public double CHeight
    {
        get => GetValue(CHeightProperty);
        set => SetValue(CHeightProperty, value);
    }
    public int GWidth
    {
        get => GetValue(GWidthProperty);
        set => SetValue(GWidthProperty, value);
    }
    public int GHeight
    {
        get => GetValue(GHeightProperty);
        set => SetValue(GHeightProperty, value);
    }
    
    public double StartX
    {
        get => GetValue(StartXProperty);
        set => SetValue(StartXProperty, value);
    }
    public double StartY
    {
        get => GetValue(StartYProperty);
        set => SetValue(StartYProperty, value);
    }
    
    public double EndX
    {
        get => GetValue(EndXProperty);
        set => SetValue(EndXProperty, value);
    }
    public double EndY
    {
        get => GetValue(EndYProperty);
        set => SetValue(EndYProperty, value);
    }
    public double ScaleXStart
    {
        get => GetValue(ScaleXStartProperty);
        set => SetValue(ScaleXStartProperty, value);
    }
    public double ScaleYStart
    {
        get => GetValue(ScaleYStartProperty);
        set => SetValue(ScaleYStartProperty, value);
    }
    public double ScaleXEnd
    {
        get => GetValue(ScaleXEndProperty);
        set => SetValue(ScaleXEndProperty, value);
    }
    public double ScaleYEnd
    {
        get => GetValue(ScaleYEndProperty);
        set => SetValue(ScaleYEndProperty, value);
    }
    public double RotateStart
    {
        get => GetValue(RotateStartProperty);
        set => SetValue(RotateStartProperty, value);
    }
    public double RotateEnd
    {
        get => GetValue(RotateEndProperty);
        set => SetValue(RotateEndProperty, value);
    }
    public double OpacityStart
    {
        get => GetValue(OpacityStartProperty);
        set => SetValue(OpacityStartProperty, value);
    }
    public double OpacityEnd
    {
        get => GetValue(OpacityEndProperty);
        set => SetValue(OpacityEndProperty, value);
    }
    
}