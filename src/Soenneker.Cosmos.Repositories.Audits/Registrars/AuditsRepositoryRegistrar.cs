using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cosmos.Container.Registrars;
using Soenneker.Cosmos.Repositories.Audits.Abstract;
using Soenneker.Utils.BackgroundQueue.Registrars;
using Soenneker.Utils.UserContext.Registrars;

namespace Soenneker.Cosmos.Repositories.Audits.Registrars;

/// <summary>
/// A data persistence abstraction layer for Cosmos DB Audit type documents
/// </summary>
public static class AuditsRepositoryRegistrar
{
    /// <summary>
    /// Adds <see cref="IAuditsRepository"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddAuditsRepositoryAsSingleton(this IServiceCollection services)
    {
        services.AddBackgroundQueueAsSingleton()
                .AddUserContextAsScoped()
                .AddCosmosContainerUtilAsSingleton()
                .TryAddSingleton<IAuditsRepository, AuditsRepository>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IAuditsRepository"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddAuditsRepositoryAsScoped(this IServiceCollection services)
    {
        services.AddBackgroundQueueAsSingleton()
                .AddUserContextAsScoped()
                .AddCosmosContainerUtilAsSingleton()
                .TryAddScoped<IAuditsRepository, AuditsRepository>();

        return services;
    }
}