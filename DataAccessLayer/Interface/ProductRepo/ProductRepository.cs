using DataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _connection;
        public ProductRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Product? Find(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (var connection = _connection)
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT product_id,product_name,brand_id,category_id,model_year,list_price FROM production.products";
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            BrandId = reader.GetInt32(2),
                            CategoryId = reader.GetInt32(3),
                            ModelYear = reader.GetInt16(4),
                            ListPrice = reader.GetDecimal(5),
                        });
                    }

                }
            }
            return products;
        }

        public IEnumerable<Product> GetAll(string keyName, int key)
        {
            throw new NotImplementedException();
        }
    }
}
