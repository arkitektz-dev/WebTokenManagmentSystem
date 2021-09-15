using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebTokenManagmentSystem.Models
{
    public partial class WebTokenManagmentSystemDBContext : DbContext
    {
        public WebTokenManagmentSystemDBContext()
        {
        }

        public WebTokenManagmentSystemDBContext(DbContextOptions<WebTokenManagmentSystemDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppSetting> AppSettings { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<CounterHistory> CounterHistories { get; set; }
        public virtual DbSet<CounterServiceRelation> CounterServiceRelations { get; set; }
        public virtual DbSet<CounterTokenRelation> CounterTokenRelations { get; set; }
        public virtual DbSet<CounterType> CounterTypes { get; set; }
        public virtual DbSet<QueueHistory> QueueHistories { get; set; }
        public virtual DbSet<ServiceMaster> ServiceMasters { get; set; }
        public virtual DbSet<ServiceOption> ServiceOptions { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<TokenStatusHistory> TokenStatusHistories { get; set; }
        public virtual DbSet<UserCounterHistory> UserCounterHistories { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-QM4LMEI;Initial Catalog=WebTokenManagmentSystemDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AppSetting>(entity =>
            {
                entity.ToTable("AppSetting");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SettingKey).HasColumnName("setting_key");

                entity.Property(e => e.SettingValue).HasColumnName("setting_value");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Csrid).HasColumnName("CSRID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CounterHistory>(entity =>
            {
                entity.ToTable("CounterHistory");

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.Property(e => e.Login).HasColumnType("datetime");

                entity.Property(e => e.Logout).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CounterServiceRelation>(entity =>
            {
                entity.ToTable("CounterServiceRelation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CounterTypeId).HasColumnName("CounterTypeID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ServiceMasterId).HasColumnName("ServiceMasterID");

                entity.HasOne(d => d.CounterType)
                    .WithMany(p => p.CounterServiceRelations)
                    .HasForeignKey(d => d.CounterTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CounterServiceRelation_CounterType");

                entity.HasOne(d => d.ServiceMaster)
                    .WithMany(p => p.CounterServiceRelations)
                    .HasForeignKey(d => d.ServiceMasterId)
                    .HasConstraintName("FK_CounterServiceRelation_ServiceMaster1");
            });

            modelBuilder.Entity<CounterTokenRelation>(entity =>
            {
                entity.ToTable("CounterTokenRelation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TokenId).HasColumnName("TokenID");

                entity.HasOne(d => d.Counter)
                    .WithMany(p => p.CounterTokenRelations)
                    .HasForeignKey(d => d.CounterId)
                    .HasConstraintName("FK_CounterTokenRelation_Counters");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CounterTokenRelations)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_CounterTokenRelation_Status");

                entity.HasOne(d => d.Token)
                    .WithMany(p => p.CounterTokenRelations)
                    .HasForeignKey(d => d.TokenId)
                    .HasConstraintName("FK_CounterTokenRelation_Token");
            });

            modelBuilder.Entity<CounterType>(entity =>
            {
                entity.ToTable("CounterType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CounterName).HasMaxLength(50);
            });

            modelBuilder.Entity<QueueHistory>(entity =>
            {
                entity.ToTable("QueueHistory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsPlayed).HasColumnName("isPlayed");
            });

            modelBuilder.Entity<ServiceMaster>(entity =>
            {
                entity.ToTable("ServiceMaster");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ServiceName).HasMaxLength(50);
            });

            modelBuilder.Entity<ServiceOption>(entity =>
            {
                entity.ToTable("ServiceOption");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ServiceMasterId).HasColumnName("ServiceMasterID");

                entity.HasOne(d => d.ServiceMaster)
                    .WithMany(p => p.ServiceOptions)
                    .HasForeignKey(d => d.ServiceMasterId)
                    .HasConstraintName("FK_ServiceOption_ServiceMaster");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("Token");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CompleteDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomTokenNumber)
                    .HasMaxLength(250)
                    .HasColumnName("Custom_Token_Number");

                entity.Property(e => e.IsCustomer).HasColumnName("isCustomer");

                entity.Property(e => e.ServiceOptionId).HasColumnName("ServiceOptionID");

                entity.HasOne(d => d.ServiceOption)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.ServiceOptionId)
                    .HasConstraintName("FK_Token_ServiceOption");
            });

            modelBuilder.Entity<TokenStatusHistory>(entity =>
            {
                entity.ToTable("Token_Status_History");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_Date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TokenId).HasColumnName("Token_ID");

                entity.HasOne(d => d.Token)
                    .WithMany(p => p.TokenStatusHistories)
                    .HasForeignKey(d => d.TokenId)
                    .HasConstraintName("FK_Token_Status_History_Token_Status_History");
            });

            modelBuilder.Entity<UserCounterHistory>(entity =>
            {
                entity.ToTable("UserCounterHistory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CounterId).HasColumnName("CounterID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.ToTable("UserDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.ToTable("UserToken");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TokenId).HasColumnName("TokenID");

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Token)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.TokenId)
                    .HasConstraintName("FK_UserToken_Token");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserToken_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
