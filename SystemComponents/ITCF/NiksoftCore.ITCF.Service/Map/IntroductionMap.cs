using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.ITCF.Service
{
    public class IntroductionMap : IEntityTypeConfiguration<Introduction>
    {
        public void Configure(EntityTypeBuilder<Introduction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("ITCF_Introductions");

            builder.HasOne(x => x.Group)
                .WithMany(x => x.Introductions)
                .HasForeignKey(x => x.GroupId).IsRequired(false);

            builder.HasOne(x => x.Business)
                .WithMany(x => x.Introductions)
                .HasForeignKey(x => x.BusinessId).IsRequired(false);
        }
    }
}