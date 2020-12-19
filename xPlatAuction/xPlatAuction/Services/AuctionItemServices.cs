/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using xPlatAuction.Models;

namespace xPlatAuction.Services
{
    public class AuctionItemService
    {
        MobileServiceClient client = new MobileServiceClient(Constants.ApplicationUrl, new HttpClientHandler());

        readonly IMobileServiceTable<AuctionItem> _auctionTable;
        public AuctionItemService()
        {
            _auctionTable = client.GetTable<AuctionItem> ();
        }

        public async Task<bool> SaveItem (AuctionItem item)
        {
            try {
                await _auctionTable.InsertAsync (item);
                return true;
            } catch (MobileServiceInvalidOperationException exception) {
                var response = await exception.Response.Content.ReadAsStringAsync ();
                return false;
            } catch (Exception exc) {
                return false;
            }
        }

        public async Task<List<AuctionItem>> ReadItems ()
        {
            return await client.GetTable<AuctionItem> ().ToListAsync ();
        }

    }
}
