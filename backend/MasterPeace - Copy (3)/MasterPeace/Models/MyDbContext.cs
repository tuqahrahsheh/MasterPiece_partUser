using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MasterPeace.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<ContactMessage> ContactMessages { get; set; }

    public virtual DbSet<DoctorRequest> DoctorRequests { get; set; }

    public virtual DbSet<HomeDoctor> HomeDoctors { get; set; }

    public virtual DbSet<HomeVisit> HomeVisits { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<MedicalSuppliesRequest> MedicalSuppliesRequests { get; set; }

    public virtual DbSet<MedicationDelivery> MedicationDeliveries { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PaymentRequest> PaymentRequests { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<Update> Updates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-0SF6ESM;Database=MasterPro;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ_Admins_Email").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_Appointments_Service");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Appointments_Users");
        });

        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId);

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.DateSent).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Subject).HasMaxLength(200);
        });

        modelBuilder.Entity<DoctorRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.ContactEmail).HasMaxLength(256);
            entity.Property(e => e.DoctorImage).IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.Specialty).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<HomeDoctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__HomeDoct__2DC00EDF41378496");

            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Specialty).HasMaxLength(100);
        });

        modelBuilder.Entity<HomeVisit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PK__HomeVisi__4D3AA1BE6AB218D5");

            entity.Property(e => e.VisitId).HasColumnName("VisitID");
            entity.Property(e => e.DiscountAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.ServiceFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Doctor).WithMany(p => p.HomeVisits)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__HomeVisit__Docto__07C12930");

            entity.HasOne(d => d.User).WithMany(p => p.HomeVisits)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__HomeVisit__UserI__06CD04F7");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__Hospital__38C2E58F031AA9A0");

            entity.Property(e => e.HospitalId).HasColumnName("HospitalID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.HospitalName).HasMaxLength(150);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.WorkingHours).HasMaxLength(100);
        });

        modelBuilder.Entity<MedicalSuppliesRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.DeliveryTime).HasColumnType("datetime");
            entity.Property(e => e.MedicationName).HasMaxLength(256);
            entity.Property(e => e.PrescriptionFilePath).HasMaxLength(256);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserName).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.MedicalSuppliesRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_MedicalSuppliesRequests_Users");
        });

        modelBuilder.Entity<MedicationDelivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("PK__Medicati__626D8FEE09A12D4A");

            entity.ToTable("MedicationDelivery");

            entity.Property(e => e.DeliveryId).HasColumnName("DeliveryID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.DeliveryFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.MedicationDeliveries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Medicatio__UserI__0C85DE4D");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Request).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK_Orders_MedicalSuppliesRequests");
        });

        modelBuilder.Entity<PaymentRequest>(entity =>
        {
            entity.HasKey(e => e.PaymentRequestId).HasName("PK__PaymentR__9738488E255421BE");

            entity.Property(e => e.PaymentRequestId).HasColumnName("PaymentRequestID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CardNumber).HasMaxLength(16);
            entity.Property(e => e.CardholderName).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Cvv)
                .HasMaxLength(3)
                .HasColumnName("CVV");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.ExpiryMonth).HasMaxLength(2);
            entity.Property(e => e.ExpiryYear).HasMaxLength(4);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.ServiceType).HasMaxLength(50);
            entity.Property(e => e.SubscriptionType).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ZipCode).HasMaxLength(20);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.ServiceName).HasMaxLength(200);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__9A2B24BD0502D171");

            entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.SubscriptionType).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Subscript__UserI__10566F31");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Testimon__3213E83F7D6B5886");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Firstname).HasMaxLength(255);
            entity.Property(e => e.Isaccepted)
                .HasDefaultValue(false)
                .HasColumnName("isaccepted");
            entity.Property(e => e.Lastname)
                .HasMaxLength(255)
                .HasColumnName("lastname");
            entity.Property(e => e.TheTestimonials)
                .HasMaxLength(500)
                .HasColumnName("theTestimonials");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Testimoni__UserI__1AD3FDA4");
        });

        modelBuilder.Entity<Update>(entity =>
        {
            entity.Property(e => e.UpdateId).HasColumnName("UpdateID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
