namespace GithubWebApi.Model;

internal class TreeCreateModel
{
    [JsonPropertyName("base_tree")]
    public string? BaseTree { get; set; }

    [JsonPropertyName("tree")]
    public List<TreeItemModel>? Tree { get; set; }
}

/*
{
    "base_tree":"9fb037999f264ba9a7fc6274d15fa3ae2ab98312",
    "tree":
    [
        {
            "path":"file.rb",
            "mode":"100644",
            "type":"blob",  
            "sha":"44b4fc6d56897b048c772eb4087f854f46256132"
        }
    ]
}
*/