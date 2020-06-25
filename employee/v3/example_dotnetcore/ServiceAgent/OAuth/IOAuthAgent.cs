using System.Threading.Tasks;

namespace EmployeeExample.ServiceAgent.OAuth
{
    public interface IOAuthAgent
    {
        Task<string> ReadOrRetrieveAccessToken();
    }
}
