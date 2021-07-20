using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BookShop.Models;

namespace BookShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
     : base(options)
        {
            ChangeTracker.Tracked += ChangeTracker_Tracked;
        }

        private void ChangeTracker_Tracked(object sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {
            if (e.Entry.Entity is Book book && book.Codes == null)
            {
                book.Codes = new HashSet<string>();
            }
        }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>(b =>
            {
                b.Property(e => e.Codes).HasConversion<string>(
                    set => System.Text.Json.JsonSerializer.Serialize(set, null),
                    str => System.Text.Json.JsonSerializer.Deserialize<HashSet<string>>(str, null));
            });
        }

        public DbSet<Countries> Countries { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Themes> Themes { get; set; }
    }
}
