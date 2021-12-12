using AutoMapper;
using Microsoft.Extensions.Localization;
using Server.Core.Interfaces.Caching;
using Server.Model.Dto.Configuration;
using Server.Model.Dto.User;
using System;

namespace Server.Core.Interfaces.Repositories
{
    public interface IRequestContext
    {
        IRepositoriesCollection Repositories { get; }
        IServiceProvider ServiceProvider { get; }
        IDistributedCacheManager DistributedCacheManager { get; }
        IMapper Mapper { get; }
        SettingConfiguration Configuration { get; }
        IStringLocalizer<T> GetLocalResource<T>();
        ActiveUserContext ActiveUserContext { get; set; }
    }
}
