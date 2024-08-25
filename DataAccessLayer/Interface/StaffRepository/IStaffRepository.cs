using DataAccessLayer.Data.IDataAccess;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface.StaffRepository
{
    public interface IStaffRepository:IGenericRepository<Staff,int>
    {
        IEnumerable<Staff> GetWithManagerId(int managerId);
        //IEnumerable<Staff> GetAll();
        //IEnumerable<Staff> GetAll(string keyName, int key);
        //Staff? Find(int key);
    }
}
