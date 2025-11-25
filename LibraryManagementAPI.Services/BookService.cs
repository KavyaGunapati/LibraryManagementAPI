
using LibraryManagementAPI.Interfaces.IManagers;
using LibraryManagementAPI.Interfaces.IServices;
using LibraryManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
namespace LibraryManagementAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookManager _bookManager;
        public BookService(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public async Task<IList<BookResponse>> GetAllBooksAsync()
        {
            return await _bookManager.GetAllBooksAsync();
        }

        public async Task<BookResponse?> GetBookByIdAsync(int id)
        {
            return await _bookManager.GetBookByIdAsync(id);
        }

        public async Task AddBookAsync(BookCreate dto)
        {
            await _bookManager.AddBookAsync(dto);
        }

        public async Task UpdateBookAsync(int id, BookUpdate dto)
        {
            await _bookManager.UpdateBookAsync(id, dto);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookManager.DeleteBookAsync(id);
        }

        public async Task<string> UploadCoverAsync(int bookId, IFormFile file,string rootPath)
        {
            return await _bookManager.UploadCoverAsync(bookId, file, rootPath);
        }

        public async Task<FileStream> DownloadCoverAsync(int bookId,string rootPath)
        {
            var path=await _bookManager.GetCoverAsync(bookId,rootPath);
            return new FileStream(path, FileMode.Open);
        }
    }
}
