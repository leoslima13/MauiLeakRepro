namespace MemoryLeakTest;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var navPage = new NavigationPage(new MainPage());
        navPage.Popped += (s, e) =>
        {
            DestroyBehaviors((e.Page as ContentPage)!);
        };
        MainPage = navPage;
    }
    
    private static void DestroyBehaviors(ContentPage page)
    {
        ClearBehaviors(page.Content);
    }

    private static void ClearBehaviors(Element element)
    {
        switch (element)
        {
            case Layout layout:
            {
                foreach (var child in layout.Children)
                {
                    if (child is not VisualElement ve) continue;
                    ve.Behaviors?.Clear();
                    ClearBehaviors(ve);
                }

                break;
            }
            case ContentView { Content: not null } contentView:
                ClearBehaviors(contentView.Content);
                break;
        }
    }
}