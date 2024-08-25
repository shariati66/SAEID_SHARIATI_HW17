using BusinessLogicLayer.Model;
using BusinessLogicLayer.Services.CustomerService;
using BusinessLogicLayer.Services.OrderService;
using BusinessLogicLayer.Services.StaffService;
using DataAccessLayer.Interface.OrdersRepository;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace SAEID_SHARIATI_HW17.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICustomerService _customer;
        private readonly IOrderService _order;
        private readonly IStaffService _staff;
        public OrderController(IConfiguration configuration)
        {
            _customer = new CustomerService(configuration);
            _order = new OrderService(configuration);
            _staff = new StaffService(configuration);
        }
        public IActionResult Index()
        {
            OrderDetail orderdetail = null;
            return View(orderdetail);
        }
        [HttpPost]
        public IActionResult Index(int OrderId)
        {
            OrderDetail orderDetail = null;
            if (OrderId != 0)
            {
                var orders = _order.GetAll();
                orders = orders.Where(order => order.OrderId == OrderId);
                
                if (orders.Count() != 0)
                {
                    var customers = _customer.GetAll();
                    var staffs = _staff.GetAll();
                    var result = orders.Join(customers, order => order.CustomerId, customer => customer.CustomerId, (o, c) => new
                    {
                        OrderId = o.OrderId,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Phone = c.Phone,
                        Address = $"{c.City} {c.Street} {c.State} ",
                        OrderDate = o.OrderDate,
                        RequiredDate = o.RequiredDate,
                        ShippedDate = o.ShippedDate,
                        StaffId = o.StaffId
                    }).Join(staffs, o => o.StaffId, c => c.StaffId, (o, c) => new
                    {
                        OrderId = o.OrderId,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        Phone = o.Phone,
                        Address = o.Address,
                        OrderDate = o.OrderDate,
                        RequiredDate = o.RequiredDate,
                        ShippedDate = o.ShippedDate,
                        StaffFirstName = c.FirstName,
                        StaffLastname = c.LastName
                    }).ToList();
                    orderDetail = new()
                    {
                        OrderId = result[0].OrderId,
                        FirstName = result[0].FirstName,
                        LastName = result[0].LastName,
                        Phone = result[0].Phone,
                        Address = result[0].Address,
                        OrderDate = result[0].OrderDate,
                        RequiredDate = result[0].RequiredDate,
                        ShippedDate = result[0].ShippedDate,
                        StaffFirstName = result[0].StaffFirstName ?? "",
                        StaffLastName = result[0].StaffLastname ?? ""
                    };
                }
            }
            return View(orderDetail);
        }
    }
}
