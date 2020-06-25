using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DeepLearning.Predication;
using Microsoft.AspNetCore.Mvc;

namespace DeepLearning.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassifierController : ControllerBase
    {
        private readonly Predicator _predicator = new Predicator("/Users/martinstanek/Desktop/fruitImageClassifier.zip");

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return await Task.FromResult(Ok("hello"));
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> ClasisifyAsync()
        {
            var file = Request.Form.Files[0];
            
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                using var ms = new MemoryStream();

                file.CopyTo(ms);

                var fileBytes = ms.ToArray();

                var whatIsOnImage = _predicator.WhatIsOnImage(fileBytes, fileName);

                return await Task.FromResult(Ok(whatIsOnImage));
            }

            return BadRequest();
        }
    }
}