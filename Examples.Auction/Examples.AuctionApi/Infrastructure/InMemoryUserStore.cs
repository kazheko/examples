using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AuctionApi.Models;

namespace Examples.AuctionApi.Infrastructure
{
    public class InMemoryUserStore : IUserStore
    {
        private readonly IDictionary<string, User> _users;
        private readonly IDictionary<string, string> _usersEmails;

        public InMemoryUserStore()
        {
            _users = new ConcurrentDictionary<string, User>();
            _usersEmails = new ConcurrentDictionary<string, string>();
        }

        public Task<User> CreateAsync(User user, string password)
        {
            _users.Add(user.Email,user);
            _usersEmails.Add(user.Email, password);
            return Task.FromResult(user);
        }

        public Task<bool> Validate(string email, string password)
        {
            string pass;
            return Task.FromResult(_usersEmails.TryGetValue(email, out pass) && pass.Equals(password));
        }

        public Task<User> FindAsync(string email)
        {
            User user;
            return Task.FromResult(_users.TryGetValue(email, out user) ? user : null);
        }
    }
}
