using Microsoft.AspNetCore.Mvc;
using QRCoderWeb.Services;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QRCoderWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : ControllerBase
    { 
        [HttpGet]
        public IActionResult GenereteByteArray(string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest();

            return Ok(QrCodeGenerator.GenerateByteArray(url));
        }

        [HttpGet("img")]
        public HttpResponseMessage GetImage(string url)
        {
            if (string.IsNullOrEmpty(url))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            var img = QrCodeGenerator.GenereteImage(url);

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return result;
            }
        }
    }
}
