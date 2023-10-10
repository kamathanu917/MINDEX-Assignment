using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation CreateCompensation(Compensation compensation);
        Compensation ReadCompensationByEmployeeId(string employeeId);
    }
}
