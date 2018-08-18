using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class MagacinContext : DbContext
    {
        public virtual DbSet<Kategorija> Kategorijas { get; set; }
        public virtual DbSet<Proizvod> Proizvods { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategorija>()
                .HasMany(e => e.Proizvods)
                .WithRequired(e => e.Kategorije)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Proizvod>()
                .Property(e => e.Cena)
                .HasPrecision(12, 3);
        }
    }
}