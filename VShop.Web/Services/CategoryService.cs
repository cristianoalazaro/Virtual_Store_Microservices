using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/categories/";
        private readonly JsonSerializerOptions _options;

        public CategoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory= clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive= true };
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var client = _clientFactory.CreateClient("ProductApi");

            IEnumerable<CategoryViewModel> categories;

            var response = await client.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                categories = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(
                                   apiResponse, _options);
            }
            else
            {
                return null;
            }
            return categories;
        }

        public Task<CategoryViewModel> FindCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryVM)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryViewModel> UpdateCategory(CategoryViewModel categoryVM)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
