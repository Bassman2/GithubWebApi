namespace GithubActionViewer.Model;

public class MainModel : IJsonOnDeserialized
{
    public static MainModel? Load(string path)
    {
        JsonTypeInfo<MainModel> jsonTypeInfo = (JsonTypeInfo<MainModel>)SourceGenerationContext.Default.GetTypeInfo(typeof(MainModel))!;

        using var stream = System.IO.File.OpenRead(path);
        MainModel? mainModel = JsonSerializer.Deserialize<MainModel>(stream, jsonTypeInfo);
        return mainModel;
    }

    public void OnDeserialized()
    {
    }

    [JsonPropertyName("servers")]
    public List<ServerModel> Servers { get; set; } = [];



}
