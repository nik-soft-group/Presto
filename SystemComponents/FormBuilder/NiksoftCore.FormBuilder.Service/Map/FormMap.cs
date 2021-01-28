﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NiksoftCore.FormBuilder.Service
{
    public class FormMap : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("FB_Forms");

            builder.HasOne(x => x.FormCategroy)
                .WithMany(x => x.Forms)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired(true);
        }
    }
}