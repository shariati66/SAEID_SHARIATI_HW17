using DataAccessLayer.Entities;
using DataAccessLayer.Interface.ProductRepo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.ProductServiceSession
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IConfiguration configuration)
        {
            _productRepository = new ProductRepository(configuration);
        }
        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyName, int key)
        {
            throw new NotImplementedException();
        }
    }
}
