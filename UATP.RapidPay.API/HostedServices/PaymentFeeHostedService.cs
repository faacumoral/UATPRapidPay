using UATP.RapidPay.Core.PaymentFee;

namespace UATP.RapidPay.API.HostedServices
{
    public class PaymentFeeHostedService : IDisposable, IHostedService
    {
        private Timer _timer = null;

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(CalculateFee, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        private void CalculateFee(object? state)
        {
            UFE.Instance.CalculateNewFee();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
