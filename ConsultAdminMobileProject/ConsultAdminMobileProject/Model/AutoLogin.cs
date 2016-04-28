using SQLite;

namespace ConsultAdminMobileProject.Model
{
    public class SavedUser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class RememberMe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool RememberLogin { get; set; }
    }
}
