using FidelizeDotz.Services.Api.Domain.Infra;
using FidelizeDotz.Services.Api.Domain.Infra.Data;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FidelizeDotz.Services.Api.Configuration
{
    /// <summary>
    ///     Classe responsável por realizar a configuração DBContext com a base de dados utilizando o EF
    /// </summary>
    /// <remarks>
    ///     Só foi implementado a conexão com o Postgres para demonstração.
    ///     Caso seja necessário comunicar com outra base de dados,
    ///     aqui é o local que devemos configurar e realizar a implementação dos métodos
    /// </remarks>
    public static class DatabaseConfiguration
    {
        public static void AddEFMySqlConfiguration(this IServiceCollection services, string connection)
        {
            services.AddDbContext<FidelizeDotzDbContext>(options =>
            {
                options.UseMySql(connection);
                options.EnableDetailedErrors();
            });
            services.AddScoped<IUnitOfWork, UnitOfWork<FidelizeDotzDbContext>>();
            services.AddScoped<IUnitOfWorks<FidelizeDotzDbContext>, UnitOfWork<FidelizeDotzDbContext>>();
        }
    }
}