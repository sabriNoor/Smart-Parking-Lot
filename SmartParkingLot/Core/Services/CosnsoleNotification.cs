using Microsoft.Extensions.Logging;
using SmartParkingLot.Core.Services.Interfaces;
using SmartParkingLot.Domain.Events;

namespace SmartParkingLot.Core.Services
{
    class ConsoleNotification : INotifiable
    {
        
        ILogger<ConsoleNotification> _logger;
        public ConsoleNotification(ILogger<ConsoleNotification> logger)
        {
            _logger = logger;
        }
        public void Notify(Object sender, LotFullEventArgs e)
        {
            string message = $"Console Notification: {e.Message} at {e.Timestamp}";
            Console.WriteLine(message);
            _logger.LogInformation(message);
        }
    }
}