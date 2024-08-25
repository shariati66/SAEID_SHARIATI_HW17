using DataAccessLayer.Entities;
using DataAccessLayer.Interface.CustomerRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(IConfiguration configuration)
        {
            ICustomerRepository customerRepo = new CustomerRepository(configuration);
            _customerRepository = customerRepo;

        }
        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public IEnumerable<Customer> GetAll(string keyName, int key)
        {
            throw new NotImplementedException();
        }
    }
}
