using DataAccessLayer.Entities;
using DataAccessLayer.Interface.OrderItemRepo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderItemService(IConfiguration configuration)
        {
            _orderItemRepository = new OrderItemRepository(configuration);
        }
        public IEnumerable<OrderItem> GetAll()
        {
            return _orderItemRepository.GetAll();   
        }

        public IEnumerable<OrderItem> GetAll(string keyName, int key)
        {
            throw new NotImplementedException();
        }
    }
}
