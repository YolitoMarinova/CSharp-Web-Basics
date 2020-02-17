namespace SharedTrip.Services.Users
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        void RegisterUser(string username, string email, string password);

        bool IsUsernameExist(string username);

        bool IsEmailExist(string email);
    }
}
