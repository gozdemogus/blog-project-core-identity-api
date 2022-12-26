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
            optionsBuilder.UseSqlServer("Server=tcp:127.0.0.1,1433;Database=BlogProject2;MultipleActiveResultSets=true;User=SA;Password=MyPass@word;");
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

            //blog-yorumlar iliskisi
            modelBuilder.Entity<Comment>()
             .HasOne(c => c.Blog)
             .WithMany(b => b.Comments)
                .OnDelete(DeleteBehavior.Cascade);
            //ondelete eklenmesinin sebebi, eğer blog silinirse iliskili olan commentler de silinsin

            //yorumlara verilen cevaplar iliskisi

            modelBuilder.Entity<Comment>()
        .HasMany(c => c.Replies)
        .WithOne(r => r.Comment)
        .HasForeignKey(r => r.CommentId)
        .OnDelete(DeleteBehavior.Cascade);


            //yorum-yazar iliskisi
            modelBuilder.Entity<Comment>()
     .HasOne(c => c.AppUser)
     .WithMany(a => a.Comments)
     .HasForeignKey(c => c.AppUserId)
     .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reply>()
   .HasOne(c => c.AppUser)
   .WithMany(a => a.Replies)
   .HasForeignKey(c => c.AppUserId)
   .OnDelete(DeleteBehavior.NoAction);


        }




        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
    }
}

