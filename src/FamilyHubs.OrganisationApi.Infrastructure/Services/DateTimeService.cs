using FamilyHubs.SharedKernel.Interfaces;

namespace FamilyHubs.Organisation.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

