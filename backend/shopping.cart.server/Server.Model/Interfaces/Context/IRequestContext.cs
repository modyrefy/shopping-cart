using AutoMapper;
using Microsoft.Extensions.Localization;
using Server.Core.Interfaces.Caching;
using Server.Model.Dto.Configuration;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Repositories;
using System;

namespace Server.Model.Interfaces.Context
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
