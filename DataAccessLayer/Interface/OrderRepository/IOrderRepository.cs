using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface.OrdersRepository
{
    public interface IOrderRepository:IGenericRepository<Order,int>
    {
    }
}
