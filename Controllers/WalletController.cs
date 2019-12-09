using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class WalletController : Controller
    {
        private readonly ApiContext _ctx;
        
        public WalletController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Wallet.OrderBy(c => c.Rank);

            return Ok(data);
        }

        // GET api/wallet/5
        [HttpGet("{id}", Name = "GetWallet")]
        public IActionResult Get(int id)
        {
            var crypto = _ctx.Wallet.Find(id);
            return Ok(crypto);
        }

        // api/wallet/
        [HttpPost]
        public IActionResult Post([FromBody] Wallet wallet)
        {
            if (wallet == null)
            {
                return BadRequest();
            }

            _ctx.Wallet.Add(wallet);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetWallet", new { id = wallet.idCrypto }, wallet);
        }
    }
}



// using Advantage.API.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using System;

// namespace Advantage.API.Controllers
// {
//     [Route("api/[controller]")]
//     public class OrderController : Controller
//     {
//         private readonly ApiContext _ctx;
        
//         public OrderController(ApiContext ctx)
//         {
//             _ctx = ctx;
//         }

//         // GET api/order/
//         [HttpGet]
//         public IActionResult Get()
//         {
//             var data = _ctx.Orders.Include(o => o.Customer)
//                 .OrderByDescending(c => c.Placed);

//             return Ok(data);
//         }

//         [HttpGet("ByState")]
//         public IActionResult ByState()
//         {
//             var orders = _ctx.Orders.Include(o => o.Customer).ToList();

//             var groupedResult = orders.GroupBy(o => o.Customer.State)
//                 .ToList()
//                 .Select(grp => new{
//                     State = grp.Key,
//                     Total = grp.Sum(x => x.Total)
//                 }).OrderByDescending(res => res.Total)
//                 .ToList();

//             return Ok(groupedResult);    
//         }

//         [HttpGet("ByCustomer/{n}")]
//         public IActionResult ByCustomer(int n)
//         {
//             var orders = _ctx.Orders.Include(o => o.Customer).ToList();

//             var groupedResult = orders.GroupBy(o => o.Customer.Id)
//                 .ToList()
//                 .Select(grp => new{
//                     Name = _ctx.Customers.Find(grp.Key).Name,
//                     Total = grp.Sum(x => x.Total)
//                 }).OrderByDescending(res => res.Total)
//                 .Take(n)
//                 .ToList();

//             return Ok(groupedResult);    
//         }

//         [HttpGet("GetOrder/{id}", Name="GetOrder")]
//         public IActionResult GetOrder(int id)
//         {
//             var order = _ctx.Orders.Include(o => o.Customer)
//             .First(o => o.Id == id);

//             return Ok(order);
//         }

//     }
// }