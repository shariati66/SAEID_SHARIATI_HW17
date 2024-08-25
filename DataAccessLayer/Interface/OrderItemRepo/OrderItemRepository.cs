using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface.OrderItemRepo
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly SqlConnection _connection;
        public OrderItemRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public OrderItem? Find(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> GetAll()
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            using (var connection = _connection)
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT order_id,item_id,product_id,quantity,list_price,discount FROM sales.order_items";
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        orderItems.Add(new OrderItem
                        {
                            OrderId = reader.GetInt32(0),
                            ItemId = reader.GetInt32(1),
                            ProductId = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            ListPrice = reader.GetDecimal(4),
                            Discount= reader.GetDecimal(5)
                        });
                    }
                }
            }
            return orderItems;
        }

        public IEnumerable<OrderItem> GetAll(string keyName, int key)
        {
            throw new NotImplementedException();
        }
    }
}
