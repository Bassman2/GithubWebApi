
namespace GithubWebApi;

[DebuggerDisplay("{FullName}")]
public class Repository
{
    internal Repository()
    { }

    public long? Id { get; internal init; }

    public string? NodeId { get; internal init; }

    public string? Name { get; internal init; }

    public string? FullName { get; internal init; }

    public User? Owner { get; internal init; }

    public bool? Private { get; internal init; }

    public string? HtmlUrl { get; internal init; }

    public string? Description { get; internal init; }

    public bool? Fork { get; internal init; }

    public string? Url { get; internal init; }

    public string? ArchiveUrl { get; internal init; }
}
