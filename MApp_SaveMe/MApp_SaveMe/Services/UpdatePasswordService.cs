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
    public class UpdatePasswordService : BaseService, IUpdatePasswordService
    {
        private readonly IGenericRepository _genericRepository;
        public UpdatePasswordService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<bool> ChangePassword(LoginModel loginModel)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
            {
                Path = ApiConstants.ChangePassword
            };
            bool isSuccess = await _genericRepository.PostAsync<LoginModel, bool>(uriBuilder.ToString(), loginModel);
            return isSuccess;
        }
    }
}
