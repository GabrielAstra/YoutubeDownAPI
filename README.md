# YoutubeDownAPI
Studying the YouTubeExplode package, testing a WebAPI to introduce in my MVC project in the future.


Esse projeto utilizada a biblioteca YoutubeExplode para baxar vídeos .mp4 e áudios .mp3 de vídeos do Youtube, ainda não tem mais opções de streams (fluxos), mas futuramente irei adicionar. 

O primeiro método BaixarVídeo pega informações e títulos do vídeo que será passado na "videoUrl", assim que todos dados forem baixados ele irá baixar no diretório de saída que eu defini, uma pasta no meu próprio 
computador. Pulando um pouco, se não houver informação nula sobre o vídeo (isso precisaria de uma explicação mais de baixo nível, que está na documentação da biblioeteca), ele fará o download, caso contrário
irá dar uma Exception.

Em breve mais explicações e vídeos em português sobre o uso.

WARNING! THIS PROJECT WORKS ONLY IN LINUX, MY PROJECT THAT START IN WINDOWS IT IS IN OTHER REPOSITORY.
