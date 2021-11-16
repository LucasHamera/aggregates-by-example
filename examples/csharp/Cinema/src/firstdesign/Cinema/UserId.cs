namespace Cinema
{
    public record UserId
    {
        private readonly Guid _id;

        public UserId(): this(Guid.NewGuid())
        {
        }

        public UserId(Guid id)
        {
            _id = id;
        }

        public static UserId Of(Guid id)
            => new UserId(id);
    }
}
