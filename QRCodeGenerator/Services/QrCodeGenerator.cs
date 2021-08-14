using System.Drawing;
using System.IO;
using QRCoder;

namespace QRCoderWeb.Services
{
    public static class QrCodeGenerator
    {
        public static Bitmap GenereteImage(string url)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(10);

            return qrCodeImage;
        }

        public static byte[] GenerateByteArray(string url)
        {
            var image = GenereteImage(url);
            return ImageToByte(image);
        }

        private static byte[] ImageToByte(Image img)
        {
            using var stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
