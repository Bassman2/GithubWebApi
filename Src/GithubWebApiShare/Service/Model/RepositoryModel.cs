﻿namespace GithubWebApi.Service.Model;

[DebuggerDisplay("{FullName}")]
internal class RepositoryModel
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("node_id")]
    public string? NodeId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }

    [JsonPropertyName("owner")]
    public UserModel? Owner { get; set; }

    [JsonPropertyName("private")]
    public bool? Private { get; set; }

    [JsonPropertyName("html_url")]
    public string? HtmlUrl { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("fork")]
    public bool? Fork { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("archive_url")]
    public string? ArchiveUrl { get; set; }

    public static implicit operator Repository?(RepositoryModel? model)
    {
        return model is null ? null : new Repository()
        {
            Id = model.Id,
            NodeId = model.NodeId,
            Name = model.Name,
            FullName = model.FullName,
            Owner = model.Owner,
            Private = model.Private,
            HtmlUrl = model.HtmlUrl,
            Description = model.Description,
            Fork = model.Fork,
            Url = model.Url,
            ArchiveUrl = model.ArchiveUrl
        };
    }
}
