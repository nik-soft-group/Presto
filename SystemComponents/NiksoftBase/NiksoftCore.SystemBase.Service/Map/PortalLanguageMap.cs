using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.SystemBase.Service
{
    public class PortalLanguageMap : IEntityTypeConfiguration<PortalLanguage>
    {
        public void Configure(EntityTypeBuilder<PortalLanguage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("PortalLanguages");
        }
    }
}