using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.FormBuilder.Service
{
    public class ListControlMap : IEntityTypeConfiguration<ListControl>
    {
        public void Configure(EntityTypeBuilder<ListControl> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("FB_ListControls");

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Childs)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false);
        }
    }
}