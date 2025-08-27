using SmartParkingLot.Domain.Enums;

namespace SmartParkingLot.Domain.Exceptions
{
    public class ParkingLotException : Exception
    {
        /// <summary>
        /// Represents the operation that caused the exception.
        /// If null, the operation is unknown; this allows the exception to be thrown outside of a specific operation context, making the code more flexible.
        /// If <see cref="OperationType.CheckIn"/>, the exception occurred during a check-in operation.
        /// If <see cref="OperationType.CheckOut"/>, the exception occurred during a check-out operation.
        /// </summary>
        public OperationType? Operation { get; }

        public ParkingLotException(OperationType? operation=null)
        : base()
        {
            Operation = operation;
        }
        public ParkingLotException(string message, OperationType? operation=null)
        : base(message)
        {
            Operation = operation;
        }
        public ParkingLotException(string message, Exception innerException, OperationType? operation=null)
        : base(message, innerException)
        {
            Operation = operation;
        }

        public override string Message
        {
            get
            {
                return Operation switch
                {
                    OperationType.CheckIn =>  $"{base.Message} Can't check in: no capacity available.",
                    OperationType.CheckOut => $"{base.Message} Can't check out: license plate is unknown.",
                    _ => base.Message
                };
                
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Operation: {Operation?.ToString() ?? "None"}";
        }
    }
}