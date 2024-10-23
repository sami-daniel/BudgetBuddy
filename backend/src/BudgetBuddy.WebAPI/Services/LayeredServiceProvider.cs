using BudgetBuddy.IoC;

namespace BudgetBuddy.WebAPI.Services;

/// <summary>
/// Provides extension method to add layered services to the IServiceCollection.
/// </summary>
public static class LayeredServiceProvider
{
    /// <summary>
    /// Adds layered services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    public static void AddLayeredServices(this IServiceCollection services)
    {
        DISetup.Setup(services);
    }
}
