using Avalonia;

namespace MyFirstAvaloniaApp.misc;

// 史上最唐
public static class Viewport
{

    public static PixelSize GetWindowSize()
    {
        return new PixelSize(1024, 576);
    }
    
    // public static float GetDpi()
    // {
    //     return 1.5f;
    // }

    // public static PixelSize GetScreenSize()
    // {
    //     return new PixelSize(2560, 1600);
    //     if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop 
    //         && desktop.MainWindow is not null && desktop.MainWindow.Screens.Primary is not null)
    //     {
    //         return desktop.MainWindow.Screens.Primary.Bounds.Size;
    //     }
    //     return PixelSize.Empty;
    // }

    // public static PixelPoint GetWindowCenterPos(PixelSize windowSize)
    // {
    //     
    //     PixelSize screenSize = GetScreenSize();
    //     
    //     Console.WriteLine($"Screen Width: {screenSize.Width}, Screen Height: {screenSize.Height}");
    //     int x = screenSize.Width / 2 - (int)(windowSize.Width / 2f * GetDpi());
    //     int y = screenSize.Height / 2 - (int)(windowSize.Height / 2f * GetDpi());
    //     return new PixelPoint(x, y - 200);
    // }

}