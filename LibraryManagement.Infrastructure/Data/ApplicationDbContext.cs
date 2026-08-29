using LibraryManagement.Domain.Entities;
using LibraryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }
        public DbSet<BookReview> BookReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Author).IsRequired().HasMaxLength(100);
                entity.Property(b => b.ISBN).IsRequired().HasMaxLength(20);
                entity.Property(b => b.Publisher).HasMaxLength(100);
            });

            modelBuilder.Entity<BookLoan>(entity =>
            {
                entity.HasOne<ApplicationUser>()
                       .WithMany(u => u.BookLoans)
                       .HasForeignKey(bl => bl.UserId)
                       .OnDelete(DeleteBehavior.Restrict);

                // One blocking loan per book at a time
                entity.HasIndex(l => l.BookId)
                       .IsUnique()
                       .HasFilter($"[Status] IN ({(int)LoanStatus.Reserved}, {(int)LoanStatus.Active}, {(int)LoanStatus.Overdue})")
                       .HasDatabaseName("IX_BookLoans_BookId_Active");
            });

            modelBuilder.Entity<BookReview>(entity =>
            {
                entity.HasOne<ApplicationUser>()
                       .WithMany()
                       .HasForeignKey(br => br.UserId)
                       .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(br => new { br.BookId, br.UserId })
                      .IsUnique()
                      .HasDatabaseName("IX_BookReviews_BookId_UserId");
            });
        }
    }
}
