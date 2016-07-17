using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examples.AuctionApi.Models;

namespace Examples.AuctionApi.Infrastructure
{

    internal class InMemoryUserStore : IUserStore
    {
        private readonly IList<User> _users;

        public InMemoryUserStore()
        {
            _users = new List<User>();
        }

        public Task<IEnumerable<User>> FindAsync()
        {
            return Task.FromResult(_users.AsEnumerable());
        }

        public Task<User> CreateAsync(User user)
        {
            _users.Add(user);
            return Task.FromResult(user);
        }
    }
}
