namespace SmartParkingLot.Core.Interfaces
{
    public interface IVehicleQueryView
    {
        void RenderVehiclesByType();
        void RenderVehiclesByLicensePlate();
        void RenderVehiclesByEntryTime();
        void RenderAllVehicles();
    }

}