using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.FormBuilder.Service
{
    public class FormCategoryMap : IEntityTypeConfiguration<FormCategory>
    {
        public void Configure(EntityTypeBuilder<FormCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("FB_FormCategories");

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Childs)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false);
        }
    }
}