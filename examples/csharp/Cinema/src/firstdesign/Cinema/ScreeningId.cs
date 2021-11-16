namespace Cinema
{
    public record ScreeningId
    {
        private readonly Guid _id;

        public ScreeningId(): this(Guid.NewGuid())
        {
        }

        public ScreeningId(Guid id)
        {
            _id = id;
        }

        public static ScreeningId Of(Guid id)
            => new ScreeningId(id);

        public override string ToString()
            => _id.ToString();
    }
}
