using WebApp.ViewModels.Components;

namespace WebApp.ViewModels.Sections;

public class ManageYourWorkViewModel
{
    public string? Id { get; set; }
    public ImageViewModel ManageImage { get; set; } = null!;
    public string? Title { get; set; }
    public List<ManageItemViewModel>? Item { get; set; }
    public LinkViewModel Link { get; set; } = new LinkViewModel();
}
