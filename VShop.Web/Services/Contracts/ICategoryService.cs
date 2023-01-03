using VShop.Web.Models;

namespace VShop.Web.Services.Contracts;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllCategories();
    Task<CategoryViewModel> FindCategoryById(int id);
    Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryVM);
    Task<CategoryViewModel> UpdateCategory(CategoryViewModel categoryVM);
    Task<bool> DeleteCategory(int id);

}
