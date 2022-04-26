using Microsoft.Extensions.Hosting;
using System.Threading.Tasks.Dataflow;

namespace MobilivaCase.HostedService
{
    public class EmailHostedService : IHostedService, IDisposable
    {
        private Task? _sendTask;
        private CancellationToken? cancellationToken;
        private readonly BufferBlock<EmailModel> _mailQueue;
        private readonly IEmailSender _mailSender;
        
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
