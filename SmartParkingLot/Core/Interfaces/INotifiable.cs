using SmartParkingLot.Domain.Events;

namespace SmartParkingLot.Core.Interfaces
{
    public interface INotifiable
    {
        void Notify(Object sender, LotFullEventArgs e);
    }
}