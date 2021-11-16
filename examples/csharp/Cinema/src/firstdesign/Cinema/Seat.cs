using Cinema.Exceptions;

namespace Cinema
{
    public class Seat: IEquatable<Seat>
    {
        public Seat(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new EmptySeatNumberException();
            Number = number;
        }

        public string Number { get; }

        public bool Equals(Seat? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Number == other.Number;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Seat)obj);
        }

        public override int GetHashCode() 
            => Number.GetHashCode();

        public static bool operator==(Seat left, Seat right) 
            => left.Equals(right);

        public static bool operator !=(Seat left, Seat right)
            => !left.Equals(right);

        public override string ToString()
            => Number;
    }
}
