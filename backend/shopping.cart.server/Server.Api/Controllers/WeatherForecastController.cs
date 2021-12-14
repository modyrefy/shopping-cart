using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Server.Core.BaseClasses;
using Server.Core.Interfaces.Caching;
using Server.Model.Dto.User;
using Server.Model.Interfaces.Caching;
using Server.Model.Interfaces.Context;
using Server.Resources.Resources;
using Server.Services.Processor.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRequestContext _requestContext;
        private readonly ICacheManager _cacheManager;
        private readonly IDistributedCacheManager _distributedCacheManager;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRequestContext requestContext, ICacheManager cacheManager, IDistributedCacheManager distributedCacheManager)
        {
            _logger = logger;
            _requestContext = requestContext;
            _cacheManager = cacheManager;
            _distributedCacheManager= distributedCacheManager;
        }
        private List<int> GetIdList()
        {
            return new List<int>() { 1, 2, 3, 45, 6, 7 };
        }
        [HttpGet]
        [Route("testid")]
        [AllowAnonymous]
        public async Task<ResponseBase<ActiveUserContext>> TestUser()
        {
            AuthincateUserProcessor processor = null;
            using ((processor = new AuthincateUserProcessor(_requestContext)) as IDisposable)
            {
                var px = _requestContext.ActiveUserContext;
                var result = await processor.DoProcessAsync(new UserAuthenticateRequestModel()
                {
                    Username = "democustomer",
                    Password = "12345"
                });
                if (result != null && result.Result != null)
                {
                    _distributedCacheManager.Set<ActiveUserContext>(nameof(ActiveUserContext), result.Result);
                }
                return result;
            }
            //var px = _requestContext.ActiveUserContext;
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<WeatherForecast> Get()
        {
            _distributedCacheManager.ResetCache("test");
         var res=   _distributedCacheManager.GetOrSet<List<int>>("test", () => GetIdList(), null);
            _cacheManager.GetOrSet<List<int>>("test", () => GetIdList(), null);
            _cacheManager.GetOrSet<List<int>>("test", () => GetIdList(), null);
            var locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var pt1 = _requestContext.Repositories.TestRepository.SearchAsync("abc", "123").GetAwaiter().GetResult();
            //var px=collection.BrandsRepository.GetAll();
            //var px1 = collection.BrandsRepository.GetAll();
            var px = _requestContext.Repositories.BrandsRepository.GetAll();
            var locals = this._requestContext.GetLocalResource<Abc>();
            var abx = locals.GetAllStrings(false);
            var abx1 = locals.GetString("abc");
            var abx2 = locals.GetString("hyza");
            //var locals = this.RequestContext.GetLocalResource<CancelledProjectsResources>();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
