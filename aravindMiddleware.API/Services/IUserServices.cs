namespace aravindMiddleware.API.Services
{
    public interface IUserServices
    {
        string Authenticate(string Secret);
    }
}
