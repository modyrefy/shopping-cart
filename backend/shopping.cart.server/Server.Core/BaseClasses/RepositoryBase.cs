using Microsoft.EntityFrameworkCore.Storage;
using Server.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Core.BaseClasses
{
    public abstract class RepositoryBase<TEntity, TPrimaryKeyType> : IRepository<TEntity, TPrimaryKeyType> where TEntity : class
    {

        //Get with expression
        public abstract IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        public abstract Task<IEnumerable<TEntity>> GetAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        //Get
        public abstract TEntity GetById(TPrimaryKeyType id);
        public abstract Task<TEntity> GetByIdAsync(TPrimaryKeyType id);
        //Get All
        public abstract IQueryable<TEntity> GetAllQueryable();
        public abstract List<TEntity> GetAll();
        public abstract Task<List<TEntity>> GetAllAsync();
        //First Or Default
        public abstract TEntity FirstOrDefault();
        public abstract Task<TEntity> FirstOrDefaultAsync();
        //Find
        public abstract IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        public abstract Task<IEnumerable<TEntity>> FindAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        //Single Or Default
        public abstract TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        public abstract Task<TEntity> SingleOrDefaultAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        //Add
        public abstract TEntity Insert(TEntity entity);
        public abstract Task<TEntity> InsertAsync(TEntity entity);
        //Add Range
        public abstract void AddRange(IEnumerable<TEntity> entities);
        public abstract Task AddRangeAsync(IEnumerable<TEntity> entities);
        //Remove
        public abstract void Remove(TEntity entity);
        public abstract Task RemoveAsync(TEntity entity);
        //Remove Range
        public abstract void RemoveRange(IEnumerable<TEntity> entities);
        public abstract Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        // void RemoveEntity(T entityToDelete);
        //Update
        public abstract TEntity Update(TEntity entity);
        public abstract Task<TEntity> UpdateAsync(TEntity entity);


        // Transactions
        public abstract IDbContextTransaction BeginTransaction();
        public abstract Task<IDbContextTransaction> BeginTransactionAsync();
        public abstract void CommitTransaction();
        public abstract Task CommitTransactionAsync();
        public abstract int SaveChanges();
        public abstract Task<int> SaveChangesAsync();
        public abstract void RollbackTransaction();
        public abstract Task RollbackTransactionAsync();

        //CreateExpression
        public abstract IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, string value, bool exact = false);
        public abstract IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, decimal? value);
        public abstract IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, int? value);
        public abstract IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, bool? value);
        public abstract IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, List<int> values);
    }
}
