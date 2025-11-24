namespace LibraryManagementAPI.Models.DTOs
{

    public class BorrowReturn
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }

}
