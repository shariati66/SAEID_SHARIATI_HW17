using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IGenericService<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string keyName, int key);
    }
}
