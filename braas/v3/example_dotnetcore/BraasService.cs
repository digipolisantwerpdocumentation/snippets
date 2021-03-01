using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BraasExample
{
    public class BraasService
    {
        private readonly HttpClient _httpClient;

        public BraasService(string baseAddress, string apiKey)
        {
            // Use IHttpClientFactory (AddHttpClient) in real implementations
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseAddress);
            _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        }

        private Task<HttpResponseMessage> GetAsync(string uri, string jwt)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                if (jwt != null)
                {
                    requestMessage.Headers.Add("Dgp-Authorization-For", $"Bearer {jwt}");
                }

                return _httpClient.SendAsync(requestMessage);
            }
        }

        public async Task<JObject> GetApplication(string applicationId, string jwt)
        {
            var responseMessage = await GetAsync($"applications/{applicationId}", jwt);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetApplication failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetApplication response ({(int)responseMessage.StatusCode}): {responseContent}");
            // GetApplication response (200): {"application":{"attributes":{"CRUD":"","DOMEIN":null,"LOCATIE":null,"STADSBEDRIJF":"zwijndrecht"},"description":null,"displayName":"testapprc01704-someTenantKey","id":"97df3524-3675-4240-9ffc-36f2a1bd99b6","name":"testapprc01704-someTenantKey","targetApplicationUniqueKey":"testapprc01704","tenantKey":"someTenantKey"}}

            return JObject.Parse(responseContent);
        }

        public async Task<JObject> GetApplicationRoles(string applicationId, string jwt)
        {
            var responseMessage = await GetAsync($"applications/{applicationId}/roles", jwt);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetApplicationRoles failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetApplicationRoles response ({(int)responseMessage.StatusCode}): {responseContent}");
            // GetApplicationRoles response (200): {"cursor":126,"roles":[{"attributes":{"BEHEERDER":"","CRUD":"","DOMEIN":null,"LOCATIE":null,"STADSBEDRIJF":null},"description":null,"id":"08364e92-6116-4b1d-8da9-a565d89ad148","name":"Test role rc01704","validFrom":null,"validTo":null}]}

            return JObject.Parse(responseContent);
        }

        public async Task<JObject> GetRoleTeams(string roleId, string jwt)
        {
            var responseMessage = await GetAsync($"roles/{roleId}/teams", jwt);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetRoleTeams failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetRoleTeams response ({(int)responseMessage.StatusCode}): {responseContent}");
            // GetRoleTeams response (200): {"cursor":1557930635498103,"teams":[{"attributes":{"CRUD":null,"DOMEIN":null,"LOCATIE":"test1","STADSBEDRIJF":null},"description":null,"id":"7a1bc4e8-1417-42e6-90ec-e646b9188d00","joinApprovalRequired":false,"name":"Test team rc01704","open":true,"validFrom":null,"validTo":null,"visible":true}]}

            return JObject.Parse(responseContent);
        }

        public async Task<JObject> GetTeamSubjects(string teamId, string jwt)
        {
            var responseMessage = await GetAsync($"teams/{teamId}/subjects", jwt);
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"GetTeamSubjects failed ({(int)responseMessage.StatusCode}): {responseContent}");
            }

            Console.WriteLine($"GetTeamSubjects response ({(int)responseMessage.StatusCode}): {responseContent}");
            // GetTeamSubjects response (200): {"cursor":110668,"members":[{"address":null,"domain":"ICA","email":"Some.User@digipolis.be","externalMutableReference":"rc01704@digant.antwerpen.local","firstname":"Some","id":"3aebe845-0a71-443a-b868-5c4f0e1b84cc","lastname":"User","nickname":null,"owner":true,"type":"mprofiel","username":"rc01704"},{"address":null,"domain":"ICA","email":"Other.User@digipolis.be","externalMutableReference":"rc00992@digant.antwerpen.local","firstname":"Other","id":"29730b11-7056-48c3-80ef-b6de3904455c","lastname":"User","nickname":null,"owner":false,"type":"mprofiel","username":"rc00992"}]}

            return JObject.Parse(responseContent);
        }
    }
}
