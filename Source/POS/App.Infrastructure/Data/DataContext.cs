using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*
                 * To protect potentially sensitive information in your connection string, you should move it out of source code. 
                 * See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                 */
                //optionsBuilder.UseMySql("server=localhost;user id=root;password=;database=app_pos;");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
