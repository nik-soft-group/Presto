using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.ITCF.Service
{
    public class UserLegalFormMap : IEntityTypeConfiguration<UserLegalForm>
    {
        public void Configure(EntityTypeBuilder<UserLegalForm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("ITCF_UserLegalForms");
        }
    }
}