using System;
using System.Linq;
using xPlatAuction.Models;
using xPlatAuction.Services;

namespace xPlatAuction
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked (object sender, EventArgs e)
        {
            Message.Text = "Loading items...";

            try {
                var service = new AuctionItemService();
                var testItem = new AuctionItem
                {
                    Id = "1234-1324-132454342",
                    Text = "First item",
                };

                await service.SaveItem(testItem);

                var items = await service.ReadItems();
                var item = items.First ();
                Message.Text = item.Text ?? "Fail";
            } catch (Exception err) {
                Console.WriteLine (err);
                Console.WriteLine ("----------");
            }
        }
    }
}
