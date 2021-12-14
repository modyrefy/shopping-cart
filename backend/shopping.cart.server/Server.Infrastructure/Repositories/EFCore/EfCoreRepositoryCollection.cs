using Server.Infrastructure.Data;
using Server.Model.Interfaces.Repositories;
using Server.Model.Models;

namespace Server.Infrastructure.Repositories.EFCore
{
    public class EfCoreRepositoryCollection : IRepositoriesCollection
    {
        private readonly DefaultDBContext DbContext;
        //private ITestRepository TestRepository;
        public EfCoreRepositoryCollection(DefaultDBContext dbContext)
        {
            this.DbContext = dbContext;
        }

        //public IRepository<Brands> BrandsRepository{ get {
        //        if (brandsRepository == null)
        //        { 
        //        brandsRepository= new EfCoreRepository<Brands>(this.DbContext);
        //        }
        //        return brandsRepository;
        //    } }

        public IRepository<Brands> BrandRepository => new EfCoreRepository<Brands>(this.DbContext);
        public IRepository<Categories> CategoryRepository => new EfCoreRepository<Categories>(this.DbContext);
        public IRepository<Users> UserRepository => new EfCoreRepository<Users>(this.DbContext);
        public IRepository<ExceptionLogs> ExceptionLogRepository => new EfCoreRepository<ExceptionLogs>(this.DbContext);
        public ITestRepository TestRepository => new CustomTestRepository(DbContext);
        public IRepository<object> Repository => new EfCoreRepository<object>(this.DbContext);
        // public  ITestRepository TestRepository =>new TestRepository(DbContext);
    }
}
