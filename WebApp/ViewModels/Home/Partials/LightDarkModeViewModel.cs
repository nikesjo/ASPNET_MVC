using WebApp.ViewModels.Components;

namespace WebApp.ViewModels.Home.Partials;

public class LightDarkModeViewModel
{
    public string? Id { get; set; }
    public ImageViewModel SliderButton { get; set; } = null!;
    public string? TitleD { get; set; }
    public ImageViewModel Dark { get; set; } = null!;
    public string? TitleL { get; set; }
    public ImageViewModel Light { get; set; } = null!;
}
