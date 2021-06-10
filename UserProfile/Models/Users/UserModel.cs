namespace UserProfile.Models.Users
{
  public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string ProfileImagePath { get; set; }
        public bool isActive { get; set; }
        public bool Salary { get; set; }
    }
}