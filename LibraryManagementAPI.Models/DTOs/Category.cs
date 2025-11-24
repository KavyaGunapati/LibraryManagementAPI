using System.ComponentModel.DataAnnotations;
namespace LibraryManagementAPI.Models.DTOs
{
    public class CategoryCreate
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string Name { get; set; }
    }


    public class CategoryUpdate
    {
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
        public string? Name { get; set; }
    }

    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

}
