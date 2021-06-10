namespace UserProfile.Models.Users
{
  public class UpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProfileImagePath { get; set; }
    }
}