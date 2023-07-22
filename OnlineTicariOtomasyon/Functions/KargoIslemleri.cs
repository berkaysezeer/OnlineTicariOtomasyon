using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using ZXing;

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

        public static string QrKodOlustur(string kargoUrl, string takipKodu, HttpServerUtilityBase Server)
        {
            string folderPath = "/Images/QrKod/";
            string imagePath = $"/Images/QrKod/{takipKodu}.png";

            if (!Directory.Exists(Server.MapPath(folderPath)))
            {
                Directory.CreateDirectory(Server.MapPath(folderPath));
            }

            var barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            var result = barcodeWriter.Write(kargoUrl);

            string barcodePath = Server.MapPath(imagePath);
            var barcodeBitmap = new Bitmap(result);
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return imagePath;
        }
    }
}