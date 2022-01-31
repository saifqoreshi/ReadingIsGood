using BookStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using BookStoreApi.Authentication;

namespace BookStoreApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingIsGoodController : ControllerBase
    {
        private static CustomerDTO CustomerDTO(Customer dto) => new CustomerDTO
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
        };
        private readonly BookStoreContext context;
        private readonly IAuthManager jwtAuthManager;

        public ReadingIsGoodController(BookStoreContext ctx, IAuthManager authManager)
        {
            context = ctx;
            this.jwtAuthManager = authManager;
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public ActionResult GetCustomers()
        {
            try
            {
                var customers = context.Customers.ToList();
                if (customers == null)
                    return NoContent();
                return Ok(customers.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        //[HttpGet("{id:int}")]
        //public IActionResult GetCustomer(int id)
        //{
        //    try
        //    {
        //        var customer = context.Customers.SingleOrDefault(c => c.CustomerId == id);
        //        if (customer == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(CustomerDTO(customer));
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching customer from the db.");
        //    }
        //}

        [HttpPost]
        [Route("CreateCustomer")]
        public IActionResult CreateCustomer(CustomerDTO dto)
        {
            if (dto == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);

            Customer customer = new Customer()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber
            };
            try
            {
                var created = context.Customers.Add(customer);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetCustomerOrders), new { customerId = created.Entity.CustomerId }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException.Message);
            }
        }

        [HttpGet]
        [Route("GetCustomerOrders/{customerId:int}")]
        public IActionResult GetCustomerOrders(int customerId)
        {
            var customer = context.Customers.Find(customerId);
            if (customer == null)
                return NotFound($"Customer with Id {customerId} not found");
            var orders = context.Orders.Where(o => o.CustomerId == customer.CustomerId).Select(s => new
            {
                s.OrderId,
                s.Customer.FirstName,
                s.Customer.LastName,
                s.OrderDate,
                s.Amount,
                s.Status,
                Items = s.OrderDetails.Count()
            });

            if (orders.Count() > 0)
            {
                return Ok(orders);
            }
            return NotFound($"Orders not found for the customer with Id: {customerId}");
        }

        [HttpGet]
        [Route("GetOrder/{orderId:int}")]
        public IActionResult GetOrder(int orderId)
        {
            var order = context.Orders.Where(o => o.OrderId == orderId);
            if (order == null)
                return NotFound($"Order with Id {orderId} not found");

            var details = context.Orders.Where(o => o.OrderId == orderId).
                          Select(o => new
                          {
                              o.OrderId,
                              CustomerName = o.Customer.FirstName + " " + o.Customer.LastName,
                              o.Amount,
                              o.OrderDate,
                              o.Status,
                              Details = context.OrderDetails.Where(d => d.OrderId == o.OrderId).Select(s => new
                              {
                                  s.Book.BookId,
                                  s.Quantity,
                                  s.Book.Title,
                                  s.Book.Author,
                                  s.Book.PublishedDate
                              }).ToList(),

                          });
            return Ok(details.ToList());
        }

        [HttpPost]
        [Route("CreateOrder")]
        public ActionResult CreateOrder(OrderDTO dto)
        {

            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);

            if (dto == null)
                return BadRequest();

            var order = new Order()
            {
                CustomerId = dto.CustomerId,
                Status = (byte)OrderStatus.Initiated,
                Amount = 0
            };

            try
            {
                var createdOrder = context.Orders.Add(order);
                context.SaveChanges();
                var details = new List<OrderDetail>();
                decimal sum = 0;

                foreach (var d in dto.Details)
                {
                    var book = context.Books.Find(d.BookId);

                    if (book == null)
                        return BadRequest($"Book with Id: {d.BookId} not found.");
                    else if (book.Quantity == 0)
                        return NotFound("Book with Id {d.BookId} is out of stock.");

                    details.Add(new OrderDetail
                    {
                        BookId = d.BookId,
                        OrderId = createdOrder.Entity.OrderId,
                        Quantity = d.Quantity
                    });
                    sum += book.Price.Value * d.Quantity;
                    book.Quantity = book.Quantity - d.Quantity;
                }
                order.Amount = sum;
                context.OrderDetails.AddRange(details);
                context.SaveChanges();
                var returnValue = new
                {
                    order.OrderId,
                    order.CustomerId,
                    order.Status,
                    order.Amount,
                    dto.Details
                };
                return CreatedAtAction(nameof(GetOrder), new { orderId = createdOrder.Entity.OrderId }, returnValue);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GeAllBooks()
        {
            var books = context.Books.ToList();
            if (books == null)
                return NotFound($"Books not found");
            return Ok(books);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Credentials userCred)
        {
            var token = jwtAuthManager.Authenticate(userCred.Username, userCred.Password);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}
