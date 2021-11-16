namespace Cinema.Exceptions
{
    public class TooLateReservationException: Exception
    {
        public TooLateReservationException(): base("Reservation is made too late.")
        {
            
        }
    }
}
