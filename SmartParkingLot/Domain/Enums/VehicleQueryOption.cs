namespace SmartParkingLot.Domain.Enums
{
    /// <summary>
    /// Represents different options for querying vehicles in the parking lot.
    /// </summary>
    public enum VehicleQueryOption
    {
        /// <summary>
        /// No filter applied, all vehicles are displayed.
        /// </summary>
        All = 1,
        /// <summary>
        /// Filter by vehicle type.
        /// </summary>
        ByType = 2,
        /// <summary>
        /// Filter by license plate.
        /// </summary>    
        ByLicensePlate = 3,
        /// <summary>
        /// Order vehicles by entry time.
        /// </summary>
        ByEntryTime = 4
    }
}