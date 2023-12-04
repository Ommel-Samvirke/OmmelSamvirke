using System.Reflection;

namespace OmmelSamvirke.Web.Routing;

public enum PageEnum
{
    [Path(PageRoutes.Home)]
    Home,
    [Path(PageRoutes.PageEditor)]
    PageEditor
}

public static class PageExtensions
{
    public static string? GetPath(this PageEnum page)
    {
        FieldInfo? field = typeof(PageEnum).GetField(page.ToString());
        if (field == null) return null;

        if (Attribute.GetCustomAttribute(field, typeof(PathAttribute)) is PathAttribute attr)
        {
            return attr.Path;
        }

        return null;
    }
}
