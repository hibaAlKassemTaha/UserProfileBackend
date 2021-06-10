namespace UserProfile.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string ProfileImagePath { get; set; }
        public bool isActive { get; set; }
        public int Salary { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}