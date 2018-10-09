using System;
using System.Text;
using System.Security.Cryptography;

namespace CryptSharp.Core
{
    public class JoomlaCrypter
    {
        public MD5 MD5Hash { get; set; }

        public JoomlaCrypter()
        {
            MD5Hash = MD5.Create();
        }

        /// <summary>
        /// Generate a MD5 hash
        /// </summary>
        /// <param name="md5Hash">MD5 Cryptography</param>
        /// <param name="password">Input to hash</param>
        /// <returns>md5 hashed input</returns>
        public string Crypt(string password)
        {
            byte[] data = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary>
        /// Verify a hash against a string.
        /// </summary>
        /// <param name="password">Input to be verified / compared to the original</param>
        /// <param name="hash">Original hash</param>
        /// <returns></returns>
        public bool CheckPassword(string password, string hash)
        {
            if (hash.StartsWith("$P$"))
            {
                return Crypter.CheckPassword(password, hash);
            }
            else
            {
                var passwordPair = hash.Split(':');

                if (passwordPair.Length == 2)
                {
                    hash = passwordPair[0];
                    password = password + passwordPair[1];
                }

                string hashOfInput = Crypt(password);
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                return (0 == comparer.Compare(hashOfInput, hash));
            }
        }
    }
}
