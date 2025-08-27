namespace SmartParkingLot.Domain.Events
{
    public class LotFullEventArgs : EventArgs
    {
        public string Message { get; set; } = String.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public LotFullEventArgs() { }
        public LotFullEventArgs(string message)
        {
            Message = message;
        }
        public LotFullEventArgs(string message, DateTime timestamp)
        {
            Message = message;
            Timestamp = timestamp;
        }
        
        public override string ToString()
        {
            return $"@{Timestamp}: {Message}";
        }

    }
}
