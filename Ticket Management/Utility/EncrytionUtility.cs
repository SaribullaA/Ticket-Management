using System.Text;

namespace Ticket_Management.Utility
{
    public class EncrytionUtility
    {
        public static string Base64Encode(string data)
        {
            string encodedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
            return encodedData;
        }
    }
}
