using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEfContains
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
        {

        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>(entity => {
                entity.HasKey(e => e.StockId)
                .HasName("pk_test");

                entity.ToTable("test");

                entity.Property(e => e.StockId)
                .HasColumnName("stockId")
                .HasMaxLength(6)
                .IsRequired()
                .IsUnicode(false);

                entity.Property(e => e.StockName)
                .HasColumnName("stockName")
                .HasMaxLength(48)
                .IsRequired()
                .IsUnicode();
            });
        }
    }
}
