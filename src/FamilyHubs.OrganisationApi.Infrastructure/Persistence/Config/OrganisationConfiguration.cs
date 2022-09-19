﻿using FamilyHubs.Organisation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyHubs.Organisation.Infrastructure.Persistence.Config;


public class OrganisationConfiguration : IEntityTypeConfiguration<OpenReferralOrganisation>
{
    public void Configure(EntityTypeBuilder<OpenReferralOrganisation> builder)
    {
        builder.Property(p => p.Id).ValueGeneratedNever();
        builder.Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(t => t.Description)
            .HasMaxLength(500);
        builder.Property(t => t.Created)
            .IsRequired();
        builder.Property(t => t.CreatedBy)
            .HasMaxLength(255)
            .IsRequired();
    }
}
