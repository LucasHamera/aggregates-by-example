namespace Cinema.Exceptions
{
    public class AttemptReserveABookedSeatException: Exception
    {
        public AttemptReserveABookedSeatException(Seat seat): base($"Seat {seat.Number} is reserved.")
        {
            Seat = seat;
        }

        public Seat Seat { get; }
    }
}
