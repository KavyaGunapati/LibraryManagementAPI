using LibraryManagementAPI.Models.DTOs;

namespace LibraryManagementAPI.Interfaces.IManagers
{
    public interface IBookManager
    {
        Task<IList<BookResponse>> GetAllBooksAsync();
        Task<BookResponse?> GetBookByIdAsync(int id);
        Task AddBookAsync(BookCreate book);
        Task UpdateBookAsync(int id, BookUpdate book);
        Task DeleteBookAsync(int id);
    }
}

