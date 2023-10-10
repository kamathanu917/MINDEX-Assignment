using CodeChallenge.Models;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        Compensation AddCompensation(Compensation compensation);
        Compensation GetCompensationByEmployeeId(string employeeId);
        Task SaveAsync();
    }
}
