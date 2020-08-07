using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        public AuthenticationService(IGenericRepository genericRepository, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _genericRepository = genericRepository;
        }

        public async Task<AuthenticationResponse> Register(string firstName, string middleName, string lastName, string gender, int age,
                                                           string maritalStatus, int adultsInFamily, int kidsInFamily, string username, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.UserRegistration
            };

            UserRegistration authenticationRequest = new UserRegistration()
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Gender = gender,
                Age = age,
                MaritalStatus = maritalStatus,
                NumberMembersInFamily = adultsInFamily,
                NumberKidsInFamily = kidsInFamily,
                UserName = username,
                Password = password
            };

            return await _genericRepository.PostAsync<UserRegistration, AuthenticationResponse>(builder.ToString(), authenticationRequest);
        }

        public bool IsUserAuthenticated()
        {
            return !string.IsNullOrEmpty(_settingsService.UserIdSetting);
        }

        public async Task<AuthenticationResponse> Authenticate(string userName, string password)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.Authenticate
            };

            LoginModel loginRequest = new LoginModel()
            {
                UserName = userName,
                Password = password
            };

            return await _genericRepository.PostAsync<LoginModel, AuthenticationResponse>(builder.ToString(), loginRequest);
        }
    }
}
