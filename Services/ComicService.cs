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
            var comic = new ComicViewModel();
            var serializer = new DataContractJsonSerializer(typeof(ComicViewModel));

            var response = await _client.GetAsync(GetComicUrl(comicId));

            if (!response.IsSuccessStatusCode) {
                return null;
            }
            comic = serializer.ReadObject(await response.Content.ReadAsStreamAsync()) as ComicViewModel; 

            return comic;
        }

        private string GetComicUrl(int? comicId)
        {
            return comicId != null ? string.Concat(comicId.ToString(), "/", ComicsUrlSegment) : ComicsUrlSegment;
        }

        public int GetNextComicNumber(int previousNumber, bool isPrevious)
        {
            return isPrevious ? previousNumber - 1 : previousNumber + 1; 
        }
    }
}