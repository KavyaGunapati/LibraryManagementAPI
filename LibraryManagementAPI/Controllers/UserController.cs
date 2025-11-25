
using LibraryManagementAPI.Interfaces.IServices;
using LibraryManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Borrow a book
        [HttpPost("{userId}/borrow")]
        [Authorize(Roles = "Member,Librarian,Admin")]
        public async Task<IActionResult> BorrowBook(int userId, [FromBody] BorrowRequest dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.BorrowBookAsync(userId, dto);
                return Ok(new { message = "Book borrowed successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Return a book
        [HttpPost("{userId}/return/{bookId}")]
        [Authorize(Roles = "Member,Librarian,Admin")]
        public async Task<IActionResult> ReturnBook(int userId, int bookId)
        {
            try
            {
                await _userService.ReturnBookAsync(userId, bookId);
                return Ok(new { message = "Book returned successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // View borrowed books
        [HttpGet("{userId}/borrowed-books")]
        [Authorize(Roles = "Member,Librarian,Admin")]
        public async Task<IActionResult> GetBorrowedBooks(int userId)
        {
            try
            {
                var borrowedBooks = await _userService.GetBorrowedBooksAsync(userId);
                return Ok(borrowedBooks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
