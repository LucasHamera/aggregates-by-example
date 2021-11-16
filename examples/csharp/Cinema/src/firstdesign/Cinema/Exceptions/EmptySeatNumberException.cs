namespace Cinema.Exceptions
{
    internal class EmptySeatNumberException: Exception
    {
        public EmptySeatNumberException(): base("Seat number cannot be empty.")
        {
            
        }
    }
}
