using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OnlineTicariOtomasyon.Functions
{
    public static class KargoIslemleri
    {
        //https://www.codeproject.com/Articles/14403/Generating-Unique-Keys-in-Net
        public static string TakipKoduUret()
        {
            char[] chars = new char[62];
            chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[6];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(6);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}