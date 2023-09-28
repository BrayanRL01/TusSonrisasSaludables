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
        public virtual DbSet<UsersTrigger> UsersTriggers { get; set; } = null!;
        public virtual DbSet<VwAdminAppointment> VwAdminAppointments { get; set; } = null!;
        public virtual DbSet<VwAppointment> VwAppointments { get; set; } = null!;
        public virtual DbSet<VwBrand> VwBrands { get; set; } = null!;
        public virtual DbSet<VwCategory> VwCategories { get; set; } = null!;
        public virtual DbSet<VwDoctor> VwDoctors { get; set; } = null!;
        public virtual DbSet<VwGenre> VwGenres { get; set; } = null!;
        public virtual DbSet<VwIdentification> VwIdentifications { get; set; } = null!;
        public virtual DbSet<VwProduct> VwProducts { get; set; } = null!;
        public virtual DbSet<VwProvince> VwProvinces { get; set; } = null!;
        public virtual DbSet<VwRole> VwRoles { get; set; } = null!;
        public virtual DbSet<VwSpecialty> VwSpecialties { get; set; } = null!;
        public virtual DbSet<VwSubCategory> VwSubCategories { get; set; } = null!;
        public virtual DbSet<VwUser> VwUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasIndex(e => e.StartTime, "UQ__Appointm__18D8086C6DB2B8C8")
                    .IsUnique();

                entity.HasIndex(e => e.EndTime, "UQ__Appointm__C7752F0EFFD0E58F")
                    .IsUnique();

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__Appointme__Docto__656C112C");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__Speci__66603565");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Appointme__UserI__6477ECF3");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasIndex(e => e.BrandName, "UQ__Brands__2206CE9B8E1273B3")
                    .IsUnique();

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E0961CF175")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MainCategoryId).HasColumnName("MainCategoryID");
            });

            modelBuilder.Entity<ClinicProcedure>(entity =>
            {
                entity.HasKey(e => e.ProcedureId)
                    .HasName("PK__ClinicPr__54C2E50DF237BD03");

                entity.HasIndex(e => e.ProcedureName, "UQ__ClinicPr__3950E7D4CCFF5F2C")
                    .IsUnique();

                entity.Property(e => e.ProcedureId).HasColumnName("ProcedureID");

                entity.Property(e => e.ProcedureName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasIndex(e => e.IdNumber, "UQ__Doctors__62DF803316E7C809")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNumber, "UQ__Doctors__85FB4E381A6C2DDB")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Doctors__A9D10534A7CD84FE")
                    .IsUnique();

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

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

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

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
                    .HasConstraintName("FK__Doctors__GenreID__5BE2A6F2");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.SpecialtyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctors__Special__5CD6CB2B");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Doctors__TypeID__5AEE82B9");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasIndex(e => e.GenreName, "UQ__Genres__BBE1C339A13BEBDB")
                    .IsUnique();

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IdentificationType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__Identifi__516F0395527994C8");

                entity.HasIndex(e => e.IdType, "UQ__Identifi__9A39EABD7194241E")
                    .IsUnique();

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.IdType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PatientRecord>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__PatientR__FBDF78C9651B64FB");

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
                    .HasConstraintName("FK__PatientRe__Docto__72C60C4A");

                entity.HasOne(d => d.Procedure)
                    .WithMany(p => p.PatientRecords)
                    .HasForeignKey(d => d.ProcedureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientRe__Proce__73BA3083");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PatientRecords)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientRe__UserI__71D1E811");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ProductName, "UQ__Products__DD5A978A24163AEB")
                    .IsUnique();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ProductImage).HasColumnType("image");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__BrandI__6A30C649");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Products__Catego__6B24EA82");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasIndex(e => e.ProvinceName, "UQ__Province__B27F2372E1AD9E7E")
                    .IsUnique();

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.ProvinceName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleType, "UQ__Roles__5D0A2E9492121FD2")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleType)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("PK__Shopping__51BCD79798AC9E56");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__ShoppingC__UserI__778AC167");
            });

            modelBuilder.Entity<ShoppingDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId)
                    .HasName("PK__Shopping__135C314D6E6AAB90");

                entity.Property(e => e.DetailId).HasColumnName("DetailID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Taxes).HasColumnType("decimal(3, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.ShoppingDetails)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK__ShoppingD__CartI__7A672E12");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ShoppingD__Produ__7B5B524B");
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.HasIndex(e => e.SpecialtyName, "UQ__Specialt__7DCA5748BB17E814")
                    .IsUnique();

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.SpecialtyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.IdNumber, "UQ__Users__62DF8033D880C028")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E38B0986906")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534FB794443")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GenreId).HasColumnName("GenreID");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false);

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
                    .HasConstraintName("FK__Users__GenreID__48CFD27E");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__ProvinceI__49C3F6B7");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__RoleID__46E78A0C");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__TypeID__47DBAE45");
            });

            modelBuilder.Entity<UsersTrigger>(entity =>
            {
                entity.HasKey(e => e.TriggerId)
                    .HasName("PK__UsersTri__11321F0299E136B8");

                entity.Property(e => e.TriggerId).HasColumnName("TriggerID");

                entity.Property(e => e.TriggerDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TriggerReason)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserData)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAdminAppointment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_AdminAppointments");

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.Doctor)
                    .HasMaxLength(82)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.PacientName)
                    .HasMaxLength(302)
                    .IsUnicode(false)
                    .HasColumnName("Pacient Name");

                entity.Property(e => e.SpecialtyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");
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

            modelBuilder.Entity<VwDoctor>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Doctors");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.DoctorPhoto).HasColumnType("image");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(82)
                    .IsUnicode(false)
                    .HasColumnName("Full Name");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialtyName)
                    .HasMaxLength(50)
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

                entity.Property(e => e.IdType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

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

                entity.Property(e => e.ProductImage).HasColumnType("image");

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

            modelBuilder.Entity<VwSpecialty>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Specialties");

                entity.Property(e => e.SpecialtyId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SpecialtyID");

                entity.Property(e => e.SpecialtyName)
                    .HasMaxLength(50)
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
