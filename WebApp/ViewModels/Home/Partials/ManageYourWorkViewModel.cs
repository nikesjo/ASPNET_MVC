using WebApp.ViewModels.Components;
using WebApp.ViewModels.Home.Partials.PartialComponents;

namespace WebApp.ViewModels.Home.Partials;

public class ManageYourWorkViewModel
{
    public string? Id { get; set; }
    public ImageViewModel ManageImage { get; set; } = null!;
    public string? Title { get; set; }
    public List<ManageItemViewModel>? Item { get; set; }
    public LinkViewModel Link { get; set; } = new LinkViewModel();
}
