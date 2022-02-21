using Utility;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run(async context =>
{
    var path = context.Request.Path.HasValue ? context.Request.Path.Value.ToLower() : string.Empty;
    switch (path)
    {
        case "/file":
            await FileUtility.WriteFile();
            break;
        case "/memory":
            await FileUtility.WriteMemoryStream();
            break;
        case "/collect":
            GC.Collect();
            break;
        case "/clear":
            FileUtility.ClearFiles();
            break;
        default:
            break;
    }

    await context.Response.WriteAsync(path);
});

await app.RunAsync();