using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Server.Model.Interfaces.Repositories
{
    public interface IRepository<TEntity,TPrimaryKeyType> where TEntity : class
    {
        //Get with expression
        IEnumerable<TEntity> Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAsync(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string includeProperties = "");
        //Get
        TEntity GetById(TPrimaryKeyType id);
        Task<TEntity> GetByIdAsync(TPrimaryKeyType id);
        //Get All
        IQueryable<TEntity> GetAllQueryable();
        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        //First Or Default
        TEntity FirstOrDefault();
        Task<TEntity> FirstOrDefaultAsync();
        //Find
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        //Single Or Default
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        //Add
        TEntity Insert(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        //Add Range
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        //Remove
        void Remove(TEntity entity);
        Task RemoveAsync(TEntity entity);
        //Remove Range
        void RemoveRange(IEnumerable<TEntity> entities);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        // void RemoveEntity(T entityToDelete);
        //Update
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);


        // Transactions
        public  IDbContextTransaction BeginTransaction();
        public Task<IDbContextTransaction> BeginTransactionAsync();
        public void CommitTransaction();
        public Task CommitTransactionAsync();
        public int SaveChanges();
        public Task<int> SaveChangesAsync();
        public void RollbackTransaction();
        public  Task RollbackTransactionAsync();
        public void ClearChangeTracker();

        //CreateExpression
        public IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, string value, bool exact = false);
        public  IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, decimal? value);
        public  IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, int? value);
        public  IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, bool? value);
        public  IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, List<int> values);
        //  public IQueryable<TEntity> CreateExpression<TEntity>(IQueryable<TEntity> recordSet, string columnName, string value, bool exact = false);

        //T GetById(int id);
        //IEnumerable<TEntity> GetAll();
        //IEnumerable<TEntity> Find(Expression<Func<T, bool>> expression);
        //void Add(T entity);
        //void AddRange(IEnumerable<TEntity> entities);
        //void Remove(T entity);
        //void RemoveRange(IEnumerable<TEntity> entities);
    }   
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class
    {

    }
}
