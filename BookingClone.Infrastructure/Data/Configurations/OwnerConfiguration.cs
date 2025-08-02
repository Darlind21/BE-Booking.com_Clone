using BookingClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Data.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasIndex(o => o.IdCardNumber)
                .IsUnique();

            builder.HasIndex(o => o.BankAccount)
                .IsUnique();

            builder.HasIndex(o => o.PhoneNumber)
                .IsUnique();
        }
    }
}
