using FamilyHubs.IdentityServerHost;
using FamilyHubs.IdentityServerHost.Persistence.Repository;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
//#if USE_IN_MEMORY
//        .ConfigureInMemServices()
//#else
        .ConfigureServices(builder.Configuration)
//#endif
        .ConfigurePipeline();

    if (args.Contains("/seed"))
    {
        Log.Information("Seeding database...");
        await SeedData.EnsureSeedData(app);
        Log.Information("Done seeding database. Exiting");
        return;
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}