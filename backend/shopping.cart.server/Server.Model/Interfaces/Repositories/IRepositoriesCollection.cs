using Server.Model.Interfaces.Repositories;
using Server.Model.Models;

namespace Server.Model.Interfaces.Repositories
{
    public interface IRepositoriesCollection
    {
        #region Entities
        public IRepository<object> Repository { get; }
        public IRepository<Brands> BrandRepository { get; }
        public IRepository<Categories> CategoryRepository { get; }
        public IRepository<Users> UserRepository { get; }
        public IRepository<ExceptionLogs> ExceptionLogRepository { get; }
        public IRepository<Products> ProductRepository { get; }
        public IRepository<ProductTags> ProductTagRepository { get; }
        public IRepository<ProductImages> ProductImageRepository { get; }
        #endregion
        #region context
        public ILookupRepository LookupRepository { get; }

        #endregion
        //public IQueryable<T> CreateExpression1<T>(IQueryable<T> recordSet, string columnName, string value, bool exact = false);
        #region Sp
        public ITestRepository TestRepository { get; }
        #endregion
    }
}
