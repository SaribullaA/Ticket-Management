namespace Ticket_Management.PlatformException
{
    public class PlatformException : IOException
    {
        public int StatusCode { get; set; }

        public PlatformException(int statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }
    }
}
