using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RPGEnemies.Models
{
    public partial class RPGEnemiesContext : DbContext
    {
        public RPGEnemiesContext()
        {
        }

        public RPGEnemiesContext(DbContextOptions<RPGEnemiesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StatBlocks> StatBlocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-645J246E;Database=RPGEnemies;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<StatBlocks>(entity =>
            {
                entity.HasKey(e => e.CharacterName)
                    .HasName("PK__StatBloc__7878E19D4083547A");

                entity.Property(e => e.CharacterName)
                    .HasColumnName("characterName")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Abilities)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Skills)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Talents)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Weapons)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });
        }

        
    }
}
