using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Server.Core.Interfaces.Caching;
using Server.Model.Dto.Configuration;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Context;
using Server.Model.Interfaces.Repositories;
using System;

namespace Server.Core
{
    public class RequestContext : IRequestContext
    {
        public IRepositoriesCollection Repositories { get; }
        public IServiceProvider ServiceProvider { get; }
        public ActiveUserContext ActiveUserContext  { get ; set ; }
        public IDistributedCacheManager DistributedCacheManager { get ; }
        public IMapper Mapper { get; }
       public SettingConfiguration Configuration { get; }
        public IStringLocalizer<T> GetLocalResource<T>()
        {
            return ServiceProvider.GetRequiredService<IStringLocalizer<T>>();
        }
        public RequestContext(
            IRepositoriesCollection repositories,
            IServiceProvider serviceProvider,
            IDistributedCacheManager distributedCacheManager,
            IMapper mapper,
            SettingConfiguration configuration
            )
        {
            Repositories = repositories;
            ServiceProvider = serviceProvider;
            DistributedCacheManager = distributedCacheManager;
            Mapper = mapper;
            Configuration = configuration;
        }


        //public IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, string value, bool exact = false)
        //{
        //    throw new NotImplementedException();
        //}
        //public IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, decimal? value)
        //{
        //    throw new NotImplementedException();
        //}
        //public IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, int? value)
        //{
        //    throw new NotImplementedException();
        //}
        //public IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, bool? value)
        //{
        //    throw new NotImplementedException();
        //}
        //public IQueryable<T> CreateExpression<T>(IQueryable<T> recordSet, string columnName, List<int> values)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
