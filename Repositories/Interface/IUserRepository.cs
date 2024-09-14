
using PaymentControl.model;

namespace PaymentControl.Repositories.Interface
{
    public interface IUserRepository
    {
        public Task<User> GetUserAsync(string username, string password);
        public Task<User> AddUserAsync(User user);

    }
}