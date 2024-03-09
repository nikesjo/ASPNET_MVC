using WebApp.ViewModels.Components;

namespace WebApp.ViewModels.Home.Partials.PartialComponents;

public class AppViewModel
{
    public string? Text { get; set; }
    public string? Title { get; set; }
    public string? Rating { get; set; }
    public ImageViewModel AppImage { get; set; } = null!;
}
