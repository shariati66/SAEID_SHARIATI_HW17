using BusinessLogicLayer.Services.StaffService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SAEID_SHARIATI_HW17.Controllers
{
    public class StaffController : Controller
    {
        private readonly StaffService _staffService;
        public StaffController(IConfiguration configuration)
        {
            _staffService = new StaffService(configuration);
        }
        // GET: StaffController
        public ActionResult Index()
        {
            var staffs = _staffService.GetAll().ToList();
            return View(staffs);
        }
        [HttpPost]
        public IActionResult Index([FromForm] int storeId, [FromForm] int staffId)
        {
            var staffs = _staffService.GetAll().ToList();
            if (storeId != 0 && staffId != 0)
            {
                var staffsResult = staffs.Where(staff => staff.StoreId == storeId && staff.StaffId == staffId).ToList();
                staffs = staffsResult;
            }

            else if (storeId != 0)
            {
                var staffsWithStore = staffs.Where(staff => staff.StoreId == storeId).ToList();
                staffs = staffsWithStore;
            }
            else if (staffId != 0)
            {
                var staffsWithManager = staffs.Where(staff => staff.StaffId == staffId).ToList();
                staffs = staffsWithManager;
            }
            return View(staffs);

        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StaffController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
