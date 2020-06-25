using EmployeeExample.ServiceAgent.OAuth;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeExample.ServiceAgent.Handlers
{
    public class OAuthTokenHandler : DelegatingHandler
    {
        private readonly IOAuthAgent _oAuthAgent;

        public OAuthTokenHandler(IOAuthAgent oAuthAgent)
        {
            _oAuthAgent = oAuthAgent ?? throw new ArgumentNullException(nameof(oAuthAgent));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _oAuthAgent.ReadOrRetrieveAccessToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
