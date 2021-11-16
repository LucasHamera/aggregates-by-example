namespace Cinema.Exceptions
{
    internal class TooBigReservationException: Exception
    {
        public TooBigReservationException(int maxReservationSeats): base($"The reservation is too large, the maximum number of places is {maxReservationSeats}.")
        {   
            MaxReservationSeats = maxReservationSeats;
        }

        public int MaxReservationSeats { get;  }
    }
}
