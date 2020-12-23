using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.ITCF.Service
{
    public class BusinessMap : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("ITCF_Businesses");

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Businesses)
                .HasForeignKey(x => x.CatgoryId).IsRequired(true);

            builder.HasOne(x => x.IndustrialPark)
                .WithMany(x => x.Businesses)
                .HasForeignKey(x => x.IndustrialParkId).IsRequired(false);
        }
    }
}