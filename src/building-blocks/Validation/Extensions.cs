using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Validation;
public static class Extensions
{
    public static IServiceCollection RegisterValidators(this IServiceCollection services, Assembly assemblyContainingValidators)
    {
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(assemblyContainingValidators);
        return services;
    }
}
