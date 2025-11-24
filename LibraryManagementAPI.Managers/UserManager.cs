using AutoMapper;
using LibraryManagementAPI.DataAccess.Entities;
using LibraryManagementAPI.Interfaces.IRepository;
using LibraryManagementAPI.Interfaces.IServices;
using LibraryManagementAPI.Models.DTOs;
using LibraryManagerAPI.Interfaces.IManagers;

namespace LibraryManagementAPI.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<BorrowRecord> _borrowRecordRepo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public UserManager(IGenericRepository<User> userRepo, IMapper mapper,IGenericRepository<Book> bookRepo,IGenericRepository<BorrowRecord> borrowRecordRepo,ITokenService tokenService)
        {
            _userRepository = userRepo;
            _mapper = mapper;
            _bookRepository = bookRepo;
            _borrowRecordRepo = borrowRecordRepo;
            _tokenService = tokenService;
        }
        public async Task BorrowBookAsync(int userId, BorrowRequest dto)
        {
            var book=await _bookRepository.GetByIdAsync(dto.BookId);
            if (book==null) throw new KeyNotFoundException("Book not found.");
            var newRecord = new BorrowRecord
            {
                UserId = userId,
                BookId = book.Id,
                BorrowDate = DateTime.Now,
                IsReturned = false
            };
            await _borrowRecordRepo.AddAsync(newRecord);
            await _userRepository.SaveChangesAsync();

        }

        public Task<IList<BorrowReturn>> GetBorrowedBooksAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse?> LoginAsync(UserLogin dto)
        {
            var user=(await _userRepository.FindAsync(u=>u.Username==dto.Username)).FirstOrDefault();
            if(user == null) return null;
            var token=_tokenService.GenerateToken(user.Username,user.Role);
            return new AuthResponse { Token = token ,Username=user.Username,Role=user.Role};
        }

        public async Task<AuthResponse> RegisterAsync(UserRegister dto)
        {
            var exsistingUser=(await _userRepository.FindAsync(u=>u.Username==dto.Username)).FirstOrDefault();
            if(exsistingUser != null) throw new InvalidOperationException("Username already exists.");
            var user=_mapper.Map<User>(dto);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            var token=_tokenService.GenerateToken(user.Username,user.Role);
            return new AuthResponse { Token = token, Username = user.Username, Role = user.Role };
        }

        public Task ReturnBookAsync(int userId, int bookId)
        {
            throw new NotImplementedException();
        }
    }
}
