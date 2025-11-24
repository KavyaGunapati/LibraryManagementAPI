using System.ComponentModel.DataAnnotations;

namespace LibraryManagementAPI.Models.DTOs
{

    public class BookCreate
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }=string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 0.")]
        public int CategoryId { get; set; }
    }


}
