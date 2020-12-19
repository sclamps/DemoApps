using System.Collections.Generic;
using xPlatAuction.Models;

namespace xPlatAuction.API
{
    public interface IAuctionItemRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<AuctionItem> All { get; }
        AuctionItem Find(string id);
        void Insert(AuctionItem item);
        void Update(AuctionItem item);
        void Delete(string id);
    }
}