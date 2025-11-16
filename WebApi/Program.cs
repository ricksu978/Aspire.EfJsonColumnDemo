using Microsoft.EntityFrameworkCore;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Aspire.Microsoft.EntityFrameworkCore.SqlServer
builder.AddSqlServerDbContext<AppDbContext>("sql-2025", configureDbContextOptions: options =>
{
    // SQL Server 205 - Enable compatibility level 170
    options.UseSqlServer(sqlServer => sqlServer.UseCompatibilityLevel(170));
});

var app = builder.Build();

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

dbContext.Database.Migrate();

var sqlQuery = dbContext.Contacts.Where(x => x.Address.State == "NSW").ToQueryString();

// The generated SQL should utilize the JSON functions `JSON_VALUE`
Console.WriteLine(sqlQuery);

app.Run();
