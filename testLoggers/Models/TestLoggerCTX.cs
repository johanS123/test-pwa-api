using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace testLoggers.Models
{
    public partial class TestLoggerCTX : DbContext
    {
        public TestLoggerCTX()
        {

        }

        public TestLoggerCTX(DbContextOptions<TestLoggerCTX> options) : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Audits> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.id)
                    .HasName("PK_usuarios");

                entity.Property(e => e.firstname).IsUnicode(false);

                entity.Property(e => e.lastname).IsUnicode(false);

                entity.Property(e => e.email).IsUnicode(false);

                entity.Property(e => e.password).IsUnicode(false);

                entity.Property(e => e.role).IsUnicode(false);
            });


            modelBuilder.Entity<Audits>(entity =>
            {
                entity.HasKey(e => new { e.idUser })
                    .HasName("PK__audits__3213E83F06764578");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Audits)
                    .HasForeignKey(d => d.idUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__audits__idUser__36B12243");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
