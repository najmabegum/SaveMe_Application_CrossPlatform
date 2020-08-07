using Akavache;
using MApp_SaveMe.Constants;
using MApp_SaveMe.Contracts;
using MApp_SaveMe.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MApp_SaveMe.Services
{
    public class CategoryDataService : BaseService, ICategoriesDataService
    {
        private readonly IGenericRepository _genericRepository;
        public CategoryDataService(IGenericRepository genericRepository,
            IBlobCache cache = null) : base(cache)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<CategoryList>> GetAllCategories()
        {
            List<CategoryList> categoriesFromCache = await GetFromCache<List<CategoryList>>(CacheNameConstants.AllCategories);

            if (categoriesFromCache != null)
            {
                return categoriesFromCache;
            }
            else
            {
                UriBuilder uriBuilder = new UriBuilder(ApiConstants.BaseUri)
                {
                    Path = ApiConstants.GetAllCategories
                };
                IEnumerable<CategoryList> categoriesFromApi = await _genericRepository.GetAsync<List<CategoryList>>(uriBuilder.ToString());
                await Cache.InsertObject(CacheNameConstants.AllCategories, categoriesFromApi, DateTimeOffset.Now.AddSeconds(60));
                return categoriesFromApi;
            }
        }
    }
}
