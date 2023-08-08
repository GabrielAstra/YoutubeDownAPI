using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace APIDown.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaixarController : ControllerBase
    {
        private readonly YoutubeClient _youtube;

        public BaixarController()
        {
            _youtube = new YoutubeClient();
        }

        [HttpGet]
        public async Task<IActionResult> BaixarVideo(string videoUrl)
        {
            try
            {
                var videoInformacao = await _youtube.Videos.GetAsync(videoUrl);
                var videoTitulo = videoInformacao.Title;

                var diretorioDeSaida = @"/home/gabriel/Desktop/videos"; 
                var diretorioSaida = Path.Combine(diretorioDeSaida, $"{videoTitulo}.mp4");

                Directory.CreateDirectory(diretorioDeSaida);

                var informacaoUrl = await _youtube.Videos.Streams.GetManifestAsync(videoUrl);
                var infoUr = informacaoUrl.GetMuxedStreams().GetWithHighestVideoQuality();

                if (infoUr != null)
                {
                    await _youtube.Videos.Streams.DownloadAsync(infoUr, diretorioSaida);
                }
                else
                {
                    throw new Exception("Nenhuma stream de vídeo disponível.");
                }

                return PhysicalFile(diretorioSaida, "video/mp4");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
