using DataAccessLayer.Entities;
using DataAccessLayer.Interface.OrdersRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SqlConnection _connection;
        public OrderRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Order? Find(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            using (var connection = _connection)
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "Select order_id,ISNULL(customer_id,0) AS customer_id,order_status,order_date," +
                    "required_date,shipped_date,store_id,staff_id FROM sales.orders";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            OrderId = reader.GetInt32(0),
                            CustomerId = reader.GetInt32(1),
                            OrderStatus = reader.GetByte(2),
                            OrderDate = reader.GetDateTime(3),
                            RequiredDate = reader.GetDateTime(4),
                            ShippedDate = (reader.IsDBNull(5) == false) ? reader.GetDateTime(5) : new DateTime(),
                            StoreId = reader.GetInt32(6),
                            StaffId = reader.GetInt32(7)
                        });
                    }
                }
            }
            return orders.ToList();
        }

        public IEnumerable<Order> GetAll(string keyName, int key)
        {
            List<Order> orders = new List<Order>();
            using (var connection = _connection)
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "Select order_id,ISNULL(customer_id,0) AS customer_id,order_status,order_date," +
                    "required_date,ISNULL(shipped_date,0) AS shipped_date,store_id,staff_id FROM sales.orders " +
                    $"WHERE {keyName}={key}";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            OrderId = reader.GetInt32(0),
                            CustomerId = reader.GetInt32(1),
                            OrderStatus = reader.GetByte(2),
                            OrderDate = reader.GetDateTime(3),
                            RequiredDate = reader.GetDateTime(4),
                            ShippedDate = reader.GetDateTime(5),
                            StoreId = reader.GetInt32(6),
                            StaffId = reader.GetInt32(7)
                        });
                    }
                }
            }
            return orders.ToList();
        }
    }
}
