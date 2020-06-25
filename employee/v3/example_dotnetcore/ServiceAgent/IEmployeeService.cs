using EmployeeExample.models;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace EmployeeExample.ServiceAgent
{
    interface IEmployeeService
    {
        Task<Employee> GetEmployee([NotNull] string crsNumber);

        Task<EmployeesHALResponse> SearchEmployees(string crsNumber, string NameContains, bool? isSuperVisor, int page, int pageSize);
    }
}
