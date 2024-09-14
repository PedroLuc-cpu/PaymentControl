using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PaymentControl.data;
using PaymentControl.model;
using PaymentControl.Repositories.Interface;

namespace PaymentControl.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PaymentControlContext _context;
        public UserRepository(PaymentControlContext context)
        {
            _context = context;
        }
        public async Task<User> AddUserAsync(User user)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var users = new User
            {
                Username = user.Username,
                Password = hashedPassword,
                Email = user.Email,
                Role = user.Role,
            };
            _context.users.Add(users);
            await _context.SaveChangesAsync();
            return users;
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.Username == username || u.Email == username);
            if (user == null)
            {
                return null;
            }
            if (!VerifiryPassword(password, user.Password))
            {
                return null;
            }
            return user;
        }

        private bool VerifiryPassword(string inputPassword, string storePassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storePassword);
        }
    }
}