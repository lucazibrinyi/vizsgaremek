using System.Security.Cryptography;
using System.Text;

namespace easyquoteDesktop
{

    /**
    * In this file:
    * 
    * @Md5hash
    * 
    * **/

    public static class HashStringMd5
    {
        public static string Md5hash(string text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
