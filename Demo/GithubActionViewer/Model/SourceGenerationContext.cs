namespace GithubActionViewer.Model;

[JsonSourceGenerationOptions(
//JsonSerializerDefaults.Web,
DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
WriteIndented = true,
AllowTrailingCommas = true,
ReadCommentHandling = JsonCommentHandling.Skip)]
[JsonSerializable(typeof(MainModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{ }
