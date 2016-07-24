using System.Threading.Tasks;
using Examples.AuctionApi.Models;

namespace Examples.AuctionApi.Infrastructure
{
    public interface IUserStore
    {
        Task<bool> Validate(string email, string password);
        Task<User> FindAsync(string email);
        Task<User> CreateAsync(User user, string password);
    }
}
