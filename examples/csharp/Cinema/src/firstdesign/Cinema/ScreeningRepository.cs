namespace Cinema
{
    public interface IScreeningRepository
    {
        public Screening? GetById(ScreeningId id);
        public void Add(Screening screening);
    }

    internal class ScreeningRepository: IScreeningRepository
    {
        private readonly IDictionary<ScreeningId, Screening> _screenings = new Dictionary<ScreeningId, Screening>();
        public Screening? GetById(ScreeningId id)
            => _screenings.TryGetValue(id, out var screening) ? screening : null;

        public void Add(Screening screening)
            => _screenings.Add(screening.Id, screening);
    }
}
