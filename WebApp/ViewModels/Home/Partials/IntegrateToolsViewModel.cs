using WebApp.ViewModels.Home.Partials.PartialComponents;

namespace WebApp.ViewModels.Home.Partials;

public class IntegrateToolsViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public List<ToolsBoxViewModel>? Tools { get; set; }
}
