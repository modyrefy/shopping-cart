using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Dto.Caching
{
    public class CacheOptions
    {
        public TimeSpan AbsoluteExpirationRelativeToNow { get; set; } = TimeSpan.FromMinutes(600);
        public DateTimeOffset AbsoluteExpirationMinutes { get; set; } = DateTimeOffset.MaxValue;
        public TimeSpan SlidingExpirationMinutes { get; set; } = TimeSpan.FromMinutes(60);
    }
}
