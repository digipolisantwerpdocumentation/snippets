using EmployeeExample.models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeExample.ServiceAgent
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService(IServiceProvider serviceProvider, HttpClient httpClient)
        {
            _serviceProvider = serviceProvider;
            _httpClient = httpClient;
        }

        private readonly IServiceProvider _serviceProvider;
        private readonly HttpClient _httpClient;

        protected JsonSerializerOptions serializerSettings = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        public async Task<Employee> GetEmployee([NotNull] string crsNumber)
        {
            var path = $"employees/{crsNumber.ToString(CultureInfo.InvariantCulture)}";

            using (HttpResponseMessage httpResponse = await _httpClient.GetAsync(path))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    using (var stream = await httpResponse.Content.ReadAsStreamAsync())
                    {
                        var employee = await System.Text.Json.JsonSerializer.DeserializeAsync<Employee>(stream, serializerSettings);

                        Console.WriteLine($"Get employee response ({(int)httpResponse.StatusCode}): {System.Text.Json.JsonSerializer.Serialize(employee)} ");

                        return employee;
                    }
                }

                throw new HttpRequestException($"EmployeeService could not find employee with crsNumber {crsNumber.ToString(CultureInfo.InvariantCulture)} or the service is unavailable.");
            }
        }

        public async Task<EmployeesHALResponse> SearchEmployees(string crsNumber, string NameContains, 
                                                                bool? isSuperVisor, int page, int pageSize)
        {
            // this query uses only a subset of the possible query parameters;
            // see swagger-documentation for all possible options
            var path = $"employees?Page={page}&PageSize={pageSize}&PagingStrategy=withcount&Includes=organisation";

            if (!string.IsNullOrWhiteSpace(crsNumber)) path += $"&CrsNumbers={crsNumber}";
            if (!string.IsNullOrWhiteSpace(NameContains)) path += $"&NameContains={NameContains}";
            if (isSuperVisor.HasValue) path += $"&IsSupervisor={isSuperVisor.ToString()}";

            using (HttpResponseMessage httpResponse = await _httpClient.GetAsync(path))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    using (var stream = await httpResponse.Content.ReadAsStreamAsync())
                    {
                        var employee = await System.Text.Json.JsonSerializer.DeserializeAsync<EmployeesHALResponse>(stream, serializerSettings);

                        Console.WriteLine($"Get employees response ({(int)httpResponse.StatusCode}): {System.Text.Json.JsonSerializer.Serialize(employee)} ");

                        return employee;
                    }
                }

                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Employees search request on the EmployeeService wasn't succesful: {responseContent}.");
            }
        }
    }
}
