using System.Security.Cryptography;
using System.Text;

namespace TaskManagerBackend.Utilities
{
    public class Utility
    {
        /*A function to hash a provided string
         * Parameters: a string
         * Return: a hashed string
        */
        public static string ComputeSha256Hash(string? rawData)
        {
            if (rawData == null) return string.Empty;

            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        /*A function to check if the key of a user is already expired
         * Parameters: User object
         * Return: true if the date expired
        static bool DateExpired(User user)
        {
            if (DateTime.Compare(DateTime.UtcNow, DateTime.Parse(user.ExpirationDate)) == -1) return false;
            return true;
        }*/
    }
}
