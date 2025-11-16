# 🎉 EF Core 10 Native JSON Column Demo

https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew

**EF Core 10 has arrived!** This demo showcases the new native JSON column support in EF Core 10 with SQL Server 2025 and Azure SQL Database.

Now EF Core 10 can leverage SQL Server's native `json` data type, which brings significant improvements over the old `nvarchar(max)` approach. EF Core can even automatically migrate your existing `nvarchar(max)` columns to the new `json` type!

## Why This Matters: `json` > `nvarchar(max)`

✅ **Built-in validation of JSON** - SQL Server validates JSON structure at the database level

✅ **Faster reads/updates** - Native binary storage format for improved performance

✅ **Better querying** - Leverage powerful JSON functions like `JSON_VALUE`, `JSON_QUERY`, and more

## What This Demo Shows

This project demonstrates:
- Using EF Core 10's `.ToJson()` configuration to map complex properties to native JSON columns
- Querying JSON columns with LINQ, which translates to SQL Server's JSON functions
- Running on SQL Server 2025 with compatibility level 170
- Integration with .NET Aspire for easy orchestration

## Supported SQL Server Versions

This native JSON column feature requires:
- **SQL Server 2025** with compatibility level **170 or higher**
- **Azure SQL Database**

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for running SQL Server 2025 container)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/ricksu978/Aspire.EfJsonColumnDemo.git
   cd Aspire.EfJsonColumnDemo
   ```

2. **Run the project with .NET Aspire**
   ```bash
   cd AppHost
   dotnet run
   ```

   This will:
   - Start SQL Server 2025 in a Docker container
   - Apply EF Core migrations (creating the `json` column)
   - Run the WebApi project
   - Open the Aspire Dashboard

3. **Check the console output**

   The demo will output the generated SQL query showing the use of `JSON_VALUE` function:
   ```sql
   SELECT [c].[Id], [c].[Address]
   FROM [Contacts] AS [c]
   WHERE JSON_VALUE([c].[Address], '$.State' RETURNING nvarchar(max)) = N'NSW'
   ```

## Project Structure

- **AppHost** - .NET Aspire orchestration host
- **WebApi** - ASP.NET Core Web API with EF Core 10 DbContext
  - `Contact.cs` - Entity with a complex `Address` property mapped to JSON
  - `AppDbContext.cs` - DbContext configured to use `.ToJson()` for the Address property
  - `Migrations/` - EF Core migrations showing the `json` column type

## Key Code Highlights

### Entity Configuration
```csharp
modelBuilder.Entity<Contact>()
    .ComplexProperty(x => x.Address)
    .ToJson(); // Maps to native JSON column
```

### SQL Server Configuration
```csharp
builder.AddSqlServerDbContext<AppDbContext>("sql-2025", configureDbContextOptions: options =>
{
    options.UseSqlServer(sqlServer =>
        sqlServer.UseCompatibilityLevel(170)); // Required for JSON support
});
```

### EF Core Migration Output
```csharp
// CSharp
Address = table.Column<string>(type: "json", nullable: false)
```

```sql
-- SQL Script
CREATE TABLE [Contacts] (
    [Id] uniqueidentifier NOT NULL,
    [Address] json NOT NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
);
```

## Learn More

- [EF Core 10 Release Notes](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-10.0/whatsnew)
- [SQL Server 2025 JSON Support](https://learn.microsoft.com/en-us/sql/relational-databases/json/json-data-sql-server)
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.

## Author

**Rick Su**
[LinkedIn](https://www.linkedin.com/in/ricksu978/)
[GitHub](https://github.com/ricksu978)