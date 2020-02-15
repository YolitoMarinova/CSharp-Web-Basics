using IRunes.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IRunes
{
    public class IRunesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=LENOVO\SQLEXPRESS;Database=IRunesDb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Track>
                (t => t.HasOne(tr => tr.Album)
                       .WithMany(al => al.Tracks)
                       .HasForeignKey(tr => tr.AlbumId)
                       .OnDelete(DeleteBehavior.Restrict));        
        }
    }
}
