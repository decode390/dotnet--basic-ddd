using Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(config => {
      config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
    });

    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

    services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

    return services;
  }
}