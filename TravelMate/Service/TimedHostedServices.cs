using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelMate.InterfaceFolder;
using TravelMate.ModelFolder.ContextFolder;

namespace TravelMate.Service
{
    public class TimedHostedServices : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceProvider _service;

        public TimedHostedServices(IServiceProvider service)
        {
            _service = service;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var autoEvent = new AutoResetEvent(true);
            // Create a timer that invokes CheckStatus after 10 second, 
            // and every 1 hour thereafter.
            _timer = new Timer(DoWork, autoEvent, 10000, 40000);
             return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            using var scope = _service.CreateScope();
            scope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
            var getGlobalCovidData = scope.ServiceProvider.GetRequiredService<IGetGlobalCovidData>();
            ApiInitialization.InitializeClient();
            await getGlobalCovidData.GetData();
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
