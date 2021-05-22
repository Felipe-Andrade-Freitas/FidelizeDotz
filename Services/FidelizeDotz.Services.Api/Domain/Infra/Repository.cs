using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;
using MetLife.Sinistro.Api.CrossCutting.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FidelizeDotz.Services.Api.Domain.Infra
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual IPagedList<TEntity> GetPagedList(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> source = _dbSet;
            if (disableTracking)
                source = source.AsNoTracking();
            if (include != null)
                source = include(source);
            if (predicate != null)
                source = source.Where(predicate);
            if (ignoreQueryFilters)
                source = source.IgnoreQueryFilters();
            return orderBy != null
                ? orderBy(source).ToPagedList(pageIndex, pageSize)
                : source.ToPagedList(pageIndex, pageSize);
        }

        public virtual Task<IPagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> source = _dbSet;
            if (disableTracking)
                source = source.AsNoTracking();
            if (include != null)
                source = include(source);
            if (predicate != null)
                source = source.Where(predicate);
            if (ignoreQueryFilters)
                source = source.IgnoreQueryFilters();
            return orderBy != null
                ? orderBy(source).ToPagedListAsync(pageIndex, pageSize, cancellationToken: cancellationToken)
                : source.ToPagedListAsync(pageIndex, pageSize, cancellationToken: cancellationToken);
        }

        public virtual IPagedList<TResult> GetPagedList<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
            where TResult : class
        {
            IQueryable<TEntity> source = _dbSet;
            if (disableTracking)
                source = source.AsNoTracking();
            if (include != null)
                source = include(source);
            if (predicate != null)
                source = source.Where(predicate);
            if (ignoreQueryFilters)
                source = source.IgnoreQueryFilters();
            return orderBy != null
                ? orderBy(source).Select(selector).ToPagedList(pageIndex, pageSize)
                : source.Select(selector).ToPagedList(pageIndex, pageSize);
        }

        public virtual Task<IPagedList<TResult>> GetPagedListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 0,
            int pageSize = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default,
            bool ignoreQueryFilters = false)
            where TResult : class
        {
            IQueryable<TEntity> source = _dbSet;
            if (disableTracking)
                source = source.AsNoTracking();
            if (include != null)
                source = include(source);
            if (predicate != null)
                source = source.Where(predicate);
            if (ignoreQueryFilters)
                source = source.IgnoreQueryFilters();
            return orderBy != null
                ? orderBy(source).Select(selector)
                    .ToPagedListAsync(pageIndex, pageSize, cancellationToken: cancellationToken)
                : source.Select(selector).ToPagedListAsync(pageIndex, pageSize, cancellationToken: cancellationToken);
        }

        public virtual TEntity GetFirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> source = _dbSet;
            if (disableTracking)
                source = source.AsNoTracking();
            if (include != null)
                source = include(source);
            if (predicate != null)
                source = source.Where(predicate);
            if (ignoreQueryFilters)
                source = source.IgnoreQueryFilters();
            return orderBy != null ? orderBy(source).FirstOrDefault() : source.FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
            {
                var entity = await orderBy(query).FirstOrDefaultAsync();
                return entity;
            }

            var entity1 = await query.FirstOrDefaultAsync();
            return entity1;
        }

        public virtual TResult GetFirstOrDefault<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> source = _dbSet;
            if (disableTracking)
                source = source.AsNoTracking();
            if (include != null)
                source = include(source);
            if (predicate != null)
                source = source.Where(predicate);
            if (ignoreQueryFilters)
                source = source.IgnoreQueryFilters();
            return orderBy != null
                ? orderBy(source).Select(selector).FirstOrDefault()
                : source.Select(selector).FirstOrDefault();
        }

        public virtual async Task<TResult> GetFirstOrDefaultAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
                query = query.AsNoTracking();
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (ignoreQueryFilters)
                query = query.IgnoreQueryFilters();
            if (orderBy != null)
            {
                var result = await orderBy(query).Select(selector).FirstOrDefaultAsync();
                return result;
            }

            var result1 = await query.Select(selector).FirstOrDefaultAsync();
            return result1;
        }

        public virtual IQueryable<TEntity> FromSql(string sql, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(sql, parameters);
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual ValueTask<TEntity> FindAsync(params object[] keyValues)
        {
            return _dbSet.FindAsync(keyValues);
        }

        public virtual ValueTask<TEntity> FindAsync(
            object[] keyValues,
            CancellationToken cancellationToken)
        {
            return _dbSet.FindAsync(keyValues, cancellationToken);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? _dbSet.Count() : _dbSet.Count(predicate);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Insert(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return _dbSet.AddAsync(entity, cancellationToken).AsTask();
        }

        public virtual Task InsertAsync(params TEntity[] entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }

        public virtual Task InsertAsync(
            IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default)
        {
            return _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Update(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var property1 = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property2 = typeInfo.GetProperty(property1?.Name);
            if (property2 != null)
            {
                var instance = Activator.CreateInstance<TEntity>();
                property2.SetValue(instance, id);
                _dbContext.Entry(instance).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null)
                    Delete(entity);
            }
        }

        public virtual void Delete(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual void UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}