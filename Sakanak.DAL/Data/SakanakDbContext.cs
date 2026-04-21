using Microsoft.EntityFrameworkCore;
using Sakanak.Domain.Entities;

namespace Sakanak.DAL.Data;

public class SakanakDbContext : DbContext
{
    public SakanakDbContext(DbContextOptions<SakanakDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SakanakDBV6;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
    }
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Landlord> Landlords => Set<Landlord>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Apartment> Apartments => Set<Apartment>();
    public DbSet<ApartmentGroup> ApartmentGroups => Set<ApartmentGroup>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<LifestyleQuestionnaire> LifestyleQuestionnaires => Set<LifestyleQuestionnaire>();
    public DbSet<Media> Media => Set<Media>();
    public DbSet<Request> Requests => Set<Request>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().UseTpcMappingStrategy();
        modelBuilder.HasSequence<int>("UserIdSeq").StartsAt(1).IncrementsBy(1);

        modelBuilder.Entity<Student>().Property(e => e.UserId).HasDefaultValueSql("NEXT VALUE FOR UserIdSeq");
        modelBuilder.Entity<Landlord>().Property(e => e.UserId).HasDefaultValueSql("NEXT VALUE FOR UserIdSeq");
        modelBuilder.Entity<Admin>().Property(e => e.UserId).HasDefaultValueSql("NEXT VALUE FOR UserIdSeq");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SakanakDbContext).Assembly);
    }
}
