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
        }
    }
}