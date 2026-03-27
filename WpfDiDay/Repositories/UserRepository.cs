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
            if (user == null)
                throw new ArgumentNullException(nameof(user)); 
            this._context.Users.Add(user);
            this._context.SaveChanges();
        }
        public void Delete(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            User? existing_user = null;
            if (user.UserId != 0)
                existing_user = this._context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            else if (!string.IsNullOrEmpty(user.Email))
                existing_user = this._context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existing_user != null)
                this._context.Users.Remove(user);
            else
                Console.WriteLine("This user is not found.");
            this._context.SaveChanges();
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
