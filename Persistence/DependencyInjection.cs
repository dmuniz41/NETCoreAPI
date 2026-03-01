using System;
using Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Application.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("Database"))
                    .UseSnakeCaseNamingConvention()); // Make sure you have the snake case package installed too

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
       {
           options.Password.RequireDigit = true;
           options.Password.RequireLowercase = true;
           // Configure other options as needed
       })
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();

        return services;
    }
}