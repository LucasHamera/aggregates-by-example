using Cinema.Exceptions;

namespace Cinema
{
    public class SeatsReservation
    {
        private readonly List<Seat> _seats;

        public SeatsReservation(UserId bookerId)
        {
            BookerId = bookerId;
            _seats = new List<Seat>();
        }

        public UserId BookerId { get; }

        public IEnumerable<Seat> Seats
            => _seats;

        public int Size
            => _seats.Count;
        
        internal void Reserve(Seat seat)
        {
            if (ContainSeat(seat))
                throw new AttemptReserveABookedSeatException(seat);
            _seats.Add(seat);
        }

        internal void Release(Seat seat)
        {
            if (!ContainSeat(seat))
                throw new ReleaseNotReservedSeatException(BookerId, seat);
            _seats.Remove(seat);
        }

        internal bool ContainSeat(Seat seat)
            => _seats.Contains(seat);
    }
}
