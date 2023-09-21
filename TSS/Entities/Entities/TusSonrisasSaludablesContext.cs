using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Entities
{
    public partial class TusSonrisasSaludablesContext : DbContext
    {
        public TusSonrisasSaludablesContext()
        {
        }

        public TusSonrisasSaludablesContext(DbContextOptions<TusSonrisasSaludablesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<ClinicProcedure> ClinicProcedures { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<IdentificationType> IdentificationTypes { get; set; } = null!;
        public virtual DbSet<PatientRecord> PatientRecords { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;
        public virtual DbSet<ShoppingDetail> ShoppingDetails { get; set; } = null!;
        public virtual DbSet<Specialty> Specialties { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<VwAppointment> VwAppointments { get; set; } = null!;
        public virtual DbSet<VwBrand> VwBrands { get; set; } = null!;
        public virtual DbSet<VwCategory> VwCategories { get; set; } = null!;
        public virtual DbSet<VwGenre> VwGenres { get; set; } = null!;
        public virtual DbSet<VwIdentification> VwIdentifications { get; set; } = null!;
        public virtual DbSet<VwProduct> VwProducts { get; set; } = null!;
        public virtual DbSet<VwProvince> VwProvinces { get; set; } = null!;
        public virtual DbSet<VwRole> VwRoles { get; set; } = null!;
        public virtual DbSet<VwSubCategory> VwSubCategories { get; set; } = null!;
        public virtual DbSet<VwUser> VwUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasIndex(e => e.StartTime, "UQ__Appointm__18D8086C0E3445A0")
                    .IsUnique();

                entity.HasIndex(e => e.EndTime, "UQ__Appointm__C7752F0EFD65BD93")
                    .IsUnique();

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__Appointme__Docto__6D0D32F4");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__Speci__6E01572D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Appointme__UserI__6C190EBB");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasIndex(e => e.BrandName, "UQ__Brands__2206CE9B397F1DD0")
                    .IsUnique();

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0E5047FAB")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MainCategoryId).HasColumnName("MainCategoryID");

                entity.HasOne(d => d.MainCategory)
                    .WithMany(p => p.InverseMainCategory)
                    .HasForeignKey(d => d.MainCategoryId)
                    .HasConstraintName("FK__Categorie__MainC__5DCAEF64");
            });

            modelBuilder.Entity<ClinicProcedure>(entity =>
            {
                entity.HasKey(e => e.ProcedureId)
                    .HasName("PK__ClinicPr__54C2E50D7C275BAE");

                entity.HasIndex(e => e.ProcedureName, "UQ__ClinicPr__3950E7D42C9F6EBE")
                    .IsUnique();

                entity.Property(e => e.ProcedureId).HasColumnName("ProcedureID");

                entity.Property(e => e.ProcedureName).HasMaxLength(100);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasIndex(e => e.Idnumber, "UQ__Doctors__564DB08A23352A3A")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNumber, "UQ__Doctors__85FB4E3827D57367")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Doctors__A9D105340E903957")
                    .IsUnique();

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.DoctorName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorPhoto).HasColumnType("image");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Idnumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("IDNumber");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctors__GenreID__5629CD9C");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctors__Special__5535A963");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctors__TypeID__5441852A");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.GenreName, "UQ__Genres__BBE1C33924593AD6")
                    .IsUnique();

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IdentificationType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__Identifi__516F039597B249A4");

                entity.HasIndex(e => e.Idtype, "UQ__Identifi__B27333983ED3EC77")
                    .IsUnique();

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Idtype)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IDType");
            });

            modelBuilder.Entity<PatientRecord>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__PatientR__FBDF78C9F95126E1");

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.Diagnoses)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.ProcedureId).HasColumnName("ProcedureID");

                entity.Property(e => e.RecordDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Symptoms)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.Treatment)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.PatientRecords)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientRe__Docto__66603565");

                entity.HasOne(d => d.Procedure)
                    .WithMany(p => p.PatientRecords)
                    .HasForeignKey(d => d.ProcedureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientRe__Proce__6754599E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PatientRecords)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientRe__UserI__656C112C");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__BrandI__60A75C0F");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Catego__619B8048");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasIndex(e => e.ProvinceName, "UQ__Province__B27F23726CCC7186")
                    .IsUnique();

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.ProvinceName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleType, "UQ__Roles__5D0A2E94B438F86F")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("PK__Shopping__51BCD7972761CE5F");

                entity.Property(e => e.CartId)
                    .ValueGeneratedNever()
                    .HasColumnName("CartID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ShoppingC__UserI__71D1E811");
            });

            modelBuilder.Entity<ShoppingDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PK__Shopping__135C314DC356FA27");

                entity.Property(e => e.DetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("DetailID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Taxes).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.ShoppingDetails)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__ShoppingD__CartI__74AE54BC");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ShoppingD__Produ__75A278F5");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasIndex(e => e.SpecialtyName, "UQ__Specialt__7DCA57482C6C7384")
                    .IsUnique();

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.SpecialtyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Idnumber, "UQ__Users__564DB08AE34BB20A")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D1053417837744")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.Idnumber)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("IDNumber");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.UserAddress)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__GenreID__46E78A0C");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__ProvinceI__47DBAE45");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__RoleID__44FF419A");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__TypeID__45F365D3");
            });

            modelBuilder.Entity<VwAppointment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Appointments");

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.Doctor)
                    .HasMaxLength(82)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.SpecialtyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwBrand>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Brands");

                entity.Property(e => e.BrandId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BrandID");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Categories");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwGenre>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Genres");

                entity.Property(e => e.GenreId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GenreID");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwIdentification>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Identifications");

                entity.Property(e => e.Idtype)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IDType");

                entity.Property(e => e.TypeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TypeID");
            });

            modelBuilder.Entity<VwProduct>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Products");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice)
                    .HasMaxLength(42)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwProvince>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Provinces");

                entity.Property(e => e.ProvinceId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ProvinceID");

                entity.Property(e => e.ProvinceName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Roles");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RoleID");

                entity.Property(e => e.RoleType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSubCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_SubCategories");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.MainCategory)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategory)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Users");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(302)
                    .IsUnicode(false)
                    .HasColumnName("Full Name");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Idnumber)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("IDNumber");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
