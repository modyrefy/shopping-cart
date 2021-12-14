using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Common.Helpers;
using Server.Core;
using Server.Core.Interfaces.Caching;
using Server.Core.Mapper;
using Server.Infrastructure.Data;
using Server.Infrastructure.Provider.Caching;
using Server.Infrastructure.Repositories.EFCore;
using Server.Model.Interfaces.Caching;
using Server.Model.Interfaces.Context;
using Server.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Common.Extensions
{
    public static class CommonServiceExtensions
    {
        public static IServiceCollection AddControllersExtenison(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                // options.Filters.Add(new MySampleActionFilter());
            });

            return services;
        }
        public static IServiceCollection AddAuthenticationExtenison(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    //OnMessageReceived = context =>
                    //{
                    //    context.Token = context.Request.Cookies["test-auth"];
                    //    return Task.CompletedTask;
                    //},
                    OnTokenValidated = context =>
                    {
                        if (context.Principal?.Identity == null)
                        {
                            context.Fail("Unauthorized");
                        }
                        // var apiKey = context.Request.Headers["x-APIKey"];
                        // if (string.IsNullOrEmpty(apiKey))
                        // {
                        //     context.Fail(WebsiteConstants.Common.Unauthorized);
                        // }
                        //  var c = context.Principal;
                       

                        
                        context.HttpContext.RequestServices.GetRequiredService<IRequestContext>().ActiveUserContext = TokenContextHelper.Instance.GetCurrentUserFromTokenContext(context) ;
                        //Add claim if they are
                        //    var claims = new List<Claim>
                        //{
                        //    new Claim(ClaimTypes.Role, "superadmin")
                        //};
                        //    var appIdentity = new ClaimsIdentity(claims);
                        //    context.Principal.AddIdentity(appIdentity);
                        //ClaimsPrincipal userPrincipal = context.Principal;
                        //var px = HttpContext.Items["User"];
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }

        public static IServiceCollection AddConfigurationExtenison(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new Model.Dto.Configuration.SettingConfiguration() {
                Jwt = new Model.Dto.Configuration.Jwt(),
                ConnectionStrings=new Model.Dto.Configuration.ConnectionStrings(),
            };
            configuration.Bind("ConnectionStrings", config.ConnectionStrings);
            configuration.Bind("Jwt", config.Jwt);
            services.AddSingleton(config);
            return services;
        }
        public static IServiceCollection AddSqlCachingExtenison(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("CacheConnectionString");
                options.SchemaName = "dbo";
                options.TableName = "TestCache";
                options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(10);
            });
            services.AddScoped(typeof(IDistributedCacheManager), typeof(DistributedCacheProvider));
            return services;
        }
        public static IServiceCollection AddMemoryCachingExtenison(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped(typeof(ICacheManager), typeof(MemoryCacheProvider));
            //services.AddScoped(typeof(ICacheManager), typeof(RedisCacheProvider));
            return services;
        }
        public static IServiceCollection AddRedisCachingExtenison(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = "localRedis_";
            });
            return services;
        }
        public static IServiceCollection AddRepositoryExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DefaultDBContext, DefaultDBContext>();
            services.AddDbContext<DefaultDBContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });
            services.AddScoped(typeof(IRepositoriesCollection), typeof(EfCoreRepositoryCollection));

            return services;
        }
        public static IServiceCollection AddLocalizationResourcesExtesnion(this IServiceCollection services)
        {
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en-US"),
                        new CultureInfo("ar-AE"),
                        new CultureInfo("fr-Fr"),
                        };
                    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;
                    options.RequestCultureProviders = new IRequestCultureProvider[] { new AcceptLanguageHeaderRequestCultureProvider() };

                });
            return services;
        }
        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Server.Api", Version = "v1" });
            });
            return services;
        }
        public static IServiceCollection AddRequestContextExtension(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRequestContext), typeof(RequestContext));

            return services;
        }
        public static IServiceCollection AddMappingProfileExtension(this IServiceCollection services)
        {
            services.AddAutoMapper(r => { r.AddProfile(new MappingProfile()); });
            return services;
        }

    }
}
