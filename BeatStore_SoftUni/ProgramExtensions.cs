using static BeatStore_SoftUni.Common.ErrorMessages;

using Microsoft.EntityFrameworkCore;

namespace BeatStore_SoftUni
{
    public static class ProgramExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<T>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ErrMigration} {ex.Message}");
                }
            }

            return host;
        }
    }
}