#region License
/*
CryptSharp
Copyright (c) 2010-2014 James F. Bellinger <http://www.zer7.com/software/cryptsharp>

Edited by: Ahmed HABBACHI

Permission to use, copy, modify, and/or distribute this software for any
purpose with or without fee is hereby granted, provided that the above
copyright notice and this permission notice appear in all copies.

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
*/
#endregion

using CryptSharp.Core.Utility;
using System;

namespace CryptSharp.Core.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WordPress uses portable PHPass passwords.");
            string wpPassword = Crypter.Phpass.Crypt("HelloWorld");
            Console.WriteLine("The password HelloWorld crypts to: {0}", wpPassword);
            Console.WriteLine("The above statement is {0}.", Crypter.CheckPassword("HelloWorld", wpPassword));
            Console.WriteLine("It is {0} that the password is OpenSesame.", Crypter.CheckPassword("OpenSesame", wpPassword));
            Console.WriteLine();

            //Joomla first type
            var result1 = Crypter.MD5.Crypt("270919");
            System.Console.WriteLine($"Unsalted MD5 type: {result1 == "5a26742be1d1a6382f030ce9002c716c"}");
            Console.WriteLine();

            var result2 = Crypter.MD5.Crypt("270919", new CrypterOptions(){
                {CrypterOption.Variant, MD5CrypterVariant.Unsalted}
            });
            System.Console.WriteLine($"Unsalted MD5 type using variant: {result2 == "5a26742be1d1a6382f030ce9002c716c"}");
            Console.WriteLine();

            var result3 = Crypter.CheckPassword("270919", "5a26742be1d1a6382f030ce9002c716c");
            System.Console.WriteLine($"Check md5 password using the Crypter checkPassword: {result3}");
            Console.WriteLine();

            var result4 = Crypter.CheckPassword("628157", "241119dbb64cdcdfd7e6a2ed7c53dcc0:npce2HRmjoF4KvW3GlGvrZOLtmcv8lKC");
            System.Console.WriteLine($"Check Joomla password using the Crypter checkPassword: {result4}");
            Console.WriteLine();

            //BaseEncoding.TestVectors.Test();
            //BlowfishTest.TestVectors.Test();
            //Pbkdf2Test.TestVectors.Test();
            //SCryptTest.TestVectors.Test();
            //CrypterTest.TestVectors.Test();

            //Console.WriteLine();

            //Console.WriteLine("Now a simple BCrypt demo.");
            //string crypt = CryptSharp.Core.Crypter.Blowfish.GenerateSalt();
            //Console.WriteLine("Our salt is: {0}", crypt);

            //for (int i = 0; i < 10; i++)
            //{
            //    // Try this against PHP's crypt('password', 'output of this function').
            //    crypt = CryptSharp.Core.Crypter.Blowfish.Crypt("Hello World!", crypt);
            //    Console.WriteLine(crypt);
            //}

            //Console.WriteLine();

            //Console.WriteLine("CryptSharp can also generate Apache-compatible htpasswd MD5...");
            //Console.WriteLine("   (it does require an additional parameter)");
            //Console.WriteLine("The password HelloWorld crypts to: {0}",
            //    Crypter.MD5.Crypt("HelloWorld", new CrypterOptions
            //        {
            //            { CrypterOption.Variant, MD5CrypterVariant.Apache }
            //        }));

            //Console.WriteLine();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
