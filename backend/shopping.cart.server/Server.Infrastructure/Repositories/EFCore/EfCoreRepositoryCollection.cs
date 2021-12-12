using Server.Core.Interfaces.Repositories;
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

        public IRepository<Brands> BrandsRepository =>  new EfCoreRepository<Brands>(this.DbContext);
        public IRepository<Users> UsersRepository => new EfCoreRepository<Users>(this.DbContext);
        public ITestRepository TestRepository => new CustomTestRepository(DbContext);
        // public  ITestRepository TestRepository =>new TestRepository(DbContext);
    }
}
