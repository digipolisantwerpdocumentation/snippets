using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeExample.ServiceAgent.Handlers
{
    public class ContentTypeJsonHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null && !(request.Content is MultipartFormDataContent))
                request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaType.Json);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
