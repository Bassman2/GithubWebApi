namespace GithubWebApi;

public class Tag
{
    internal Tag(TagModel model)
    {
        this.Name = model.Name;
        this.ZipballUrl = model.ZipballUrl;
        this.TarballUrl = model.TarballUrl;
        this.Commit = model.Commit.CastModel<Commit>();
        this.NodeId = model.NodeId;
    }

    public string? Name { get;  }

    public string? ZipballUrl { get;  }

    public string? TarballUrl { get;  }

    public Commit? Commit { get;  }

    public string? NodeId { get;  }
}
