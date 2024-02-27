namespace WebApp.ViewModels.Components;

public class AppViewModel
{
    public string? Text { get; set; }
    public string? Title { get; set; }
    public string? Rating { get; set; }
    public ImageViewModel AppImage { get; set; } = null!;
}
