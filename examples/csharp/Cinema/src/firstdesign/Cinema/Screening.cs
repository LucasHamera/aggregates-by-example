using Cinema.Exceptions;

namespace Cinema
{
    public class Screening
    {
        // const, no policy
        private const int MaxReservationSeats = 5;

        private readonly ISet<SeatsReservation> _reservations = new HashSet<SeatsReservation>();
        private readonly DateTime _startDate;

        private Screening(ScreeningId id, DateTime startDate)
        {
            Id = id;
            _startDate = startDate;
        }

        public ScreeningId Id { get; }

        public IEnumerable<SeatsReservation> Reservations
            => _reservations;
        
        public static Screening Create(ScreeningId id, DateTime startDate)
            => new Screening(id, startDate);

        public SeatsReservation BookSeat(UserId bookerId, Seat seat, DateTime now)
        {
            if (IsTooLate(now))
                throw new TooLateReservationException();

            if (HasReservedSeat(seat))
                throw new AttemptReserveABookedSeatException(seat);

            var reservation = _reservations
                .FirstOrDefault(x => x.BookerId.Equals(bookerId)) ?? CreateNewReservation(bookerId);

            if (IsTooBig(reservation))
                throw new TooBigReservationException(MaxReservationSeats);

            reservation.Reserve(seat);
            return reservation;
        }

        public SeatsReservation ReleaseSeat(UserId bookerId, Seat seat)
        {
            var reservation = GetReservation(bookerId) ?? new SeatsReservation(bookerId);
            reservation.Release(seat);
            return reservation;
        }

        public SeatsReservation? GetReservation(UserId bookerId)
            => _reservations.FirstOrDefault(r => r.BookerId.Equals(bookerId));

        private bool IsTooLate(DateTime now)
            => _startDate >= now.AddMinutes(-5);

        private bool HasReservedSeat(Seat seat)
            => _reservations.Any(r => r.ContainSeat(seat));

        private SeatsReservation CreateNewReservation(UserId bookerId)
        {
            var reservation = new SeatsReservation(bookerId);
            _reservations.Add(reservation);
            return reservation;
        }

        private bool IsTooBig(SeatsReservation reservation)
            => reservation.Size >= MaxReservationSeats;
    }
}
