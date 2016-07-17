using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AuctionApi.Models;

namespace Examples.AuctionApi.Infrastructure
{
    public interface IUserStore
    {
        Task<IEnumerable<User>> FindAsync();
        Task<User> CreateAsync(User user);
    }
}
