namespace Cinema.Exceptions
{
    internal class NotFoundScreeningException: Exception
    {
        public NotFoundScreeningException(ScreeningId id): base($"Screening with id {id} not found.")
        {
            Id = id;
        }

        public ScreeningId Id { get; }
    }
}
