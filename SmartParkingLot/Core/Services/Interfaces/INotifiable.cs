using SmartParkingLot.Domain.Events;

namespace SmartParkingLot.Core.Services.Interfaces
{
    public interface INotifiable
    {
        void Notify(Object sender, LotFullEventArgs e);
    }
}