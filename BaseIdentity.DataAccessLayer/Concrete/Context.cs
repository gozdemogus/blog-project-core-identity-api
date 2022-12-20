using System;
using BaseIdentity.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseIdentity.DataAccessLayer.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:127.0.0.1,1433;Database=BlogProject;MultipleActiveResultSets=true;User=SA;Password=MyPass@word;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<Blog>()
                .HasOne(b => b.AppUser)
                .WithMany(au => au.Blogs)
                .HasForeignKey(b => b.AppUserId);

            modelBuilder.Entity<Blog>()
                 .HasMany(b => b.Comments)
                 .WithOne(c => c.Blog)
                 .HasForeignKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
            //ondelete eklenmesinin sebebi, eğer blog silinirse iliskili olan commentler de silinsin

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

