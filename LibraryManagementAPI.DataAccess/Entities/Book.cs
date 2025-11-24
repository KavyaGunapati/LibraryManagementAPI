namespace LibraryManagementAPI.DataAccess.Entities
{

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? CoverImagePath { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }

}
