using DatingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {            
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<Like>()
                .HasKey(x => new {x.LikeeID, x.LikerID});

            builder.Entity<Like>()
                .HasOne(x => x.Likee)
                .WithMany(x => x.Likers)
                .HasForeignKey(x => x.LikeeID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(x => x.Liker)
                .WithMany(x => x.Likees)
                .HasForeignKey(x => x.LikerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}