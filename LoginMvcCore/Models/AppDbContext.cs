using Microsoft.EntityFrameworkCore;


namespace LoginMvcCore.Models
{
    public class AppDbContext:DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbConOptions):base(dbConOptions)
        {

        }
        public DbSet<UserLogInfo> UserLogInfos { get; set; }
    }
}
