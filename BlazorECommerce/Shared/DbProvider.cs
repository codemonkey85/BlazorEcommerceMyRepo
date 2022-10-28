namespace BlazorECommerce.Shared;

public enum DbProvider
{
    [EnumMember(Value = "SQL Server")] SqlServer,

    [EnumMember(Value = "SQLite")] Sqlite,

    [EnumMember(Value = "In Memory")] InMemory,
}
