﻿namespace GithubWebApi.Service.Model;

[JsonSourceGenerationOptions]

[JsonSerializable(typeof(BranchModel))]
[JsonSerializable(typeof(IEnumerable<BranchModel>))]

[JsonSerializable(typeof(ProtectionModel))]
[JsonSerializable(typeof(RequiredStatusChecksModel))]

[JsonSerializable(typeof(CommitModel))]

// Pull
[JsonSerializable(typeof(PullCreateModel))]
[JsonSerializable(typeof(PullModel))]
[JsonSerializable(typeof(IEnumerable<PullModel>))]
[JsonSerializable(typeof(List<PullModel>))]
[JsonSerializable(typeof(PullPatchModel))]

[JsonSerializable(typeof(RepositoryModel))]
[JsonSerializable(typeof(IEnumerable<RepositoryModel>))]

[JsonSerializable(typeof(IEnumerable<ReferenceModel>))]
[JsonSerializable(typeof(IEnumerable<RefModel>))]

[JsonSerializable(typeof(TreeModel))]
[JsonSerializable(typeof(TreeCreateModel))]


[JsonSerializable(typeof(UserModel))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}


