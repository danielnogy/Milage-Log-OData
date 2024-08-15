using ArhiParcurs.Shared;
using ArhiParcurs.Shared.Models;
using ArhiParcurs.Shared.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ArhiParcurs.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor _httpContextAccessor) : IdentityDbContext<ApplicationUser>(options)
{
    public virtual DbSet<ConsumptionIncrease> ConsumptionIncreases { get; set; }
    public virtual DbSet<Fuel> Fuels { get; set; }
    public virtual DbSet<Partner> Partners { get; set; }
    public virtual DbSet<SheetRoute> SheetRoutes { get; set; }
    public virtual DbSet<Vehicle> Vehicles { get; set; }
    public virtual DbSet<Sheet> Sheets { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Provider> Providers { get; set; }
    public virtual DbSet<Refuel> Refuels { get; set; }
    public virtual DbSet<Setting> Settings { get; set; }
    public virtual DbSet<Branch> Branches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var entityAssembly = typeof(BaseDomain).Assembly;
        // Get all entity types in the assembly
        var entityTypes = entityAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseDomain)));

        // Configure each entity type
        foreach (var entityType in entityTypes)
        {
            var entity = Activator.CreateInstance(entityType);
            var entityBuilder = modelBuilder.Entity(entityType);

            // Set default values for properties
            foreach (var property in entityType.GetProperties())
            {
                // Example: set default value for string properties
                if (property.PropertyType == typeof(string))
                {
                    entityBuilder.Property(property.Name).HasDefaultValue("");
                }
                else if (property.PropertyType == typeof(decimal))
                {
                    entityBuilder.Property(property.Name).HasDefaultValue(0);
                }
                else if (property.PropertyType == typeof(bool))
                {
                    entityBuilder.Property(property.Name).HasDefaultValue(false);
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    entityBuilder.Property(property.Name).HasDefaultValue(new DateTime(1900, 1, 1));
                }
            }
        }

        modelBuilder.Entity<Sheet>()
            .HasOne(x => x.ConsumptionIncrease)
            .WithMany(x => x.Sheets)
            .HasForeignKey(x => x.ConsumptionIncreaseId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Sheet>()
            .HasOne(x => x.Project)
            .WithMany(x => x.Sheets)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<SheetRoute>()
            .HasOne(x => x.Sheet)
            .WithMany(x => x.SheetRoutes)
            .HasForeignKey(x => x.SheetId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Sheet>()
            .HasOne(x => x.Driver)
            .WithMany()
            .HasForeignKey(x => x.DriverId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Sheet>()
            .HasOne(x => x.Creator)
            .WithMany()
            .HasForeignKey(x => x.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Sheet>()
            .HasOne(x => x.Vehicle)
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Vehicle>()
            .HasOne(x => x.Fuel)
            .WithMany(x=>x.Vehicles)
            .HasForeignKey(x => x.FuelId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<SheetRoute>()
            .HasOne(x => x.ConsumptionIncrease)
            .WithMany()
            .HasForeignKey(x => x.ConsumptionIncreaseId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Refuel>()
            .HasOne(x => x.Provider)
            .WithMany(x=>x.Refuels)
            .HasForeignKey(x => x.ProviderId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<Refuel>()
            .HasOne(x => x.Fuel)
            .WithMany()
            .HasForeignKey(x => x.FuelId)
            .OnDelete(DeleteBehavior.NoAction);

         modelBuilder.Entity<Refuel>()
            .HasOne(x => x.Sheet)
            .WithMany(x=>x.RefuelsList)
            .HasForeignKey(x => x.SheetId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Vehicle>()
            .HasOne(x => x.Branch)
            .WithMany(x=>x.Vehicles)
            .HasForeignKey(x => x.BranchId)
            .OnDelete(DeleteBehavior.NoAction);

        
        //modelBuilder.Entity<SheetRoute>().Property(x => x.RouteType).HasConversion(new EnumToStringConverter<RouteTypeEnum>());
        //modelBuilder.Entity<Vehicle>().Property(x => x.VehicleState).HasConversion(new EnumToStringConverter<VehicleStateEnum>());
        //modelBuilder.Entity<Partner>().Property(x => x.PartnerState).HasConversion(new EnumToStringConverter<PartnerStateEnum>());
        //modelBuilder.Entity<FazAuto>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_auto_PK");

        //    entity.ToTable("faz_auto");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Apar).HasColumnName("APAR");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Ca)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CA");
        //    entity.Property(e => e.Cb)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CB");
        //    entity.Property(e => e.Cf)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CF");
        //    entity.Property(e => e.Cm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CM");
        //    entity.Property(e => e.Formula).HasColumnName("FORMULA");
        //    entity.Property(e => e.Iaerod).HasColumnName("IAEROD");
        //    entity.Property(e => e.Ida).HasColumnName("IDA");
        //    entity.Property(e => e.Ispec).HasColumnName("ISPEC");
        //    entity.Property(e => e.Kmechiv).HasColumnName("KMECHIV");
        //    entity.Property(e => e.Kmef).HasColumnName("KMEF");
        //    entity.Property(e => e.Kmefinc).HasColumnName("KMEFINC");
        //    entity.Property(e => e.Marca)
        //        .HasMaxLength(30)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("MARCA");
        //    entity.Property(e => e.Motor).HasColumnName("MOTOR");
        //    entity.Property(e => e.Nrauto)
        //        .HasMaxLength(15)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NRAUTO");
        //    entity.Property(e => e.Rap).HasColumnName("RAP");
        //    entity.Property(e => e.Restrez)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("RESTREZ");
        //});

        //modelBuilder.Entity<FazCoefkg>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_coefkg_PK");

        //    entity.ToTable("faz_coefkg");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Autor)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("AUTOR");
        //    entity.Property(e => e.Idk).HasColumnName("IDK");
        //    entity.Property(e => e.Motor1fr)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("MOTOR1FR");
        //    entity.Property(e => e.Motor1r)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("MOTOR1R");
        //    entity.Property(e => e.Motor23fr)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("MOTOR23FR");
        //    entity.Property(e => e.Motor23r)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("MOTOR23R");
        //    entity.Property(e => e.Tone)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("TONE");
        //});

        //modelBuilder.Entity<FazConsum>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_consum_PK");

        //    entity.ToTable("faz_consum");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Cm1)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CM1");
        //    entity.Property(e => e.Cm2)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CM2");
        //    entity.Property(e => e.Cm3)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CM3");
        //    entity.Property(e => e.Ida).HasColumnName("IDA");
        //    entity.Property(e => e.Idcm).HasColumnName("IDCM");
        //    entity.Property(e => e.Implicit).HasColumnName("IMPLICIT");
        //});

        //modelBuilder.Entity<FazConsumcm1>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_consumcm1_PK");

        //    entity.ToTable("faz_consumcm1");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aprind).HasColumnName("APRIND");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Capcil).HasColumnName("CAPCIL");
        //    entity.Property(e => e.Cm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CM");
        //    entity.Property(e => e.Fel)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("FEL");
        //    entity.Property(e => e.Idcm1).HasColumnName("IDCM1");
        //    entity.Property(e => e.Marca)
        //        .HasMaxLength(30)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("MARCA");
        //    entity.Property(e => e.Sarcina)
        //        .HasMaxLength(10)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("SARCINA");
        //});

        //modelBuilder.Entity<FazConsumcm2>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_consumcm2_PK");

        //    entity.ToTable("faz_consumcm2");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aprind).HasColumnName("APRIND");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Capcil).HasColumnName("CAPCIL");
        //    entity.Property(e => e.Cm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CM");
        //    entity.Property(e => e.Fel)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("FEL");
        //    entity.Property(e => e.Idcm2).HasColumnName("IDCM2");
        //    entity.Property(e => e.Locuri)
        //        .HasMaxLength(10)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("LOCURI");
        //    entity.Property(e => e.Marca)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("MARCA");
        //});

        //modelBuilder.Entity<FazConsumcm3>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_consumcm3_PK");

        //    entity.ToTable("faz_consumcm3");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aprind).HasColumnName("APRIND");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Capcil).HasColumnName("CAPCIL");
        //    entity.Property(e => e.Cm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CM");
        //    entity.Property(e => e.Fel)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("FEL");
        //    entity.Property(e => e.Idcm3).HasColumnName("IDCM3");
        //    entity.Property(e => e.Loc)
        //        .HasMaxLength(10)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("LOC");
        //    entity.Property(e => e.Marca)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("MARCA");
        //});

        //modelBuilder.Entity<FazConsumcm4>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_consumcm4_PK");

        //    entity.ToTable("faz_consumcm4");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aprind).HasColumnName("APRIND");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Capcil).HasColumnName("CAPCIL");
        //    entity.Property(e => e.Cm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CM");
        //    entity.Property(e => e.Fel)
        //        .HasMaxLength(100)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("FEL");
        //    entity.Property(e => e.Idcm4).HasColumnName("IDCM4");
        //    entity.Property(e => e.Marca)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("MARCA");
        //});

        //modelBuilder.Entity<FazCurse>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_curse_PK");

        //    entity.ToTable("faz_curse");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.A).HasColumnType("numeric(8, 2)");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Cn)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CN");
        //    entity.Property(e => e.Cu)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CU");
        //    entity.Property(e => e.Gm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("GM");
        //    entity.Property(e => e.I).HasColumnType("numeric(8, 2)");
        //    entity.Property(e => e.Idc).HasColumnName("IDC");
        //    entity.Property(e => e.Idf).HasColumnName("IDF");
        //    entity.Property(e => e.Kg)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KG");
        //    entity.Property(e => e.Kme)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KME");
        //    entity.Property(e => e.Kmi)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMI");
        //    entity.Property(e => e.Kmii)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMII");
        //    entity.Property(e => e.Kmiii)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMIII");
        //    entity.Property(e => e.Kmiv)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMIV");
        //    entity.Property(e => e.Kmt)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMT");
        //    entity.Property(e => e.Kmtract)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMTRACT");
        //    entity.Property(e => e.Kmurb)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMURB");
        //    entity.Property(e => e.Kmv)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMV");
        //    entity.Property(e => e.Kmvi)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMVI");
        //    entity.Property(e => e.Nrc).HasColumnName("NRC");
        //    entity.Property(e => e.Nri).HasColumnName("NRI");
        //    entity.Property(e => e.Nrq1).HasColumnName("NRQ1");
        //    entity.Property(e => e.Nrq4).HasColumnName("NRQ4");
        //    entity.Property(e => e.Ra)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("RA");
        //    entity.Property(e => e.Remorca).HasColumnName("REMORCA");
        //    entity.Property(e => e.T).HasColumnType("numeric(8, 2)");
        //    entity.Property(e => e.Traseu)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("TRASEU");
        //    entity.Property(e => e.U).HasColumnType("numeric(8, 2)");
        //});

        //modelBuilder.Entity<FazDateluna>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_dateluna_PK");

        //    entity.ToTable("faz_dateluna");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Anul).HasColumnName("ANUL");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Bm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("BM");
        //    entity.Property(e => e.Cn)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CN");
        //    entity.Property(e => e.Cu)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("CU");
        //    entity.Property(e => e.Gz)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("GZ");
        //    entity.Property(e => e.Ida).HasColumnName("IDA");
        //    entity.Property(e => e.Idd).HasColumnName("IDD");
        //    entity.Property(e => e.Idl).HasColumnName("IDL");
        //    entity.Property(e => e.Km)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KM");
        //    entity.Property(e => e.Kme)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KME");
        //    entity.Property(e => e.Kmet)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMET");
        //    entity.Property(e => e.Kminc)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMINC");
        //    entity.Property(e => e.Kmt)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("KMT");
        //    entity.Property(e => e.Ore)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("ORE");
        //    entity.Property(e => e.Restit)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("RESTIT");
        //    entity.Property(e => e.Restrez)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("RESTREZ");
        //    entity.Property(e => e.Tone)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("TONE");
        //    entity.Property(e => e.Tonekm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("TONEKM");
        //    entity.Property(e => e.Ua)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("UA");
        //    entity.Property(e => e.Um)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("UM");
        //    entity.Property(e => e.Ut)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("UT");
        //});

        //modelBuilder.Entity<FazFoaie>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_foaie_PK");

        //    entity.ToTable("faz_foaie");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Anul).HasColumnName("ANUL");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Bm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("BM");
        //    entity.Property(e => e.Data)
        //        .HasMaxLength(10)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("DATA");
        //    entity.Property(e => e.Decada).HasColumnName("DECADA");
        //    entity.Property(e => e.Gz)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("GZ");
        //    entity.Property(e => e.Ida).HasColumnName("IDA");
        //    entity.Property(e => e.Idf).HasColumnName("IDF");
        //    entity.Property(e => e.Idl).HasColumnName("IDL");
        //    entity.Property(e => e.Nractc)
        //        .HasMaxLength(250)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NRACTC");
        //    entity.Property(e => e.Nractrst)
        //        .HasMaxLength(250)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NRACTRST");
        //    entity.Property(e => e.Nractschf)
        //        .HasMaxLength(25)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NRACTSCHF");
        //    entity.Property(e => e.Nractschu)
        //        .HasMaxLength(25)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NRACTSCHU");
        //    entity.Property(e => e.Nractu1)
        //        .HasMaxLength(250)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NRACTU1");
        //    entity.Property(e => e.Nractu2)
        //        .HasMaxLength(250)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NRACTU2");
        //    entity.Property(e => e.Nractu3)
        //        .HasMaxLength(250)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NRACTU3");
        //    entity.Property(e => e.Nrf).HasColumnName("NRF");
        //    entity.Property(e => e.Nrore)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("NRORE");
        //    entity.Property(e => e.Orap)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("ORAP");
        //    entity.Property(e => e.Oras)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("ORAS");
        //    entity.Property(e => e.Restit)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("RESTIT");
        //    entity.Property(e => e.Schf)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("SCHF");
        //    entity.Property(e => e.Schu)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("SCHU");
        //    entity.Property(e => e.Sofer)
        //        .HasMaxLength(25)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("SOFER");
        //    entity.Property(e => e.Ua)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("UA");
        //    entity.Property(e => e.Um)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("UM");
        //    entity.Property(e => e.Ut)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("UT");
        //});

        //modelBuilder.Entity<FazLuna>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_luna_PK");

        //    entity.ToTable("faz_luna");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Denluna)
        //        .HasMaxLength(10)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("DENLUNA");
        //    entity.Property(e => e.Idl).HasColumnName("IDL");
        //});

        //modelBuilder.Entity<FazPiese>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_piese_PK");

        //    entity.ToTable("faz_piese");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Act)
        //        .HasMaxLength(25)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("ACT");
        //    entity.Property(e => e.An)
        //        .HasMaxLength(4)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("AN");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Denp)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("DENP");
        //    entity.Property(e => e.Ida).HasColumnName("IDA");
        //    entity.Property(e => e.Idp).HasColumnName("IDP");
        //    entity.Property(e => e.Luna)
        //        .HasMaxLength(2)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("LUNA");
        //    entity.Property(e => e.Valoare)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("VALOARE");
        //    entity.Property(e => e.Zi)
        //        .HasMaxLength(2)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("ZI");
        //});

        //modelBuilder.Entity<FazSetari>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_setari_PK");

        //    entity.ToTable("faz_setari");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Functie)
        //        .HasMaxLength(30)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("FUNCTIE");
        //    entity.Property(e => e.Intocmit)
        //        .HasMaxLength(30)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("INTOCMIT");
        //    entity.Property(e => e.Numeman)
        //        .HasMaxLength(30)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NUMEMAN");
        //    entity.Property(e => e.Unitate)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("UNITATE");
        //});

        //modelBuilder.Entity<FazSistemu>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_sistemu_PK");

        //    entity.ToTable("faz_sistemu");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Autom)
        //        .HasDefaultValue("")
        //        .HasColumnType("text")
        //        .HasColumnName("AUTOM");
        //    entity.Property(e => e.Baia)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("BAIA");
        //    entity.Property(e => e.Filtru)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("FILTRU");
        //    entity.Property(e => e.Idul).HasColumnName("IDUL");
        //});

        //modelBuilder.Entity<FazSpori>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_spori_PK");

        //    entity.ToTable("faz_spori");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.FelAuto)
        //        .HasMaxLength(200)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("FEL_AUTO");
        //    entity.Property(e => e.FelPrest)
        //        .HasMaxLength(40)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("FEL_PREST");
        //    entity.Property(e => e.FelPrest2)
        //        .HasMaxLength(40)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("FEL_PREST2");
        //    entity.Property(e => e.Nri).HasColumnName("NRI");
        //    entity.Property(e => e.Sporkm)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("SPORKM");
        //});

        //modelBuilder.Entity<FazSport>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_sport_PK");

        //    entity.ToTable("faz_sport");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Gr)
        //        .HasMaxLength(15)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("GR");
        //    entity.Property(e => e.Motor1r1).HasColumnName("MOTOR1R1");
        //    entity.Property(e => e.Motor1r2).HasColumnName("MOTOR1R2");
        //    entity.Property(e => e.Motor2r1).HasColumnName("MOTOR2R1");
        //    entity.Property(e => e.Motor2r2).HasColumnName("MOTOR2R2");
        //    entity.Property(e => e.Motor3r1).HasColumnName("MOTOR3R1");
        //    entity.Property(e => e.Motor3r2).HasColumnName("MOTOR3R2");
        //    entity.Property(e => e.Nrcrt)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("NRCRT");
        //    entity.Property(e => e.TipR)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("TIP_R");
        //});

        //modelBuilder.Entity<FazSporu>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_sporu_PK");

        //    entity.ToTable("faz_sporu");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Autom)
        //        .HasDefaultValue("")
        //        .HasColumnType("text")
        //        .HasColumnName("AUTOM");
        //    entity.Property(e => e.Bucur).HasColumnName("BUCUR");
        //    entity.Property(e => e.Idu).HasColumnName("IDU");
        //    entity.Property(e => e.Municip).HasColumnName("MUNICIP");
        //    entity.Property(e => e.Restul).HasColumnName("RESTUL");
        //});

        //modelBuilder.Entity<FazUleica1>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_uleica1_PK");

        //    entity.ToTable("faz_uleica1");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Autobuz)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("AUTOBUZ");
        //    entity.Property(e => e.Autocamion)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("AUTOCAMION");
        //    entity.Property(e => e.Autotren)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("AUTOTREN");
        //    entity.Property(e => e.Ciclu)
        //        .HasMaxLength(25)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("CICLU");
        //    entity.Property(e => e.Idard).HasColumnName("IDARD");
        //});

        //modelBuilder.Entity<FazUleica2>(entity =>
        //{
        //    entity.HasKey(e => e.Arid).HasName("faz_uleica2_PK");

        //    entity.ToTable("faz_uleica2");

        //    entity.Property(e => e.Arid).HasColumnName("arid");
        //    entity.Property(e => e.Aractstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("aractstamp");
        //    entity.Property(e => e.Arstamp)
        //        .HasDefaultValueSql("('')")
        //        .HasColumnType("datetime")
        //        .HasColumnName("arstamp");
        //    entity.Property(e => e.Arstatus).HasColumnName("arstatus");
        //    entity.Property(e => e.Ciclu)
        //        .HasMaxLength(25)
        //        .IsUnicode(false)
        //        .HasDefaultValue("")
        //        .IsFixedLength()
        //        .HasColumnName("CICLU");
        //    entity.Property(e => e.Idard2).HasColumnName("IDARD2");
        //    entity.Property(e => e._00111000)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("_001_11000");
        //    entity.Property(e => e._0012005)
        //        .HasColumnType("numeric(8, 2)")
        //        .HasColumnName("_001_2005");
        //    entity.Property(e => e._11000).HasColumnType("numeric(8, 2)");
        //    entity.Property(e => e._2000).HasColumnType("numeric(8, 2)");
        //});

        //OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseDomain>();

        var userId = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = userId;
                entry.Entity.DateCreated = DateTime.UtcNow;
            }

            entry.Entity.UpdatedBy = userId;
            entry.Entity.DateUpdated = DateTime.UtcNow;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
