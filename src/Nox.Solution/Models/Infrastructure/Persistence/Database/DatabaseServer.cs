﻿using Nox.Types.Schema;


namespace Nox.Solution;

[GenerateJsonSchema]
public class DatabaseServer : ServerBase
{
    [Required]
    [Title("The database provider.")]
    [Description("The provider used for this database server. Examples include SqlServer, Postgres and others.")]
    [AllowVariable]
    public DatabaseServerProvider Provider { get; internal set; } = DatabaseServerProvider.SqlServer;
}
