using WebApp.ViewModels.Components;

namespace WebApp.ViewModels.Partials;

public class DownloadAppsViewModel
{
    public string? Id { get; set; }
    public ImageViewModel PhoneImage { get; set; } = null!;
    public string? Title { get; set;}
    public List<AppViewModel>? Apps { get; set; }
}
