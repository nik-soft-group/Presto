using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.ITCF.Service
{
    public class IndustrialParkMap : IEntityTypeConfiguration<IndustrialPark>
    {
        public void Configure(EntityTypeBuilder<IndustrialPark> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("ITCF_IndustrialParks");

            builder.HasOne(x => x.Country)
                .WithMany(x => x.IndustrialParks)
                .HasForeignKey(x => x.CountryId).IsRequired(true);

            builder.HasOne(x => x.City)
                .WithMany(x => x.IndustrialParks)
                .HasForeignKey(x => x.CityId).IsRequired(true);

            builder.HasOne(x => x.Province)
                .WithMany(x => x.IndustrialParks)
                .HasForeignKey(x => x.ProvinceId).IsRequired(false);
        }
    }
}