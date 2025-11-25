using LibraryManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementAPI.Interfaces.IManagers
{
    public interface IBookManager
    {
        Task<IList<BookResponse>> GetAllBooksAsync();
        Task<BookResponse?> GetBookByIdAsync(int id);
        Task AddBookAsync(BookCreate book);
        Task UpdateBookAsync(int id, BookUpdate book);
        Task DeleteBookAsync(int id);
        Task<string> UploadCoverAsync(int bookId, IFormFile file, string rootPath);
        Task<string> GetCoverAsync(int bookId, string rootPath);
    }
}

