using Microsoft.EntityFrameworkCore;
using WebLibrary.DAL.Models;

namespace WebLibrary.DAL
{
    public class MainDbContext : DbContext
    {

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> context) : base(context) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Main");

            modelBuilder.Entity<Book>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Author>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Author>().HasData(
            new Author { Id = Guid.NewGuid(), Name = "Agatha Christie", Description = "Dame Agatha Mary Clarissa Christie, was an English writer known for her 66 detective novels and 14 short story collections, particularly those revolving around fictional detectives Hercule Poirot and Miss Marple." },
            new Author { Id = Guid.NewGuid(), Name = "William Shakespeare", Description = "William Shakespeare was an English playwright, poet and actor." },
            new Author { Id = Guid.NewGuid(), Name = "Barbara Cartland" },
            new Author { Id = Guid.NewGuid(), Name = "Danielle Steel" });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Database=WebLogic;Username=postgres;Password=12345");
        //}
    }

}
