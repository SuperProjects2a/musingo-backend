using musingo_backend.Models;

namespace musingo_backend.Authentication
{
    public interface IJwtAuth
    {
        string Authentication(User user);
    }
}
