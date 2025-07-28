using SmartParkingLot.Domain.Enums;

namespace SmartParkingLot.Domain.Models
{
    public abstract class Vehicle
    {
        public required string LicensePlate { get; set; }
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// Represents the type of vehicle.
        /// This property is abstract and must be implemented by derived classes.
        /// No setter is provided to ensure that the type is defined by the specific vehicle class.
        /// </summary>
        public abstract VehicleType Type { get; }

        public Vehicle(string licensePlate, DateTime? entryTime=null)
        {
            LicensePlate = licensePlate;
            EntryTime = entryTime?? DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Type} - {LicensePlate} - Entry Time: {EntryTime}";
        }
             
    }
}