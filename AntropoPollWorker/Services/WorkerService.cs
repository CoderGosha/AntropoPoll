using AntropoPollWebApi.Core.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AntropoPollWorker.Services
{
    public class WorkerService : BackgroundService
    {
        private Thread _thread;
        private CalcService _calcService;

        public WorkerService(CalcService calcService)
        {
            _calcService = calcService;
            // _calcService = new CalcService(ConfigService.GetOpcConfig());
        }

        private ManualResetEvent WorkerCancelled = new ManualResetEvent(false);

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            WorkerCancelled.Set();
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Run(CalcAsync);
            }
        }

        private async Task CalcAsync()
        {
            try
            {
                await _calcService.CalcResultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            finally
            {
                await Task.Delay(1000);
               // Thread.Sleep(1000);
            }
        }
    }
}
