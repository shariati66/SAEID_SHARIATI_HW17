using DataAccessLayer.Entities;
using DataAccessLayer.Interface.StaffRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.StaffService
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService(IConfiguration configuration)
        {
            StaffRepository staffRepository = new StaffRepository(configuration);
            _staffRepository = staffRepository;
        }
        public IEnumerable<Staff> GetAll()
        {
           return _staffRepository.GetAll();
        }

        public IEnumerable<Staff> GetAll(string keyName, int key)
        {
            return _staffRepository.GetAll(keyName, key);
        }
    }
}
