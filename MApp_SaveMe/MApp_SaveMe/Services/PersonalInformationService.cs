using Akavache;
using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Services
{
    public class PersonalInformationService : BaseService, IPersonalInformationService
    {
        private readonly IGenericRepository _genericRepository;
        public PersonalInformationService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<bool> UpdateUserData(int userDataID, UserData userData)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.UpdateUserData + userDataID
            };
            bool isSuccess = await _genericRepository.PutAsync<UserData, bool>(uriBuilder.ToString(), userData);
            return isSuccess;
        }

        public async Task<User> FetchUser(int userDataID)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.GetUserData + userDataID.ToString()
            };
            User user = await _genericRepository.GetAsync<User>(uriBuilder.ToString());
            return user;
        }
    }
}
