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
        // ClasseYouTUbeClient que irá fornecer métodos para interagir com a API do YouTube.
        private readonly YoutubeClient _youtube;

        // COnstrutor
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
                var infoFLuxo = informacaoUrl.GetMuxedStreams().GetWithHighestVideoQuality();

                if (infoFLuxo != null)
                {
                    await _youtube.Videos.Streams.DownloadAsync(infoFLuxo, diretorioSaida);
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
        [HttpGet("BaixarAudio")]
        public async Task<IActionResult> BaixarAudio(string videoUrl)
        {
            try
            {
                var videoInformacao = await _youtube.Videos.GetAsync(videoUrl);
                var videoTitulo = videoInformacao.Title;

                var diretorioDeSaida = @"/home/gabriel/Desktop/videos"; 
                var diretorioSaida = Path.Combine(diretorioDeSaida, $"{videoTitulo}.mp3");

                Directory.CreateDirectory(diretorioDeSaida);

                var informacaoUrl = await _youtube.Videos.Streams.GetManifestAsync(videoUrl);
                var infoFluxoAudio = informacaoUrl.GetAudioOnlyStreams().GetWithHighestBitrate();

                if (infoFluxoAudio != null)
                {
                    await _youtube.Videos.Streams.DownloadAsync(infoFluxoAudio, diretorioSaida);
                }
                else
                {
                    throw new Exception("Nenhuma stream de áudio disponível.");
                }

                return PhysicalFile(diretorioSaida, "audio/mpeg");

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
