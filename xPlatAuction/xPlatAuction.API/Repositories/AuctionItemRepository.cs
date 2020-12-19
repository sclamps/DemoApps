using System.Collections.Generic;
using System.Linq;
using xPlatAuction.Models;

namespace xPlatAuction.API
{
    public class AuctionItemRepository : IAuctionItemRepository
    {
        List<AuctionItem> _auctionItemList;

        public AuctionItemRepository()
        {
            InitializeData();
        }

        public IEnumerable<AuctionItem> All => _auctionItemList;

        public bool DoesItemExist(string id)
        {
            return _auctionItemList.Any(item => item.Id == id);
        }

        public AuctionItem Find(string id)
        {
            return _auctionItemList.FirstOrDefault(item => item.Id == id);
        }

        public void Insert(AuctionItem item)
        {
            _auctionItemList.Add(item);
        }

        public void Update(AuctionItem item)
        {
            var auctionItem = this.Find(item.Id);
            var index = _auctionItemList.IndexOf(auctionItem);
            _auctionItemList.RemoveAt(index);
            _auctionItemList.Insert(index, item);
        }

        public void Delete(string id)
        {
            _auctionItemList.Remove(this.Find(id));
        }

        void InitializeData()
        {
            _auctionItemList = new List<AuctionItem>();

            var auctionItem1 = new AuctionItem
            {
                Id = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
                Text = "First item",
                Done = true
            };

            var auctionItem2 = new AuctionItem
            {
                Id = "b94afb54-a1cb-4313-8af3-b7511551b33b",
                Text = "Second item",
                Done = false
            };

            var auctionItem3 = new AuctionItem
            {
                Id = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
                Text = "Third item",
                Done = false,
            };

            _auctionItemList.Add(auctionItem1);
            _auctionItemList.Add(auctionItem2);
            _auctionItemList.Add(auctionItem3);
        }
    }
}