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
        }
        public DbSet<BookShop.Models.Author> Author { get; set; }
        public DbSet<BookShop.Models.Book> Book { get; set; }
        public DbSet<BookShop.Models.Countries> Countries { get; set; }
        public DbSet<BookShop.Models.Sales> Sales { get; set; }
        public DbSet<BookShop.Models.Shops> Shops { get; set; }
        public DbSet<BookShop.Models.Themes> Themes { get; set; }
    }
}
