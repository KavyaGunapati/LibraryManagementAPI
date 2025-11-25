
using LibraryManagementAPI.Interfaces.IManagers;
using LibraryManagementAPI.Interfaces.IServices;
using LibraryManagementAPI.Models.DTOs;

namespace LibraryManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;

        public UserService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> RegisterAsync(UserRegister dto)
        {
            return await _userManager.RegisterAsync(dto);
        }

        public async Task<AuthResponse?> LoginAsync(UserLogin dto)
        {
            return await _userManager.LoginAsync(dto);
        }

        public async Task BorrowBookAsync(int userId, BorrowRequest dto)
        {
            await _userManager.BorrowBookAsync(userId, dto);
        }

        public async Task ReturnBookAsync(int userId, int bookId)
        {
            await _userManager.ReturnBookAsync(userId, bookId);
        }

        public async Task<IList<BorrowReturn>> GetBorrowedBooksAsync(int userId)
        {
            return await _userManager.GetBorrowedBooksAsync(userId);
        }
    }
}
