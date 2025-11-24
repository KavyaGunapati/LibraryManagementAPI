
using LibraryManagementAPI.Models.DTOs;

namespace LibraryManagerAPI.Interfaces.IManagers
{
    public interface ICategoryManager
    {
        Task<IList<CategoryResponse>> GetAllCategoriesAsync();
        Task<CategoryResponse?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryCreate dto);
        Task UpdateCategoryAsync(int id, CategoryUpdate dto);
        Task DeleteCategoryAsync(int id);
    }
}
