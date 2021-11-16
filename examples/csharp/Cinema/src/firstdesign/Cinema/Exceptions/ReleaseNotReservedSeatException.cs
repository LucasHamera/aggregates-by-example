namespace Cinema.Exceptions
{
    public class ReleaseNotReservedSeatException: Exception
    {
        public ReleaseNotReservedSeatException(UserId bookerId, Seat seat): base($"The seat {seat.Number} is not reserved by the booker {bookerId}.")
        {
            BookerId = bookerId;
            Seat = seat;
        }

        public UserId BookerId { get; }
        public Seat Seat { get; }
    }
}
