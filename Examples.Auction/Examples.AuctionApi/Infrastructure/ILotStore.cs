using System.Collections.Generic;
using System.Threading.Tasks;
using Examples.AuctionApi.Models;

namespace Examples.AuctionApi.Infrastructure
{
    public interface ILotStore
    {
        Task<IEnumerable<Lot>> FindAsync();
        Task<Lot> FindAsync(int lotId);
        Task<IEnumerable<Lot>> FindAsyncQuery(string searchText);
        Task UpdateAsync(Lot lot);
        Task DeleteAsync(int lotId);
        Task CreateAsync(Lot lot);
        Task AddBid(int lotId, Bid bid);
    }
}
