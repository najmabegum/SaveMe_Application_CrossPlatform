using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Contracts
{
    public interface IUpdatePasswordService
    {
        Task<bool> ChangePassword(LoginModel loginModel);
    }
}
