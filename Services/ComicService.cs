using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using xkcd.Models;

namespace xkcd.Services
{
    public class ComicService
    {
        private const string ComicsUrlSegment = "info.0.json";
        private readonly HttpClient _client;

        public ComicService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ComicViewModel> GetComic(int? comicId)
        {
            // TODO: refactor url generation
            string url = comicId != null ? string.Concat(comicId.ToString(), "/", ComicsUrlSegment) : ComicsUrlSegment;
            var comic = new ComicViewModel();
            var serializer = new DataContractJsonSerializer(typeof(ComicViewModel));

            var streamTask = await _client.GetStreamAsync(url);
            comic = serializer.ReadObject(streamTask) as ComicViewModel;
            
            return comic;
        }
    }
}