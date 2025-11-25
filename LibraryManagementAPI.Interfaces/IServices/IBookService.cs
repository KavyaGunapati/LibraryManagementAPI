
using LibraryManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;

namespace LibraryManagementAPI.Interfaces.IServices
{
    public interface IBookService
    {
        Task<IList<BookResponse>> GetAllBooksAsync();
        Task<BookResponse?> GetBookByIdAsync(int id);
        Task AddBookAsync(BookCreate dto);
        Task UpdateBookAsync(int id, BookUpdate dto);
        Task DeleteBookAsync(int id);
        Task<string> UploadCoverAsync(int bookId, IFormFile file,string rootPath);
        Task<FileStream> DownloadCoverAsync(int bookId,string rootPath);
    }
}
