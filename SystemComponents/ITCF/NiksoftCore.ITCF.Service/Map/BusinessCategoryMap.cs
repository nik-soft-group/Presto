using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.ITCF.Service
{
    public class BusinessCategoryMap : IEntityTypeConfiguration<BusinessCategory>
    {
        public void Configure(EntityTypeBuilder<BusinessCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("ITCF_BusinessCategories");

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Childs)
                .HasForeignKey(x => x.ParentId).IsRequired(false);
        }
    }
}