using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SmartProtocol.Models
{
    public partial class DB_A57E75_chamucolol87Context : DbContext
    {
        public DB_A57E75_chamucolol87Context()
        {
        }

        public DB_A57E75_chamucolol87Context(DbContextOptions<DB_A57E75_chamucolol87Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<AlertType> AlertType { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Flow> Flow { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Step> Step { get; set; }
        public virtual DbSet<StepAlert> StepAlert { get; set; }
        public virtual DbSet<StepType> StepType { get; set; }
        public virtual DbSet<Telephone> Telephone { get; set; }
        public virtual DbSet<TelephoneType> TelephoneType { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SQL5053.site4now.net;Initial Catalog=DB_A57E75_chamucolol87;User Id=DB_A57E75_chamucolol87_admin;Password=lomas123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address", "Protocol");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.AddressTypeId)
                    .HasConstraintName("FK__Address__Address__42E1EEFE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Address__UserId__43D61337");
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("AddressType", "Protocol");

                entity.Property(e => e.AddressTypeDescription)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.AddressTypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AlertType>(entity =>
            {
                entity.ToTable("AlertType", "Protocol");

                entity.Property(e => e.AlertTypeDescription)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.AlertTypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("Email", "Protocol");

                entity.HasIndex(e => e.EmailAddress)
                    .HasName("UQ__Email__49A14740B1F317A5")
                    .IsUnique();

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Email)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Email__UserId__44CA3770");
            });

            modelBuilder.Entity<Flow>(entity =>
            {
                entity.ToTable("Flow", "Protocol");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FlowName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Flow)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Flow__UserId__45BE5BA9");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login", "Protocol");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Email)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.EmailId)
                    .HasConstraintName("FK__Login__EmailId__46B27FE2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Login__UserId__47A6A41B");
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.ToTable("Step", "Protocol");

                entity.Property(e => e.ClaimedBy).HasColumnType("datetime");

                entity.Property(e => e.StepCompletedOn).HasColumnType("datetime");

                entity.Property(e => e.StepName)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.StepObject)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.StepStartedOn).HasColumnType("datetime");

                entity.Property(e => e.StepStatus)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TriggerDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Flow)
                    .WithMany(p => p.Step)
                    .HasForeignKey(d => d.FlowId)
                    .HasConstraintName("FK__Step__FlowId__489AC854");

                entity.HasOne(d => d.StepType)
                    .WithMany(p => p.Step)
                    .HasForeignKey(d => d.StepTypeId)
                    .HasConstraintName("FK__Step__StepTypeId__498EEC8D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Step)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Step__UserId__4A8310C6");
            });

            modelBuilder.Entity<StepAlert>(entity =>
            {
                entity.ToTable("StepAlert", "Protocol");

                entity.Property(e => e.StepAlertStatus)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.StepCompletedOn).HasColumnType("datetime");

                entity.Property(e => e.StepStartedOn).HasColumnType("datetime");

                entity.HasOne(d => d.AlertType)
                    .WithMany(p => p.StepAlert)
                    .HasForeignKey(d => d.AlertTypeId)
                    .HasConstraintName("FK__StepAlert__Alert__4B7734FF");

                entity.HasOne(d => d.Step)
                    .WithMany(p => p.StepAlert)
                    .HasForeignKey(d => d.StepId)
                    .HasConstraintName("FK__StepAlert__StepI__4C6B5938");
            });

            modelBuilder.Entity<StepType>(entity =>
            {
                entity.ToTable("StepType", "Protocol");

                entity.Property(e => e.StepTypeDescription)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.StepTypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Telephone>(entity =>
            {
                entity.ToTable("Telephone", "Protocol");

                entity.Property(e => e.TelephoneNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.TelephoneType)
                    .WithMany(p => p.Telephone)
                    .HasForeignKey(d => d.TelephoneTypeId)
                    .HasConstraintName("FK__Telephone__Telep__4D5F7D71");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Telephone)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Telephone__UserI__4E53A1AA");
            });

            modelBuilder.Entity<TelephoneType>(entity =>
            {
                entity.ToTable("TelephoneType", "Protocol");

                entity.Property(e => e.TelephoneTypeDescription)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.TelephoneTypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Protocol");

                entity.Property(e => e.First)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Last)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
