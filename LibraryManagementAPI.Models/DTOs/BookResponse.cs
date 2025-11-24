namespace LibraryManagementAPI.Models.DTOs
{

    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CoverImagePath { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

}
