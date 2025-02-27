using System;
using Avalonia;
using Avalonia.Controls;

namespace MyFirstAvaloniaApp.Components.YtpCore;

public class YtpComponentBase: UserControl
{
    private bool _isOverlay;
    public static readonly StyledProperty<bool> AnimationOnProperty =
        AvaloniaProperty.Register<YtpComponentBase, bool>(nameof(AnimationOn));
    public virtual bool AnimationOn
    {
        get => GetValue(AnimationOnProperty);
        set => SetValue(AnimationOnProperty, value);
    }
    public virtual bool IsOverlay
    {
        get => _isOverlay;
    }
    // 屎山
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
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(RotateStart), 5);
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
        AvaloniaProperty.Register<YtpComponentBase, CornerRadius>(nameof(BorderRadiusEnd), new CornerRadius(32));
    public static readonly StyledProperty<TimeSpan> AnimationDurationProperty = 
        AvaloniaProperty.Register<YtpComponentBase, TimeSpan>(nameof(AnimationDuration), new TimeSpan(8000000));
    public static readonly StyledProperty<double> ShadowBlurRadiusStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ShadowBlurRadiusStart), 20);
    public static readonly StyledProperty<double> ShadowBlurRadiusEndProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ShadowBlurRadiusEnd), 20);
    public static readonly StyledProperty<double> ShadowOpacityStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ShadowOpacityStart), 0.4);
    public static readonly StyledProperty<double> ShadowOpacityEndProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ShadowOpacityEnd), 0.4);
    public static readonly StyledProperty<double> ShadowOffsetXStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ShadowOffsetXStart), 5);
    public static readonly StyledProperty<double> ShadowOffsetXEndProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ShadowOffsetXEnd), 5);
    public static readonly StyledProperty<double> ShadowOffsetYStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ShadowOffsetYStart), 5);
    public static readonly StyledProperty<double> ShadowOffsetYEndProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(ShadowOffsetYEnd), 5);
    public static readonly StyledProperty<double> BlurRadiusStartProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(BlurRadiusStart), 0);
    public static readonly StyledProperty<double> BlurRadiusEndProperty =
        AvaloniaProperty.Register<YtpComponentBase, double>(nameof(BlurRadiusEnd), 0);


    public double ShadowBlurRadiusStart
    {
        get => GetValue(ShadowBlurRadiusStartProperty);
        set => SetValue(ShadowBlurRadiusStartProperty, value);
    }   
    public double ShadowBlurRadiusEnd
    {
        get => GetValue(ShadowBlurRadiusEndProperty);
        set => SetValue(ShadowBlurRadiusEndProperty, value);
    }
    public double ShadowOpacityStart
    {
        get => GetValue(ShadowOpacityStartProperty);
        set => SetValue(ShadowOpacityStartProperty, value);
    }
    public double ShadowOpacityEnd
    {
        get => GetValue(ShadowOpacityEndProperty);
        set => SetValue(ShadowOpacityEndProperty, value);
    }
    public double ShadowOffsetXStart
    {   
        get => GetValue(ShadowOffsetXStartProperty);
        set => SetValue(ShadowOffsetXStartProperty, value);
    }
    public double ShadowOffsetXEnd
    {
        get => GetValue(ShadowOffsetXEndProperty);
        set => SetValue(ShadowOffsetXEndProperty, value);
    }
    public double ShadowOffsetYStart
    {
        get => GetValue(ShadowOffsetYStartProperty);
        set => SetValue(ShadowOffsetYStartProperty, value);
    }
    public double ShadowOffsetYEnd
    {
        get => GetValue(ShadowOffsetYEndProperty);
        set => SetValue(ShadowOffsetYEndProperty, value);
    }
    public double BlurRadiusStart
    {
        get => GetValue(BlurRadiusStartProperty);
        set => SetValue(BlurRadiusStartProperty, value);
    }
    public double BlurRadiusEnd
    {
        get => GetValue(BlurRadiusEndProperty);
        set => SetValue(BlurRadiusEndProperty, value);
    }
    
    

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
    
    public virtual void Play() {}
    
    public virtual void Stop() {}
    
    public virtual void TriggerAnimation(double timeMs, int note, int velocity) {}
    
    public virtual void Load(string path) {}
    
}