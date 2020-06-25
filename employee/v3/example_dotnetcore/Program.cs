using EmployeeExample.ServiceAgent;
using System;
using System.Threading.Tasks;

namespace EmployeeExample
{
    class Program
    {
        private static IEmployeeService _service;

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting Employee example app");

                if (string.IsNullOrWhiteSpace(Config.ApiKey) 
                    || string.IsNullOrWhiteSpace(Config.OAuthClientId)
                    || string.IsNullOrWhiteSpace(Config.OAuthClientSecret))
                {
                    throw new Exception("Define necessary configuration variables");
                }

                // setup DI
                IServiceProvider serviceProvider = ServiceHelper.InitializeServices();                
                _service = (IEmployeeService)serviceProvider.GetService(typeof(IEmployeeService));

                // GET employee
                var employee = await _service.GetEmployee("100");

                // search employees
                var employees = await _service.SearchEmployees(string.Empty, "Dummy", false, 1, 10);
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong: {ex.Message}");
            }
        }
    }
}
