using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TravelMate.ModelFolder.ContextFolder;

namespace TravelMate.Service
{
    public class TimedHostedServices : IHostedService, IDisposable
    {
        private Timer _timer;
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var autoEvent = new AutoResetEvent(true);
            // Create a timer that invokes CheckStatus after 20 second, 
            // and every 1 hour thereafter.
            _timer = new Timer(CallHttpData, autoEvent, 20000, 3600000);
            return Task.CompletedTask;
        }
        private async void CallHttpData(object state)
        {
            ApiInitialization.InitializeClient();
            var covidData = new GetGlobalCovidData();
            await covidData.GetData();
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
