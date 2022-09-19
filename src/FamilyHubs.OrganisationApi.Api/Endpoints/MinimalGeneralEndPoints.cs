using System.Diagnostics;

namespace FamilyHubs.OrganisationApi.Api.Endpoints;

public class MinimalGeneralEndPoints
{
    public void RegisterMinimalGeneralEndPoints(WebApplication app)
    {
        app.MapGet("api/info", (ILogger<MinimalGeneralEndPoints> logger) =>
        {
            try
            {
                var assembly = typeof(MinimalGeneralEndPoints).Assembly;

                var creationDate = File.GetCreationTime(assembly.Location);
                var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

                return Results.Ok($"Version: {version}, Last Updated: {creationDate}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred getting info (api). {exceptionMessage}", ex.Message);
                Debug.WriteLine(ex.Message);
                throw;
            }
        });
    }
}
