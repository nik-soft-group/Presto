using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.FormBuilder.Service
{
    public class FormAnswerMap : IEntityTypeConfiguration<FormAnswer>
    {
        public void Configure(EntityTypeBuilder<FormAnswer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("FB_FormAnswers");

            builder.HasOne(x => x.FormControl)
                .WithMany(x => x.FormAnswers)
                .HasForeignKey(x => x.ControlId)
                .IsRequired(true);

            builder.HasOne(x => x.Form)
                .WithMany(x => x.FormAnswers)
                .HasForeignKey(x => x.FormId)
                .IsRequired(true);
        }
    }
}