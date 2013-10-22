using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instaeventos.Core
{
    public class InstaeventosContext : IdentityDbContext<ApplicationUser>
    {
        public InstaeventosContext()
            : base("InstaeventosContext")
        {
        }
        public DbSet<Event> Events { get; set; }
        public DbSet<InstagramPhoto> InstagramPhotos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InstagramPhoto>().ToTable("InstagramPhotos");
        }
    }
}
