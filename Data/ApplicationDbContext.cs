using Microsoft.EntityFrameworkCore;
using Task_6.Models;

namespace Task_6.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ArticleStat> ArticleStats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().ToTable("articles");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Author>().ToTable("authors");
            modelBuilder.Entity<ArticleStat>().ToTable("article_stats");

            base.OnModelCreating(modelBuilder);
        }
    }
}