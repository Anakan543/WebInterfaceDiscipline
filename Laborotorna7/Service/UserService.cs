using Laborotorna7.Models;

namespace Laborotorna7.Service
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(string nickName, User user);
        Task<bool> DeleteUser(string nickName);
    }
    public class UserService : IUserService
    {
        private readonly List<User> _users;

        public UserService()
        {
            _users = new List<User>
            {
                new User { NickName = "john_doe", Email = "john@example.com", Password = "pass123" },
                new User { NickName = "jane_smith", Email = "jane@example.com", Password = "jane456" },
                new User { NickName = "bob_jones", Email = "bob@example.com", Password = "bob789" },
                new User { NickName = "alice_w", Email = "alice@example.com", Password = "alice101" },
                new User { NickName = "mike_k", Email = "mike@example.com", Password = "mike202" },
                new User { NickName = "sarah_p", Email = "sarah@example.com", Password = "sarah303" },
                new User { NickName = "tom_r", Email = "tom@example.com", Password = "tom404" },
                new User { NickName = "emma_l", Email = "emma@example.com", Password = "emma505" },
                new User { NickName = "david_m", Email = "david@example.com", Password = "david606" },
                new User { NickName = "lisa_n", Email = "lisa@example.com", Password = "lisa707" }
            };
        }

        public async Task<List<User>> GetUsers()
        {
            return await Task.FromResult(_users);
        }

        public async Task<User> CreateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (_users.Any(u => u.NickName == user.NickName.Trim()))   throw new InvalidOperationException("User already exists");

            _users.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<User> UpdateUser(string nickName, User user)
        {
            if (string.IsNullOrEmpty(nickName))
                throw new ArgumentNullException(nameof(nickName));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userIndex = _users.FindIndex(u => u.NickName == nickName.Trim());
            if (userIndex == -1)
                return null;

            _users[userIndex] = user;
            return await Task.FromResult(user);
        }

        public async Task<bool> DeleteUser(string nickName)
        {
            if (string.IsNullOrEmpty(nickName))
                throw new ArgumentNullException(nameof(nickName));

            var user = _users.FirstOrDefault(u => u.NickName == nickName.Trim());
            if (user == null)
                return false;

            _users.Remove(user);
            return await Task.FromResult(true);
        }
    }
}
