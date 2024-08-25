using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.StaffService
{
    public interface IStaffService:IGenericService<Staff>
    {
        IEnumerable<Staff> GetAll();
        IEnumerable<Staff> GetAll(string keyName, int key);
        //IEnumerable<Staff> GetAll(string keyName, int key);
    }
}
