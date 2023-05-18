using Microsoft.EntityFrameworkCore;
namespace BookStore.Models
{
    public class BookDbContext:DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {

        }
        public BookDbContext()
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookAuthors> BookAuthors{ get;set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookAuthors>(x =>
            {
            x.HasKey(k => new { k.BookId, k.AuthorId });
                x.HasOne(x => x.Author).WithMany(a => a.BookAuthors).HasForeignKey(a => a.AuthorId);
                x.HasOne(x => x.Book).WithMany(a => a.BookAuthors).HasForeignKey(a => a.BookId);
            });

                
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
