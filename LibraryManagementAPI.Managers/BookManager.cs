using AutoMapper;
using LibraryManagementAPI.DataAccess.Entities;
using LibraryManagementAPI.Interfaces.IManagers;
using LibraryManagementAPI.Interfaces.IRepository;
using LibraryManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;

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

        public async Task<string> GetCoverAsync(int bookId, string rootPath)
        {
            var book=await _repository.GetByIdAsync(bookId);

            if (book == null || string.IsNullOrEmpty(book.CoverImagePath))
                throw new FileNotFoundException("Cover not found.");
            var filePath=Path.Combine(rootPath,"uploads",book.CoverImagePath);
            if (!System.IO.File.Exists(filePath))
                throw new FileNotFoundException("File Not Found");
            return filePath;

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

        public async Task<string> UploadCoverAsync(int bookId, IFormFile file, string rootPath)
        {

            var book = await _repository.GetByIdAsync(bookId);
            if (book == null) throw new KeyNotFoundException("Book not found.");
            var uploadsFolder=Path.Combine(rootPath,"uploads");
            if(!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);
            var fileName=$"{Guid.NewGuid()}_{file.FileName}";
            var filePath=Path.Combine(uploadsFolder,fileName);
            using(var stream=new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            book.CoverImagePath = filePath;
            await _repository.UpdateAsync(book);
            await _repository.SaveChangesAsync();
            return fileName;

        }
    }
}
