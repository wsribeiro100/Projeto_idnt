using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Xunit;

namespace UnitTests
{
    public class DistributedCacheFixture : IDisposable
    {
        public IDistributedCache Cache { get; private set; }
        private ServiceProvider _provider;

        public DistributedCacheFixture()
        {
            var services = new ServiceCollection();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
                options.InstanceName = "TestInstance:";
            });

            _provider = services.BuildServiceProvider();
            Cache = _provider.GetRequiredService<IDistributedCache>();
        }

        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}
