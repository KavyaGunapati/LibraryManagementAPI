
using LibraryManagementAPI.Interfaces.IServices;
using LibraryManagementAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IWebHostEnvironment _env;

        public BookController(IBookService bookService, IWebHostEnvironment env)
        {
            _bookService = bookService;
            _env = env;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Librarian,Member")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Librarian,Member")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null) return NotFound(new { message = "Book not found" });
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdate dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _bookService.UpdateBookAsync(id, dto);
                return Ok(new { message = "Book updated successfully" });
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return Ok(new { message = "Book deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Librarian")]
        public async Task<IActionResult> AddBook([FromBody] BookCreate dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _bookService.AddBookAsync(dto);
                return Ok(new { message = "Book added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("{bookId}/upload-cover")]
        [Authorize(Roles = "Librarian")]

        public async Task<IActionResult> UploadCover(int bookId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "File is required" });

            try
            {
                var fileName = await _bookService.UploadCoverAsync(bookId, file, _env.WebRootPath);
                return Ok(new { message = "Cover uploaded successfully", fileName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });

            }

        }

        [HttpGet("{bookId}/download-cover")]
        [Authorize(Roles = "Admin,Librarian,Member")]
        public async Task<IActionResult> DownloadCover(int bookId)
        {
            try
            {
                var stream = await _bookService.DownloadCoverAsync(bookId, _env.WebRootPath);
                return File(stream, "application/octet-stream", $"cover_{bookId}.jpg");
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
