using System;
using System.Collections.Generic;
using System.Linq;
using FoodEyeAPI.Database;
using FoodEyeAPI.Models;
using FoodEyeAPI.Models.RequestResponseModels;
using FoodEyeAPI.Models.Table;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodEyeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class OrderController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public OrderController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Order/GetAllOrders
        [HttpGet("CustomerGetAllOrders/{customerId}")]
        public ActionResult<IEnumerable<CustomerOrderResponse>> CustomerGetAllOrders(string customerId)
        {
            var orders = _context.Orders
                .Where(order => order.UserId == customerId)
                .Select(order => new CustomerOrderResponse
                {
                    OrderId = order.OrderId,
                    ProductName = order.Product.ProductName, 
                    SellerName = order.Product.User.Name,     
                    Quantity = order.Quantity,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    OrderDetails = order.OrderDetails,                    
                })
                .ToList();

            return Ok(orders);
        }

        // GET: Order/SellerGetAllOrders/{sellerId}
        [HttpGet("SellerGetAllOrders/{sellerId}")]
        public ActionResult<IEnumerable<SellerOrderResponse>> SellerGetAllOrders(string sellerId)
        {
            var orders = _context.Orders
                .Where(order => order.Product.UserId == sellerId)
                .Select(order => new SellerOrderResponse
                {
                    OrderId = order.OrderId,
                    ProductName = order.Product.ProductName,
                    UserName = order.User.Name,
                    OrderDetails = order.OrderDetails,
                    Quantity = order.Quantity,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus
                })
                .ToList();

            return Ok(orders);
        }

        // GET: Order/GetOrderById/5
        [HttpGet("GetOrderById/{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: Order/AddOrder
        [HttpPost("AddOrder")]
        public ActionResult<Order> AddOrder(AddOrderRequest orderRequest)
        {
            try
            {
                var newOrder = new Order
                {
                    UserId= orderRequest.UserId,
                    ProductId= orderRequest.ProductId,
                    Quantity= orderRequest.Quantity,
                    OrderDate= orderRequest.OrderDate,
                    OrderDetails = orderRequest.OrderDetails,
                    OrderStatus= orderRequest.OrderStatus,
                };
                _context.Orders.Add(newOrder);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: Order/UserUpdateOrder/5
        [HttpPut("CustomerUpdateOrder")]
        public IActionResult CustomerUpdateOrder(CustomerUpdateOrderRequest updatedOrder)
        {
            var Orders = _context.Orders.Find(updatedOrder.OrderId);
            if (Orders == null)
            {
                return NotFound();
            }

            Orders.Quantity = updatedOrder.Quantity;
            Orders.OrderDetails = updatedOrder.OrderDetails;

            _context.SaveChanges();
            return Ok();
        }

        // PUT: Order/UserUpdateOrder/5
        [HttpPut("SellerUpdateOrder")]
        public IActionResult SellerUpdateOrder(SellerUpdateOrderRequest updatedOrder)
        {
            var Orders = _context.Orders.Find(updatedOrder.OrderId);
            if (Orders == null)
            {
                return NotFound();
            }

            Orders.OrderStatus = updatedOrder.OrderStatus; 

            _context.SaveChanges();
            return Ok();
        }

        // DELETE: Order/DeleteOrder/5
        [HttpDelete("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return Ok();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.OrderId == id);
        }
    }
}
