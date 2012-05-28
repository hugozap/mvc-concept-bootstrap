using System.Data.Entity;

using System.Data.Entity.ModelConfiguration.Conventions;
using App.Models;


namespace App.Data
{
    public class AppContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<UserDetails> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

    }
}
