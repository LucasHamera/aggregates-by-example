using Cinema.Exceptions;

namespace Cinema
{
    public record BookCinemaSeat(ScreeningId ScreeningId, UserId BookerId, Seat Seat);

    internal class BookCinemaSeatHandler
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly ITimeProvider _timeProvider;

        public BookCinemaSeatHandler(IScreeningRepository screeningRepository, ITimeProvider timeProvider)
        {
            _screeningRepository = screeningRepository;
            _timeProvider = timeProvider;
        }

        public void Handle(BookCinemaSeat command)
        {
            var screening = _screeningRepository.GetById(command.ScreeningId);
            if (screening is null)
                throw new NotFoundScreeningException(command.ScreeningId);
            screening.BookSeat(command.BookerId, command.Seat, _timeProvider.Now);
        }
    }
}
