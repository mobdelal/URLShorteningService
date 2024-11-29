using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ShortenedURL> ShortenedURLs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedURL>(builder =>
            {
                builder.Property(a => a.Code).HasMaxLength(7);
                builder.HasIndex(b => b.Code).IsUnique();
            });
        }


    }
}
