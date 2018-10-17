using FundaApp.Models.Conf;
using FundaApp.Service;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FundaApp.ScheduledTasks
{
    public class ImportTask : SchedulerHostedService
    {
        private readonly IFundaService service;
        private readonly int RefreshTimeInMinutes;

        public ImportTask(IFundaService _service, IOptions<Settings> _settings)
        {
            service = _service;
            RefreshTimeInMinutes = _settings.Value.RefreshTimeInMinutes;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // every RefreshTimeInMinutes minutes data is refreshed (imported from the Funda API)
                await service.ImportData();
                await Task.Delay(TimeSpan.FromMinutes(RefreshTimeInMinutes), cancellationToken);
            }
        }
    }
}
