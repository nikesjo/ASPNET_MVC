using WebApp.Models;
using WebApp.ViewModels.Components;

namespace WebApp.ViewModels.Sections;

public class NewsletterViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public ImageViewModel ArrowImage { get; set; } = null!;
    public NewsletterModel Form { get; set; } = new NewsletterModel();
}
