using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FidelizeDotz.Services.Api.Domain.Infra.Interfaces
{
    public interface IUnitOfWorks<TContext> : IUnitOfWork, IDisposable where TContext : DbContext
    {
        TContext DbContext { get; }

        Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks);

        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
    }
}