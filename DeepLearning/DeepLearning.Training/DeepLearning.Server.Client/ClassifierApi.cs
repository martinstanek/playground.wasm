using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace DeepLearning.Server.Client
{
    public class ClassifierApi
    {
        private readonly IClassifierApi _api;

        public ClassifierApi(string baseUrl) : this(new HttpClient { BaseAddress = new Uri(baseUrl) }) { }

        public ClassifierApi(HttpClient client)
        {
            _api = RestService.For<IClassifierApi>(client);
        }

        public async Task<string> ClasisifyAsync(Stream file, string fileName)
        {
            return await _api.ClasisifyAsync(new[] { new StreamPart(file, fileName) });
        }
    }
}
