var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("Seq", 9002)
    .WithLifetime(ContainerLifetime.Persistent);

var sqlPassword = builder.AddParameter("sqlPassword");
var database = builder.AddSqlServer("Sql", sqlPassword, 9003)
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("SqlDatabase", "templateDb");

builder.AddProject<Projects.VerticalTemplate_Api>("verticaltemplate-api")
    .WithReference(database)
    .WaitFor(database)
    .WaitFor(seq)
    .WithEnvironment("SEQ_SERVER_URL", "http://localhost:9002");

await builder.Build().RunAsync();
