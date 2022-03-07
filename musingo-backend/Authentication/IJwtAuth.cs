namespace musingo_backend.Authentication
{
    public interface IJwtAuth
    {
        string Authentication(string username);
    }
}
