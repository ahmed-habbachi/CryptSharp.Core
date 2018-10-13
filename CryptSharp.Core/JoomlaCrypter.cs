using System;
using System.Text;
using System.Security.Cryptography;
using CryptSharp.Core.Internal;

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
            Check.Null("password", password);
            Check.Null("hash", hash);

            var hashPair = hash.Split(':');
            if (hashPair.Length != 2)
            {
                throw new Exception("Incompatible hash!");
            }

            string hashOfInput = Crypter.MD5.Crypt(password + hashPair[1]);
            return string.Equals(hashOfInput, hashPair[0], StringComparison.InvariantCultureIgnoreCase);
        }

        public string Crypt(string password, string salt)
        {
            Check.Null("password", password);
            Check.Null("salt", salt);
            var hash = Crypter.MD5.Crypt(password + salt);
            return $"{hash}:{salt}";
        }
    }
}
