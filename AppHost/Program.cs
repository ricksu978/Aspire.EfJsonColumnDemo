using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var sql2025 = builder.AddSqlServer("sql-2025").WithImage("mssql/server:2025-latest");

builder.AddProject<WebApi>("api")
    .WithReference(sql2025)
    .WaitFor(sql2025);

builder.Build().Run();
