using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infra.Context;
using Infra.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
       IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddMapster();

            return services;
        }
    }
}
