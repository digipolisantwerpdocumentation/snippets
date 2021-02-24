using System;
using System.Threading.Tasks;

namespace BraasExample
{
    class Program
    {
        private static BraasService _service;

        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting Braas example app");

                _service = new BraasService(Config.BaseAddress, Config.ApiKey);

                var application = await _service.GetApplication(Config.ApplicationId, Config.Jwt);
                var roles = await _service.GetApplicationRoles(Config.ApplicationId, Config.Jwt);
                var teams = await _service.GetRoleTeams(roles["roles"].First.Value<string>("id"), Config.Jwt);
                var subjects = await _service.GetTeamSubjects(teams["teams"].First.Value<string>("id"), Config.Jwt);
            }
            catch (Exception ex)
            {
                Console.Write($"Something went wrong: {ex.Message}");
            }
        }
    }
}
