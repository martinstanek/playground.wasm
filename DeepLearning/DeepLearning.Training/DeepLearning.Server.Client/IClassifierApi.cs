using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace DeepLearning.Server.Client
{
    public interface IClassifierApi
    {
        [Multipart]
        [Post("/classifier")]
        Task<string> ClasisifyAsync([AliasAs("files")] IEnumerable<StreamPart> streams);
    }
}
