using DataAccessLayer.Entities;
using DataAccessLayer.Interface.OrderRepository;
using DataAccessLayer.Interface.OrdersRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IConfiguration configuration)
        {
            IOrderRepository orderRepository=new OrderRepository(configuration);
            _orderRepository=orderRepository;
        }
        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public IEnumerable<Order> GetAll(string keyName, int key)
        {
            return _orderRepository.GetAll(keyName, key);
        }
    }
}
