using System;
using System.Data.Entity;

namespace QRJ.Models
{
    public class QRCodeContext : DbContext
    {
        public QRCodeContext() : base("QRJ") 
        {
        }
        public DbSet<QRCode> QRCodes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryContent> CategoryContents { get; set; }
        public DbSet<Horoscope> Horoscopes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryContent>()
            .HasRequired(t => t.Category)
            .WithMany(t => t.Contents)
            .HasForeignKey(d => d.CategoryId)
            .WillCascadeOnDelete(true);
        }
    }
}