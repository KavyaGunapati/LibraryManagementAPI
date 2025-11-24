
using LibraryManagementAPI.Models.DTOs;

namespace LibraryManagementAPI.Interfaces.IServices
{
    public interface IUserService
    {
        Task<AuthResponse> RegisterAsync(UserRegister dto);
        Task<AuthResponse?> LoginAsync(UserLogin dto);
        Task BorrowBookAsync(int userId, BorrowRequest dto);
        Task ReturnBookAsync(int userId, int bookId);
        Task<IList<BorrowReturn>> GetBorrowedBooksAsync(int userId);
    }
}
