using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Contracts
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(string uri, string authToken = "");
        Task<T> PostAsync<T>(string uri, T data, string authToken = "");
        Task<R> PostAsync<T, R>(string uri, T data, string authToken = "");
        Task<R> PutAsync<T, R>(string uri, T data, string authToken = "");
    }
}
