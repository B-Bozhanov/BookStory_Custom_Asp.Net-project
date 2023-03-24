namespace BookStory.Data.Data
{
    using BookStory.Common;
    using BookStory.Data.Models;

    using Microsoft.EntityFrameworkCore;

    internal class ApplicationDbContext : DbContext
    {
        internal ApplicationDbContext()
        {
        }

        internal ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        internal DbSet<Book> Books { get; set; }

        internal DbSet<Account> Accounts { get; set; }

        internal DbSet<Address> Addresses { get; set; }

        internal DbSet<Author> Authors { get; set; }

        internal DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionSettings.RemoteDbConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
