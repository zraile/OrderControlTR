using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OrderControlTR.Application.Mappings;

namespace OrderControlTR.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}
