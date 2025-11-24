using AutoMapper;
using LibraryManagementAPI.DataAccess.Entities;
using LibraryManagementAPI.Interfaces.IManagers;
using LibraryManagementAPI.Interfaces.IRepository;
using LibraryManagementAPI.Models.DTOs;

namespace LibraryManagementAPI.Managers
{
    public class BookManager:IBookManager
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly IMapper _mapper;
        public BookManager(IGenericRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddBookAsync(BookCreate book)
        {
            var bookEntity = _mapper.Map<Book>(book);
            await _repository.AddAsync(bookEntity);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IList<BookResponse>> GetAllBooksAsync()
        {
           var books=await _repository.GetAllAsync();
            return _mapper.Map<IList<BookResponse>>(books);
        }

        public async Task<BookResponse?> GetBookByIdAsync(int id)
        {
            var book=await _repository.GetByIdAsync(id);
            if (book == null) throw new KeyNotFoundException($"Book with ID {id} not found.");
            return _mapper.Map<BookResponse?>(book);

        }

        public async Task UpdateBookAsync(int id, BookUpdate book)
        {
            var bookExsist = await _repository.GetByIdAsync(id);
            if (bookExsist == null) throw new KeyNotFoundException($"Book with ID {id} not found.");
            _mapper.Map(book, bookExsist);
            await _repository.UpdateAsync(bookExsist);
            await _repository.SaveChangesAsync();

        }
    }
}
