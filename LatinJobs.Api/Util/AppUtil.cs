using System.Security.Cryptography;
using System.Text;

namespace LatinJobs.Api.Util
{
    public static class AppUtil
    {
        public static string ToSha256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x"));
                }
                return builder.ToString();
            }
        }
    }
}
