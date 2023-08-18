using DBLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProducerApp.ProducerComponents;
using RabbitMqClient;

namespace ProducerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderDbContext _dbContext;
        private readonly IRabbitMqProducer<LogIntegrationEvent> _producer;

        public OrderController(OrderDbContext dbContext, IRabbitMqProducer<LogIntegrationEvent> producer)
        {
            _dbContext = dbContext;
            _producer = producer;
        }

        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult>? GetOrders()
        {
            try
            {
                List<Order> orders = await _dbContext.Orders!.ToListAsync();
                return Ok(orders);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody]Order order)
        {
            try
            {
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();

                var @event = new LogIntegrationEvent
                {
                    Id = Guid.NewGuid(),
                    Message = $"Order {order.ProductName} is sent successfully at {DateTime.Now:O}"
                };

                _producer.Publish(@event);

                return Ok(order);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
