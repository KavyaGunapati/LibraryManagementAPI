
using LibraryManagementAPI.Interfaces.IServices;
using LibraryManagementAPI.Interfaces.IManagers;
using LibraryManagementAPI.Models.DTOs;
using LibraryManagementAPI.Managers;
using LibraryManagerAPI.Interfaces.IManagers;

namespace LibraryManagementAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryService(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public async Task<IList<CategoryResponse>> GetAllCategoriesAsync()
        {
            return await _categoryManager.GetAllCategoriesAsync();
        }

        public async Task<CategoryResponse?> GetCategoryByIdAsync(int id)
        {
            return await _categoryManager.GetCategoryByIdAsync(id);
        }

        public async Task AddCategoryAsync(CategoryCreate dto)
        {
            await _categoryManager.AddCategoryAsync(dto);
        }

        public async Task UpdateCategoryAsync(int id, CategoryUpdate dto)
        {
            await _categoryManager.UpdateCategoryAsync(id, dto);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryManager.DeleteCategoryAsync(id);
        }

        
    }
}
