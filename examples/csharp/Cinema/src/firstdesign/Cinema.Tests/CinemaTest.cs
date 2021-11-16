using Xunit;
using System;
using System.Linq;
using Cinema.Exceptions;
using FluentAssertions;

namespace Cinema.Tests
{
    public class CinemaTest
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly ITimeProvider _timeProvider;
        private readonly BookCinemaSeatHandler _bookCinemaSeatHandler;
        private readonly ReleaseCinemaSeatHandler _releaseCinemaSeatHandler;

        public CinemaTest()
        {
            _screeningRepository = new ScreeningRepository();
            _timeProvider = new TimeProvider();
            _bookCinemaSeatHandler = new BookCinemaSeatHandler(_screeningRepository, _timeProvider);
            _releaseCinemaSeatHandler = new ReleaseCinemaSeatHandler(_screeningRepository);
        }
        
        [Fact]
        public void FirstReservationShouldBeAdded()
        {
            var screening = AddExampleScreening();
            var bookerId = new UserId();
            var seat = new Seat("1A");
            var command = new BookCinemaSeat(screening.Id, bookerId, seat);

            _bookCinemaSeatHandler.Handle(command);

            screening
                .Reservations
                .Count()
                .Should()
                .Be(1);
        }

        [Fact]
        public void SixthReservationShouldNotAdded()
        {
            var screening = AddExampleScreening();
            var bookerId = new UserId();
            var createCommand = () => new BookCinemaSeat(screening.Id, bookerId, GenerateSeat());

            _bookCinemaSeatHandler.Handle(createCommand());
            _bookCinemaSeatHandler.Handle(createCommand());
            _bookCinemaSeatHandler.Handle(createCommand());
            _bookCinemaSeatHandler.Handle(createCommand());
            _bookCinemaSeatHandler.Handle(createCommand());

            var act = () => _bookCinemaSeatHandler.Handle(createCommand());

            act.Should().Throw<TooBigReservationException>();
        }

        [Fact]
        public void BookAlreadyReservedSeatShouldNotBookedAgain()
        {
            var screening = AddExampleScreening();
            var bookerId = new UserId();
            var seat = new Seat("1A");
            var command = new BookCinemaSeat(screening.Id, bookerId, seat);
            _bookCinemaSeatHandler.Handle(command);

            var act = () => _bookCinemaSeatHandler.Handle(command);

            act.Should().Throw<AttemptReserveABookedSeatException>();
        }

        [Fact]
        public void TooLateReservationShouldBeNotAdded()
        {
            var screening = AddExampleScreeningStartedNow();
            var bookerId = new UserId();
            var seat = new Seat("1A");
            var command = new BookCinemaSeat(screening.Id, bookerId, seat);

            var act = () => _bookCinemaSeatHandler.Handle(command);

            act.Should().Throw<TooLateReservationException>();
        }

        [Fact]
        public void ReleaseBookedSeatShouldReleased()
        {
            var screening = AddExampleScreening();
            var bookerId = new UserId();
            var seat = new Seat("1A");
            var command = new ReleaseCinemaSeat(screening.Id, bookerId, seat);
            var reservation = screening.BookSeat(bookerId, seat, _timeProvider.Now);

            _releaseCinemaSeatHandler.Handle(command);

            reservation
                .Seats
                .Should()
                .BeEmpty();
        }

        [Fact]
        public void ReleaseNotBookedSeatShouldNotReleased()
        {
            var screening = AddExampleScreening();
            var bookerId = new UserId();
            var seat = new Seat("1A");
            var command = new ReleaseCinemaSeat(screening.Id, bookerId, seat);

            var act = () => _releaseCinemaSeatHandler.Handle(command);

            act.Should().Throw<ReleaseNotReservedSeatException>();
        }

        private Screening AddExampleScreening()
            => AddExampleScreening(DateTime.UtcNow.AddMinutes(-10));

        private Screening AddExampleScreeningStartedNow()
            => AddExampleScreening(DateTime.UtcNow);

        private Screening AddExampleScreening(DateTime now)
        {
            var screening = Screening.Create(new ScreeningId(), now);
            _screeningRepository.Add(screening);
            return screening;
        }

        private Seat GenerateSeat()
            => new Seat(Guid.NewGuid().ToString("N"));
    }
}