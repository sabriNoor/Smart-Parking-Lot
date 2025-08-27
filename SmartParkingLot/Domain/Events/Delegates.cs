namespace SmartParkingLot.Domain.Events
{
    public delegate void LotFullHandler<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs;
}
