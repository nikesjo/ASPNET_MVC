﻿using WebApp.ViewModels.Components;
using WebApp.ViewModels.Home.Partials;

namespace WebApp.ViewModels.Home;

public class HomeIndexViewModel
{
    public string Title { get; set; } = "Ultimate Task Mangement Assistant";
    public ShowcaseViewModel Showcase { get; set; } = new ShowcaseViewModel
    {
        Id = "overview",
        ShowcaseImage = new() { ImageUrl = "images/showcase.svg", AltText = "Task Mangement Assistant" },
        Title = "Task Management Assistant You Gonna Love",
        Text = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.",
        Link = new() { ControllerName = "Downloads", ActionName = "Index", Text = "Get started for free" },
        BrandsText = "Largest companies use our tool to work efficiently",
        Brands =
        [
                new() { ImageUrl = "images/brands/brand_1.svg", AltText = "Brand name 1" },
                new() { ImageUrl = "images/brands/brand_2.svg", AltText = "Brand name 2" },
                new() { ImageUrl = "images/brands/brand_3.svg", AltText = "Brand name 3" },
                new() { ImageUrl = "images/brands/brand_4.svg", AltText = "Brand name 4" },
        ],
    };

    public FeaturesViewModel Features { get; set; } = new FeaturesViewModel
    {
        Id = "features",
        Title = "What Do You Get With Our Tool?",
        Text = "Make sure all your tasks are organized so you can set the priorities and focus on important.",
        Tools =
        [
                new() { ImageUrl = "images/icons/chat.svg", AltText = "Tools picture 1", ToolTitle = "Comment on Tasks", ToolText = "Id mollis consecteur congue egestas egestas suspendisse blandit justo." },
                new() { ImageUrl = "images/icons/presentation.svg", AltText = "Tools picture 2", ToolTitle = "Task Analytics", ToolText = "Non imperdiet facilisis nulla tellus Morbi scelerisque eget adipiscing vulputate." },
                new() { ImageUrl = "images/icons/add-group.svg", AltText = "Tools picture 3", ToolTitle = "Multiple Assignees", ToolText = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris." },
                new() { ImageUrl = "images/icons/bell.svg", AltText = "Tools picture 4", ToolTitle = "Notifications", ToolText = "Diam, suspendisse velit cras ac. Lobortis diam volutpat, eget pellentesque viverra." },
                new() { ImageUrl = "images/icons/tests.svg", AltText = "Tools picture 5", ToolTitle = "Sections & Subtasks", ToolText = "Mi feugiat hac id in. Sit elit placerat lacus nibh lorem ridiculus lectus." },
                new() { ImageUrl = "images/icons/shield.svg", AltText = "Tools picture 6", ToolTitle = "Data Security", ToolText = "Aliquam malesuada neque eget elit nulla vestibulum nunc cras." }
        ],
    };

    public LightDarkModeViewModel LightDarkMode { get; set; } = new LightDarkModeViewModel
    {
        Id = "light-dark-mode",
        SliderButton = new() { ImageUrl = "images/slider-button.svg", AltText = "Slider button" },
        TitleD = "Switch Between",
        Dark = new() { ImageUrl = "images/macbook-pro-dark.svg", AltText = "Macbook pro" },
        TitleL = "Light & Dark Mode",
        Light = new() { ImageUrl = "images/macbook-pro-light.svg", AltText = "Macbook pro" },
    };

    public ManageYourWorkViewModel ManageYourWork { get; set; } = new ManageYourWorkViewModel
    {
        Id = "manage-your-work",
        ManageImage = new() { ImageUrl = "images/manage-your-work.svg", AltText = "Manage your work image" },
        Title = "Manage Your Work",
        Item =
        [
            new() { Text = "Powerful project management"},
            new() { Text = "Transparent work management"},
            new() { Text = "Manage work & focus on the most important tasks"},
            new() { Text = "Track your progress with interactive charts"},
            new() { Text = "Easiest way to track time spent on tasks"},
        ],
        Link = new() { ControllerName = "Downloads", ActionName = "Index", Text = "Learn more " },
    };

    public DownloadAppsViewModel DownloadApps { get; set; } = new DownloadAppsViewModel
    {
        Id = "download-apps",
        PhoneImage = new() { ImageUrl = "images/phone-app.svg", AltText = "Image of a phone" },
        Title = "Download Our App for Any Devices:",
        Apps =
        [
            new() { Text = "App Store", Title = "Editor's Choice", Rating = "rating 4.7, 187K+ reviews", AppImage = new ImageViewModel() { ImageUrl = "images/appstore.svg", AltText = "Link to Appstore"}},
            new() { Text = "Google Play", Title = "Editor's Choice", Rating = "rating 4.8, 187K+ reviews", AppImage = new ImageViewModel() { ImageUrl = "images/googleplay.svg", AltText = "Link to GooglePlay"}},
        ],
    };

    public IntegrateToolsViewModel IntegrateTools { get; set; } = new IntegrateToolsViewModel
    {
        Id = "integrate-tools",
        Title = "Integrate Top Work Tools",
        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin volutpat mollis egestas. Nam luctus facilisis ultrices. Pellentesque volutpat ligula est. Mattis fermentum, at nec lacus.",
        Tools =
        [
            new() { ToolText = "Lorem magnis pretium sed curabitur nunc facilisi nunc cursus sagittis.", ToolImage = new ImageViewModel() { ImageUrl = "images/tools/google.svg", AltText = "Google logo"}},
            new() { ToolText = "In eget a mauris quis. Tortor dui tempus quis integer est sit natoque placerat dolor.", ToolImage = new ImageViewModel() { ImageUrl = "images/tools/zoom.svg", AltText = "Zoom logo"}},
            new() { ToolText = "Id mollis consectetur congue egestas egestas suspendisse blandit justo.", ToolImage = new ImageViewModel() { ImageUrl = "images/tools/slack.svg", AltText = "Slack logo"}},
            new() { ToolText = "Rutrum interdum tortor, sed at nulla. A cursus bibendum elit purus cras praesent.", ToolImage = new ImageViewModel() { ImageUrl = "images/tools/gmail.svg", AltText = "Gmail logo"}},
            new() { ToolText = "Congue pellentesque amet, viverra curabitur quam diam scelerisque fermentum urna.", ToolImage = new ImageViewModel() { ImageUrl = "images/tools/trello.svg", AltText = "Trello logo"}},
            new() { ToolText = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris.", ToolImage = new ImageViewModel() { ImageUrl = "images/tools/mailchimp.svg", AltText = "MailChimp logo"}},
            new() { ToolText = "Ut in turpis consequat odio diam lectus elementum. Est faucibus blandit platea.", ToolImage = new ImageViewModel() { ImageUrl = "images/tools/dropbox.svg", AltText = "Dropbox logo"}},
            new() { ToolText = "Faucibus cursus maecenas lorem cursus nibh. Sociis sit risus id. Sit facilisis dolor arcu.", ToolImage = new ImageViewModel() { ImageUrl = "images/tools/evernote.svg", AltText = "EverNote logo"}},
        ],
    };

    public NewsletterViewModel Newsletter { get; set; } = new NewsletterViewModel
    {
        Id = "newsletter",
        Title = "Don't Want to Miss Anything?",
        ArrowImage = new() { ImageUrl = "images/arrows.svg", AltText = "image of an arrow" },
    };
}