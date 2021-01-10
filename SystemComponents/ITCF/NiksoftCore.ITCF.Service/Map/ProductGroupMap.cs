using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.ITCF.Service
{
    public class ProductGroupMap : IEntityTypeConfiguration<ProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("ITCF_ProductGroups");

            builder.HasOne(x => x.Business)
                .WithMany(x => x.ProductGroups)
                .HasForeignKey(x => x.BusinessId).IsRequired(false);

        }
    }
}