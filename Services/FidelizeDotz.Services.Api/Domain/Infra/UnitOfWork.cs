using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FidelizeDotz.Services.Api.Domain.Infra
{
    public class UnitOfWork<TContext> : IUnitOfWorks<TContext>, IUnitOfWork
        where TContext : DbContext
    {
        private bool disposed;
        private Dictionary<Type, object> repositories;

        public UnitOfWork(TContext context)
        {
            var context1 = context;
            DbContext = (object) context1 != null ? context1 : throw new ArgumentNullException(nameof(context));
        }

        public TContext DbContext { get; }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
        {
            if (repositories == null)
                repositories = new Dictionary<Type, object>();
            if (hasCustomRepository)
            {
                var service = DbContext.GetService<IRepository<TEntity>>();
                if (service != null)
                    return service;
            }

            var key = typeof(TEntity);
            if (!repositories.ContainsKey(key))
                repositories[key] = new Repository<TEntity>(DbContext);
            return (IRepository<TEntity>) repositories[key];
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlRaw(sql, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return DbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            var num = await DbContext.SaveChangesAsync();
            return num;
        }

        public async Task<int> SaveChangesAsync(
            bool ensureAutoHistory = false,
            params IUnitOfWork[] unitOfWorks)
        {
            int num;
            using (var ts = new TransactionScope())
            {
                var count = 0;
                var unitOfWorkArray = unitOfWorks;
                for (var index = 0; index < unitOfWorkArray.Length; ++index)
                {
                    var unitOfWork = unitOfWorkArray[index];
                    var num1 = count;
                    var num2 = await unitOfWork.SaveChangesAsync();
                    count = num1 + num2;
                    unitOfWork = null;
                }

                unitOfWorkArray = null;
                var num3 = count;
                var num4 = await SaveChangesAsync();
                count = num3 + num4;
                ts.Complete();
                num = count;
            }

            return num;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
        {
            DbContext.ChangeTracker.TrackGraph(rootEntity, callback);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (repositories != null)
                    repositories.Clear();
                DbContext.Dispose();
            }

            disposed = true;
        }
    }
}