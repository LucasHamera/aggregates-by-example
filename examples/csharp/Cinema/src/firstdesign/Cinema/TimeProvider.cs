namespace Cinema
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }

    internal class TimeProvider: ITimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
