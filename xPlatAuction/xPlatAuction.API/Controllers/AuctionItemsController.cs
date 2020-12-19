using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.MobileServices;
using xPlatAuction.Models;

namespace xPlatAuction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionItemsController: Controller
    {
        readonly IAuctionItemRepository _auctionItemRepository;
        readonly ILogger<AuctionItemsController> _logger;

        public AuctionItemsController (IAuctionItemRepository auctionItemRepository, ILogger<AuctionItemsController> logger)
        {
            _auctionItemRepository = auctionItemRepository;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult List()
        {
            return Ok(_auctionItemRepository.All);    
        }
    }
}