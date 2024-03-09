using WebApp.ViewModels.Components;

namespace WebApp.ViewModels.Partials;

public class FeaturesViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public List<FeaturesBoxViewModel>? Tools { get; set; }
}
