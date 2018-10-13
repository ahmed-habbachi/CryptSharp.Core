using System;
using System.Text;
using System.Security.Cryptography;
using CryptSharp.Core.Internal;
using CryptSharp.Core.Utility;

namespace CryptSharp.Core
{
    public class JoomlaCrypter : Crypter
    {
        /// <summary>
        /// Verify a hash against a string.
        /// </summary>
        /// <param name="password">Input to be verified / compared to the original</param>
        /// <param name="hash">Original hash</param>
        /// <returns></returns>
        public new bool CheckPassword(string password, string hash)
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

        public new string Crypt(string password, string salt)
        {
            Check.Null("password", password);
            Check.Null("salt", salt);
            if (salt.Length != 32 && salt.Contains(":"))
            {
                salt = salt.Split(':')[1];
            }
            var hash = Crypter.MD5.Crypt(password + salt);
            return $"{hash}:{salt}";
        }

        public override bool CanCrypt(string salt)
        {
            var split = salt.Split(':');

            if (split.Length != 2)
            {
                return false;
            }

            if (MD5.CanCrypt(split[0]))
            {
                return true;
            }

            return false;
        }

        public override string Crypt(byte[] password, string salt)
        {
            var strpassword = System.Text.Encoding.UTF8.GetString(password);
            return Crypt(strpassword, salt);
        }

        public override string GenerateSalt(CrypterOptions options)
        {
            return Base64Encoding.UnixMD5.GetString(Security.GenerateRandomBytes(31));
        }
    }
}
