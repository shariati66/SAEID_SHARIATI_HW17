using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnection _connection;
        public CustomerRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
       
        public Customer? Find(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            using (var conn = _connection)
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT customer_id,first_name,last_name,ISNULL(phone,0) AS phone,email," +
                    "ISNULL(street,'') AS street,ISNULL(city,'') AS city,ISNULL(state,'') AS state," +
                    "ISNULL(zip_code,'') AS zip_code FROM sales.customers";
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId=reader.GetInt32(0),
                            FirstName=reader.GetString(1),
                            LastName=reader.GetString(2),
                            Phone=reader.GetString(3),
                            Email=reader.GetString(4),
                            Street=reader.GetString(5),
                            City=reader.GetString(6),
                            State=reader.GetString(7),
                            ZipCode=reader.GetString(8)
                        });
                    }
                }
            }
            return customers;
        }

        public IEnumerable<Customer> GetAll(string keyName, int key)
        {
            throw new NotImplementedException();
        }
    }
}
