namespace BookStoreApi.Authentication
{
    public interface IAuthManager
    {
        string Authenticate(string username, string password);
    }
}
