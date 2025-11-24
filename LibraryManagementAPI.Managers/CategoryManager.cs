using AutoMapper;
using LibraryManagementAPI.DataAccess.Entities;
using LibraryManagementAPI.Interfaces.IRepository;
using LibraryManagementAPI.Models.DTOs;
using LibraryManagerAPI.Interfaces.IManagers;

namespace LibraryManagementAPI.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        public CategoryManager(IGenericRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddCategoryAsync(CategoryCreate dto)
        {
            var category=_mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IList<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories=await _repository.GetAllAsync();
            return _mapper.Map<IList<CategoryResponse>>(categories);
        }

        public async Task<CategoryResponse?> GetCategoryByIdAsync(int id)
        {
           var category=await _repository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"Category with this {id} not found");
            return _mapper.Map<CategoryResponse?>(category);
        }

        public async Task UpdateCategoryAsync(int id, CategoryUpdate dto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException($"Category with this {id} not found");
          await  _repository.UpdateAsync(category);
            await _repository.SaveChangesAsync();
        }
    }
}
