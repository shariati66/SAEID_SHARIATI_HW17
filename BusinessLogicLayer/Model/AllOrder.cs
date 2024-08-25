using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Model
{
    public class AllOrder
    {
        public List<Order> Orders { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Staff> Staffs { get; set; }
    }
}
