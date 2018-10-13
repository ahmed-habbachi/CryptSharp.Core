using System;
using System.Text;
using System.Security.Cryptography;

namespace CryptSharp.Core
{
    public class JoomlaCrypter
    {
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

                string hashOfInput = Crypter.MD5.Crypt(password);
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                return (0 == comparer.Compare(hashOfInput, hash));
            }
        }
    }
}
