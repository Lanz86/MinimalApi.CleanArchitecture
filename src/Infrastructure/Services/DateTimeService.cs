using MinimalApi.CleanArchitecture.Application.Common.Interfaces;

namespace MinimalApi.CleanArchitecture.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}
