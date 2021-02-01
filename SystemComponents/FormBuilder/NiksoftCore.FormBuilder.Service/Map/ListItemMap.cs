using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.FormBuilder.Service
{
    public class ListItemMap : IEntityTypeConfiguration<ListItem>
    {
        public void Configure(EntityTypeBuilder<ListItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("FB_ListItems");

            builder.HasOne(x => x.ListControl)
                .WithMany(x => x.ListItems)
                .HasForeignKey(x => x.ListControlId)
                .IsRequired(true);
        }
    }
}