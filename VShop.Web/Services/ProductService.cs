using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string apiEndpoint = "/api/products/";
        private readonly JsonSerializerOptions _options;
        private ProductViewModel productVM;
        private IEnumerable<ProductViewModel> productsVM;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            var client = _clientFactory.CreateClient("ProductApi");

            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    productsVM = await JsonSerializer
                                .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return productsVM;
        }

        public Task<ProductViewModel> FindProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
        {
            throw new NotImplementedException();
        }

        public Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }
    }
}
