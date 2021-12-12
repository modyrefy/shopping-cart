using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Server.Core.BaseClasses;
using Server.Core.Extensions;
using Server.Core.Interfaces.Repositories;
using Server.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories.EFCore
{

    //    public class TestRepository1 : EfCoreRepository<Object>,ITestRepository
    //    {
    //        private DefaultDBContext Context;
    //        public TestRepository1(DefaultDBContext dBContext) : base(dBContext)
    //        {
    //            Context = dBContext;
    //        }

    //        public dynamic SearchContextExpression()
    //        {
    //            var px1 = (from p in Context.Users select p).ToList();

    //            var query = from p in Context.Users
    //                        join p1 in Context.UserRoles
    //    on p.UserRoleId equals p1.UserRoleId
    //                        select new
    //                        {
    //                            UserId = p.UserId,
    //                            UserName = p.UserName + ' ' + Guid.NewGuid().ToString(),
    //                            RoleId = p1.UserRoleId,
    //                            RoleName = p1.UserRole
    //                        };
    //            query = query.Where(p => p.UserId == 1);
    //            query = query.Where(p => p.UserName != "abc");
    //            //query=this.CreateExpression(query, "UserName", "democustomer1");
    //            return query.ToList();

    //        }

    //        public dynamic SearchContext()
    //        {
    //            return (from p in Context.Users
    //                    join p1 in Context.UserRoles
    //on p.UserRoleId equals p1.UserRoleId
    //                    select new
    //                    {
    //                        UserId = p.UserId,
    //                        UserName = p.UserName + ' ' + Guid.NewGuid().ToString(),
    //                        RoleId = p1.UserRoleId,
    //                        RoleName = p1.UserRole
    //                    }).ToList();
    //        }
    //        public async Task<List<Get_User_InformationResult>> SearchAsync(string UserNmae, string Password)
    //        {

    //            return await Context.Procedures.Get_User_InformationAsync("ahmed", "123");
    //        }

    //        Task<List<Get_User_InformationResult>> ITestRepository.SearchAsync(string UserNmae, string Password)
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }
    public class EfCoreRepository<TEntity> : EfCoreRepository<TEntity, int> , IRepository<TEntity> where TEntity : class
    {
        public EfCoreRepository(DefaultDBContext dBContext) : base(dBContext)
        {
        }
    }
    public class EfCoreRepository<TEntity, TPrimaryKeyType> : RepositoryBase<TEntity, TPrimaryKeyType> where TEntity : class
    {
        internal  DefaultDBContext Context;
        protected DbSet<TEntity> Entities => Context.Set<TEntity>();
        //public EfCoreRepository()
        //{
        //    Context = new DefaultDBContext();
        //    //Entities = Context.Set<TEntity>();
        //}
        public EfCoreRepository(DefaultDBContext dBContext)
        {
            
            Context = dBContext;
            //Entities = Context.Set<TEntity>();
        }

        public override TEntity Insert(TEntity entity)
        {
           return  Entities.Add(entity).Entity;
        }
        public override Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }
        public override TEntity Update(TEntity entity)
        {
            var entry = AttachIfNot(entity);
            entry.State = EntityState.Modified;
            return entity;
        }
        public override Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = Update(entity);
            return Task.FromResult(entity);
        }
        public override IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }
        public override Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAsync(predicate);
        }
        public override TEntity FirstOrDefault()
        {
            return Entities.FirstOrDefault();
        }
        public override Task<TEntity> FirstOrDefaultAsync()
        {
            return Entities.FirstOrDefaultAsync();
        }
        public override IQueryable<TEntity> GetAllQueryable()
        {
            return Entities.AsQueryable();
        }
        public override List<TEntity> GetAll()
        {
            return Entities.ToList();
        }
        public override Task<List<TEntity>> GetAllAsync()
        {
            return Entities.ToListAsync();
        }
        public override TEntity GetById(TPrimaryKeyType id)
        {
            return Entities.Find(id);
        }
        public override Task<TEntity> GetByIdAsync(TPrimaryKeyType id)
        {
            return Task.FromResult(GetById(id));
        }
        public override TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).SingleOrDefault();
        }
        public override Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).SingleOrDefaultAsync();
        }
        public override IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public override Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
           return Task.FromResult(Get(filter,orderBy, includeProperties));
        }
        public override void Remove(TEntity entity)
        {

            AttachIfNot(entity);
            //if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            //{
            //    ((ISoftDelete)entity).IsDeleted = true;
            //    this.Update(entity);
            //}
            //else
            //{
            //    Entities.Remove(entity);
            //}
            Entities.Remove(entity);
            //Context.SaveChanges();
        }
        public override Task RemoveAsync(TEntity entity)
        {
            Entities.Remove(entity);
            return Task.CompletedTask;
            //Context.SaveChanges();
        }
        public override void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
            //Context.SaveChanges();
        }
        public override Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
            //Context.SaveChanges();
            return Task.CompletedTask;

        }
        public override void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
            //Context.SaveChanges();
        }
        public override Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
            //Context.SaveChanges();
            return Task.CompletedTask;
        }


        //Transactions
        public override void RollbackTransaction()
        {
            this.Context.Database.RollbackTransaction();
        }
        public override  Task RollbackTransactionAsync()
        {
           return  this.Context.Database.RollbackTransactionAsync();
            //return Task.CompletedTask;
        }
        public override int SaveChanges()
        {
          return this.Context.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
            //return Task.FromResult (this.Context.SaveChanges());
        }
        public override IDbContextTransaction BeginTransaction()
        {
            return this.Context.Database.BeginTransaction();
        }
        public override Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return this.Context.Database.BeginTransactionAsync();
        }
        public override void CommitTransaction()
        {
            this.Context.Database.CommitTransaction();
        }
        public override async Task CommitTransactionAsync()
        {
            await Context.Database.CommitTransactionAsync();
        }


        protected virtual EntityEntry<TEntity> AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries<TEntity>().FirstOrDefault();

            // var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry == null)
            {
                Entities.Attach(entity);
                entry = Context.ChangeTracker.Entries<TEntity>().FirstOrDefault();
            }


            return entry;
        }

        //CreateExpression
        public override IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, string value, bool exact = false)
        {
            if (string.IsNullOrEmpty(value)) return recordSet;
            value = value.ToLower().Trim().AsNormalizedString();
            if (exact)
            {
                recordSet = recordSet.Where(e => EF.Property<string>(e, columnName).ToLower().Trim() == value);
                return recordSet;
            }

            recordSet = recordSet.Where(e => EF.Property<string>(e, columnName).ToLower().Trim().Contains(value));
            return recordSet;
        }
        public override IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, decimal? value)
        {
            if (!value.HasValue) return recordSet;
            recordSet = recordSet.Where(e => EF.Property<decimal>(e, columnName) == value.Value);
            return recordSet;
        }
        public override IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, int? value)
        {
            if (!value.HasValue) return recordSet;
            recordSet = recordSet.Where(e => EF.Property<int>(e, columnName) == value.Value);
            return recordSet;
        }
        public override IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, bool? value)
        {
            if (!value.HasValue) return recordSet;
            recordSet = recordSet.Where(e => EF.Property<bool>(e, columnName) == value.Value);
            return recordSet;
        }
        public override IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, List<int> values)
        {
            if (values == null || values.Count == 0) return recordSet;
            recordSet = recordSet.Where(e => values.Contains(EF.Property<int>(e, columnName)));
            return recordSet;
        }
       //public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        //{
        //    var query = GetQueryable();

        //    // if (this.RequestContext?.ActiveUserAccountInfo?.TenantId != null && this.RequestContext?.ActiveUserAccountInfo.TenantId > 0 )
        //    // {
        //    //     // if (typeof(IMustHaveTenant).IsAssignableFrom(typeof(TEntity)) || typeof(IMayHaveTenant).IsAssignableFrom(typeof(TEntity)))
        //    //     // {
        //    //     //     Expression<Func<TEntity, bool>> multiTenantFilter = e => EF.Property<int>(e, "TenantId") == this.RequestContext.ActiveUserAccountInfo.TenantId;
        //    //     //     query = query.Where(multiTenantFilter);
        //    //     // }
        //    // }

        //    if (!propertySelectors.IsNullOrEmpty())
        //    {
        //        foreach (var propertySelector in propertySelectors)
        //        {
        //            query = query.Include(propertySelector);
        //        }
        //    }

        //    return query;
        //}


    }
}
