﻿using WebApp.ViewModels.Components;

namespace WebApp.ViewModels.Sections;

public class IntegrateToolsViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Text { get; set; }
    public List<ToolsBoxViewModel>? Tools { get; set; }
}
