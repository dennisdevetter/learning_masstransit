using System.Linq;
using System.Reflection;
using LearningMassTransit.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LearningMassTransit.Infrastructure.EntityFramework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies == null)
            return services;

        var dbContextType = typeof(DbContext);
        var dbSetType = typeof(DbSet<>);

        var interfaceRepoType = typeof(IRepository<,>);
        var repoType = typeof(Repository<,,>);

        foreach (var dbContext in assemblies.SelectMany(assembly =>
                     assembly.GetTypes().Where(type => dbContextType.IsAssignableFrom(type))))
        {
            foreach (var dbSet in dbContext.GetProperties()
                         .Where(property =>
                             property.PropertyType.IsGenericType &&
                             property.PropertyType.GetGenericTypeDefinition() == dbSetType)
                         .Select(property => property.PropertyType))
            {
                var entityType = dbSet.GetGenericArguments()[0];

                if (entityType.BaseType.GetGenericArguments().Any())
                {
                    var idType = entityType.BaseType.GetGenericArguments()[0];

                    var entityInterfaceRepoType = interfaceRepoType.MakeGenericType(entityType, idType);
                    var entityRepoType = repoType.MakeGenericType(dbContext, entityType, idType);

                    services.AddScoped(entityInterfaceRepoType, entityRepoType);
                }

            }
        }

        return services;
    }
}