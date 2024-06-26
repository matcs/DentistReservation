using DentistReservation.Domain.Aggregates.ChairAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentistReservation.Infrastructure.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Chair>
{
    public void Configure(EntityTypeBuilder<Chair> builder)
    {
        
    }
}