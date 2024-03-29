using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Infrastructure.Clock;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Infrastructure.Email;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Abstractions.Data;
using Dapper;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("ConnectionString") 
             ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();
        services.AddScoped<IAlquilerRepository, AlquilerRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }

}

