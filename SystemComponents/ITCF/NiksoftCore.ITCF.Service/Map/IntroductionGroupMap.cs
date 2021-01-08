using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.ITCF.Service
{
    public class IntroductionGroupMap : IEntityTypeConfiguration<IntroductionGroup>
    {
        public void Configure(EntityTypeBuilder<IntroductionGroup> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("ITCF_IntroductionGroups");
        }
    }
}