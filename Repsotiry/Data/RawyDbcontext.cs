using core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Repsotiry.Data
{


    public class RawyDbcontext : IdentityDbContext<BaseUser>
    {
        public RawyDbcontext(DbContextOptions<RawyDbcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Record>()
                .HasOne(r => r.User)
                .WithMany(u => u.Records)
                .HasForeignKey(r => r.BaseUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.User)
                .WithMany(u => u.Playlists)
                .HasForeignKey(p => p.BaseUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.BaseUserId)
                .OnDelete(DeleteBehavior.Restrict);
            // إعداد العلاقة بين Favorite و ApplicationUser
          
            base.OnModelCreating(modelBuilder);

            // Your custom entity configurations
        }
        // DbSets for your entities
        public DbSet<Aurthor> Authors { get; set; }  // Corrected typo in property name
        public DbSet<Book> Books { get; set; }
        public DbSet<Catygory> Categories { get; set; }  // Corrected typo
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
       // public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BaseUser> users { get; set; }



    }
}
