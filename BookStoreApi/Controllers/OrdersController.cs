using BookStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly BookStoreContext context;
        public OrdersController(BookStoreContext ctx)
        {
            context = ctx;
        }


    }
}
