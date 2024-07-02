using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SlotGameServer.Persistence.Context
{

    //public class SlotGameServerDbContextFactory : IDesignTimeDbContextFactory<SlotGameServerDbContext>
    //{
    //    public SlotGameServerDbContext CreateDbContext(string[] args)
    //    {
    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetCurrentDirectory())
    //        .AddJsonFile("appsettings.json")
    //        .Build();

    //        var builder = new DbContextOptionsBuilder<SlotGameServerDbContext>();
    //        var connectionString = configuration.GetConnectionString("TaskManagementConnectionString");

    //        builder.UseNpgsql(connectionString);

    //        return new SlotGameServerDbContext(builder.Options);
    //    }
    //}
}
