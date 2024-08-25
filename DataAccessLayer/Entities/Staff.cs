using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public byte Active { get; set; }
        public int StoreId { get; set; }
        public int ManagerId { get; set; }
        public string? StoreName { get; set; }
        public string? ManagerName { get; set; }

    }
}
