using WpfDiDay.Data;
using WpfDiDay.Models;

namespace WpfDiDay.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository()
        {
            _context = new AppDbContext();
        }

        public void Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool ExistsByUserName(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public User? FindByUserName(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }
}
