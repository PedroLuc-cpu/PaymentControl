using cadastro.Model;

namespace PaymentControl.model
{
    public class User : Base
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}