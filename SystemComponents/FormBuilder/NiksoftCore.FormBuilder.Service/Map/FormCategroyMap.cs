using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.FormBuilder.Service
{
    public class FormCategroyMap : IEntityTypeConfiguration<FormCategroy>
    {
        public void Configure(EntityTypeBuilder<FormCategroy> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("FB_FormCategries");

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Childs)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false);
        }
    }
}