using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Server.Common.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApplicationExtensions(this IApplicationBuilder app)//, ILoggerFactory loggerFactory
        {
            var localizeOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            if (localizeOptions != null)
            {
                app.UseRequestLocalization(localizeOptions.Value);
            }

            return app;
        }
    }
}
