using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Register(string firstName, string middleName, string lastName, string gender, int age,
                                              string maritalStatus, int adultsInFamily, int kidsInFamily, string username, string password);

        Task<AuthenticationResponse> Authenticate(string userName, string password);

        bool IsUserAuthenticated();
    }
}
