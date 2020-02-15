namespace Andreys.Services.Users
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        void CreateUser(string username, string email, string password);

        bool IsUsernameExist(string username);

        bool IsEmailExist(string email);
    }
}
