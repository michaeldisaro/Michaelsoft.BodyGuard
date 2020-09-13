namespace Michaelsoft.BodyGuard.Client.Services
{
    public interface IConnectionService
    {

        string RegisterUser(string email,
                            string password,
                            dynamic userData);

    }
}