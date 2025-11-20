const string baseContainerName = "modularmonolithtemplate";

var builder = DistributedApplication.CreateBuilder(args);

var rabbit = builder.AddRabbitMQ("RabbitMq")
    .WithLifetime(ContainerLifetime.Session)
    .WithManagementPlugin(8001)
    .WithContainerName($"{baseContainerName}-rabbit");

var seq = builder.AddSeq("Seq", 8002)
    .WithLifetime(ContainerLifetime.Session)
    .WithContainerName($"{baseContainerName}-seq");

var redis = builder.AddRedis("Redis", 8004)
    .WithLifetime(ContainerLifetime.Session)
    .WithContainerName($"{baseContainerName}-redis");

var sqlPassword = builder.AddParameter("sqlPassword");
var database = builder.AddSqlServer("Sql", sqlPassword, 8003)
    .WithLifetime(ContainerLifetime.Session)
    .WithContainerName($"{baseContainerName}-sql")
    .AddDatabase("SqlDatabase", "templateDb");

builder.AddProject<Projects.ModularMonolithTemplate_Api>($"{baseContainerName}-api")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(redis)
    .WaitFor(redis)
    .WithReference(rabbit)
    .WaitFor(rabbit)
    .WaitFor(seq)
    .WithEnvironment("SEQ_SERVER_URL", "http://localhost:8002")
    .WithUrls(context =>
    {
        foreach (var url in context.Urls)
        {
            url.DisplayLocation = UrlDisplayLocation.DetailsOnly;
        }

        context.Urls.Add(new ResourceUrlAnnotation
        {
            DisplayText = "Swagger UI",
            Url = "/swagger",
            Endpoint = context.GetEndpoint("https")
        });
    });

await builder.Build().RunAsync();
