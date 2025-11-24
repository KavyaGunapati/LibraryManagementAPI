
using LibraryManagementAPI.Models.DTOs;

namespace LibraryManagementAPI.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<IList<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryCreate dto);
        Task UpdateCategoryAsync(int id, CategoryUpdate dto);
        Task DeleteCategoryAsync(int id);
    }
}
