using Microsoft.EntityFrameworkCore;
using NiksoftCore.DataAccess;

namespace NiksoftCore.ITCF.Service
{
    public class ITCFDbContext : NikDbContext, IITCFUnitOfWork
    {

        public ITCFDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<UserLegalForm> UserLegalForms { get; set; }
        public DbSet<BusinessCategory> BusinessCategories { get; set; }
        public DbSet<IndustrialPark> IndustrialParks { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<IntroductionGroup> introductionGroups { get; set; }
        public DbSet<Introduction> Introductions { get; set; }
        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserLegalFormMap());
            builder.ApplyConfiguration(new BusinessCategoryMap());
            builder.ApplyConfiguration(new IndustrialParkMap());
            builder.ApplyConfiguration(new BusinessMap());
            builder.ApplyConfiguration(new CountryMap());
            builder.ApplyConfiguration(new ProvinceMap());
            builder.ApplyConfiguration(new CityMap());
            builder.ApplyConfiguration(new IntroductionGroupMap());
            builder.ApplyConfiguration(new IntroductionMap());

        }
    }
}
