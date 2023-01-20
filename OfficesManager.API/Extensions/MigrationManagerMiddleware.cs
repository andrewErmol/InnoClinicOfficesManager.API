using Microsoft.EntityFrameworkCore;
using OfficesManager.Database;

namespace OfficesManager.API.Extensions
{
    public static class MigrationManagerMiddleware
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<OfficesManagerDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            return app;
        }
    }
}
