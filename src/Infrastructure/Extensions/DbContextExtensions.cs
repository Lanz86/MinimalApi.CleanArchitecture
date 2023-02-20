using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using MinimalApi.CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.CleanArchitecture.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task DispatchDomainEvents(this DbContext context)
        {
            var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await domainEvent.PublishAsync();
        }
    }
}
