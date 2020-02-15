namespace IRunes.Services.Users
{
    public interface IUsersService
    {
        string GetUsername(string userId);

        string GetUserId(string username, string password);

        void RegisterUser(string username, string password, string email);

        bool IsUsernameExist(string username);

        bool IsEmailExist(string email);
    }
}
