namespace LibraryManagementAPI.DataAccess.Entities
{

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = string.Empty;

        public ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }

}
