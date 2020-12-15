using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.SystemBase.Service
{
    public class SystemSettingMap : IEntityTypeConfiguration<SystemSetting>
    {
        public void Configure(EntityTypeBuilder<SystemSetting> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("SystemSettings");
        }
    }
}