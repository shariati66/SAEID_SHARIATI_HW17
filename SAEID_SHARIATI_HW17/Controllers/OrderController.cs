using BusinessLogicLayer.Model;
using BusinessLogicLayer.Services.CustomerService;
using BusinessLogicLayer.Services.OrderItemService;
using BusinessLogicLayer.Services.OrderService;
using BusinessLogicLayer.Services.ProductServiceSession;
using BusinessLogicLayer.Services.StaffService;
using DataAccessLayer.Entities;
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
        private readonly IOrderItemService _orderItem;
        private readonly IProductService _product;
        public OrderController(IConfiguration configuration)
        {
            _customer = new CustomerService(configuration);
            _order = new OrderService(configuration);
            _staff = new StaffService(configuration);
            _orderItem = new OrderItemService(configuration);
            _product = new ProductService(configuration);
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
                    var orderItems = _orderItem.GetAll();
                    var products = _product.GetAll();
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
                    var details = orderItems.Where(order => order.OrderId == OrderId).ToList().Join(products, o => o.ProductId, p => p.ProductId,(o,p)=> new
                    {
                        OrderId=o.OrderId,
                        ProductId = o.ProductId,
                        ProductName= p.ProductName,
                        Quantity = o.Quantity,
                        PriceProduct = o.ListPrice * o.Quantity,
                        DiscountProduct = o.ListPrice * o.Discount * o.Quantity,
                        PaymentProduct = (o.ListPrice * o.Quantity)-(o.ListPrice * o.Discount * o.Quantity),
                        
                    }).ToList();
                    List<OrderItemDetail> detailsOrder= new List<OrderItemDetail>();
                    foreach(var item in details)
                    {
                        OrderItemDetail orderItemDetail = new OrderItemDetail();
                        orderItemDetail.OrderId = item.OrderId;
                        orderItemDetail.ProductId = item.ProductId;
                        orderItemDetail.ProductName = item.ProductName;
                        orderItemDetail.Quantity= item.Quantity;
                        orderItemDetail.PriceProduct = item.PriceProduct;
                        orderItemDetail.DiscountProduct = item.DiscountProduct;
                        orderItemDetail.PaymentProduct = item.PaymentProduct;
                        detailsOrder.Add(orderItemDetail);
                    }
                   
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
                    orderDetail.orderItemDetails = detailsOrder;
                    orderDetail.TotalPrice =Math.Round( detailsOrder.Sum(o => o.PriceProduct),2);
                    orderDetail.Discount = Math.Round(detailsOrder.Sum(s=>s.DiscountProduct),2);
                    orderDetail.PurePayment =Math.Round(detailsOrder.Sum(s => s.PaymentProduct),2);
                }
            }
            return View(orderDetail);
        }
    }
}
