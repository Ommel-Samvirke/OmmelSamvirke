namespace OmmelSamvirke.Web.Routing;

[AttributeUsage(AttributeTargets.Field)]
public sealed class PathAttribute : Attribute
{
    public string? Path { get; }

    public PathAttribute(string? path)
    {
        Path = path;
    }
}
