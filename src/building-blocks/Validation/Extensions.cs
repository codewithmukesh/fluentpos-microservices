using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Validation;
public static class Extensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services, Assembly assemblyContainingValidators)
    {
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssembly(assemblyContainingValidators);
        return services;
    }
}
