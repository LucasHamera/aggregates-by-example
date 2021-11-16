using Cinema.Exceptions;

namespace Cinema
{
    public record ReleaseCinemaSeat(ScreeningId ScreeningId, UserId BookerId, Seat Seat);

    internal class ReleaseCinemaSeatHandler
    {
        private readonly IScreeningRepository _screeningRepository;

        public ReleaseCinemaSeatHandler(IScreeningRepository screeningRepository)
        {
            _screeningRepository = screeningRepository;
        }

        public void Handle(ReleaseCinemaSeat command)
        {
            var screening = _screeningRepository.GetById(command.ScreeningId);
            if (screening is null)
                throw new NotFoundScreeningException(command.ScreeningId);
            screening.ReleaseSeat(command.BookerId, command.Seat);
        }
    }
}
