using Microsoft.EntityFrameworkCore;
using WK.Catalog.Api.Configurations;
using WK.Catalog.Infra.Data.EF;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAppConections(builder.Configuration)
    .AddUseCases()
    .AddAndConfigureControllers();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();
app.UseDocumentation();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<CatalogDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();
public partial class Program { }
