using Cake_Shop.Data;
using Cake_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography.Xml;

namespace Cake_Shop.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public DashboardController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {

            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize]
        public IActionResult Index()

        {
            return View();
        }

        //Products

        public IActionResult CreateNewProducts(Products products, IFormFile photo)
        {



            if (photo == null || photo.Length == 0)
            {
                return Content("File Not Selected");
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName); //path

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                photo.CopyTo(stream);
                stream.Close();
            }

            products.Image = photo.FileName;
            _context.Add(products); //creat new record 
            _context.SaveChanges(); // تجي معهااا 
            return RedirectToAction("products");

        }

        //if (ModelState.IsValid)
        //{
        //    _context.Add(products); // إنشاء سجل جديد
        //    _context.SaveChanges(); // حفظ التغييرات
        //    TempData["Save"] = "تمت عملية الحفظ";
        //    return RedirectToAction("products");
        //}

        //TempData["Save"] = "لم تتم عملية الحفظ";
        //return View("products");



        public IActionResult Products(string name)
        {
            if (name != null)
            {
                var search = _context.products.Where(p => p.Name.Contains(name)).ToList();
                return View(search);
            }

            var getdata = _context.products.ToList();

            return View(getdata);

        }

        //search
        [HttpPost]
        public IActionResult Advance_search(string Name)
        {
            if (Name != null && Name.Length > 0)
            {
                var search = _context.products.Where(p => p.Name.Contains(Name)).ToList();
                return PartialView("_partial/productspartial", search);
            }


            var product = _context.products.ToList();

            return PartialView("_partial/productspartial", product);
        }



        public IActionResult DeleteProduct(int id)
        {
            var products = _context.products.SingleOrDefault(c => c.Id == id); //search
            if (products != null)
            {
                _context.products.Remove(products);
                _context.SaveChanges();
            }

            var product = _context.products.ToList();

            return PartialView("_partial/productspartial", product);
        }


        //Update
        public IActionResult EditProducts(int id  /*Products product , IFormFile Image*/ )
        {
            var edit_products = _context.products.SingleOrDefault(e => e.Id == id);
            //if (edit_products == null)

            //    if (Image != null && Image.Length > 0)
            //    {
            //        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Image.FileName);

            //        using (var stream = new FileStream(imagePath, FileMode.Create))
            //        {
            //            Image.CopyTo(stream);
            //        }

            //        edit_products.Image = Image.FileName; // حفظ اسم الصورة الجديدة
            //    }

            _context.SaveChanges();
            //return RedirectToAction("Products");


            return View(edit_products);
        }

        public IActionResult UpdateProducts(Products products)
        {
            _context.products.Update(products);
            _context.SaveChanges();

            return RedirectToAction("Products");
        }

        public IActionResult Details(int id)
        {
            var products = _context.products.SingleOrDefault(p => p.Id == id); // ابحث عن المنتج بناءً على المعرف
            if (products == null)
            {
                return NotFound(); // إذا لم يتم العثور على المنتج
            }
            return View(products); // إرسال المنتج إلى الصفحة
        }




        //Customers

        public IActionResult CreateNewCustomers(Customers customers)
        {
            _context.Add(customers);
            _context.SaveChanges();
            return RedirectToAction("customers");
        }

        public IActionResult Customers()
        {

            var getdata = _context.customers.ToList();

            return View(getdata);

        }

        

        public IActionResult DeleteCustomers(int id)
        {
            var customers = _context.customers.SingleOrDefault(c => c.Id == id); //search
            if (customers != null)
            {
                _context.customers.Remove(customers);
                _context.SaveChanges();
            }

            return RedirectToAction("Customers");
        }

    



        // Orders
        public IActionResult CreateNewOrders(Orders orders)
        {
         

            _context.Add(orders);
            _context.SaveChanges();
            return RedirectToAction("orders");


        }

        public IActionResult Orders()
        {


            var orders = _context.orders.ToList();
            var products = _context.products.ToList();
            var customers = _context.customers.ToList();
            var status = _context.status.ToList();

            ViewBag.Products = products;
            ViewBag.Customers = customers;
            ViewBag.OrderStatus = status;

            var getproducts = _context.orders
                .Join(_context.products,
                      order => order.Products_id,
                      product => product.Id,
                      (order, product) => new { order, product })
                .Join(_context.customers,
                      op => op.order.CustomerId,
                      customer => customer.Id,
                      (op, customer) => new { op.order, op.product, customer })
                .Join(_context.status,
                      opc => opc.order.StatusId,
                      status => status.Id,
                      (opc, status) => new
                      {
                          Id = opc.order.Id,
                          OrdersId = opc.order.OrderId,
                          Products_id = opc.product.Id,
                         /* ProductName = opc.product.Id, */// مثال إضافة اسم المنتج
                          Customer_id = opc.customer.Id,
                        /*  CustomerName = opc.customer.Name, */// مثال إضافة اسم العميل
                          StatusId = status.Id,
                        /*  StatusName = status.Name, */// مثال إضافة اسم الحالة
                          Quantity = opc.order.Quantity,
                          OrderDate = opc.order.OrderDate
                      })
                .ToList();

            ViewBag.Getproducts = getproducts;

            return View();
        }


        






        public IActionResult DeleteOrders(int id)
        {
            var orders = _context.orders.SingleOrDefault(c => c.Id == id); //search
            if (orders != null)
            {
                _context.orders.Remove(orders);
                _context.SaveChanges();
            }

              var order = _context.orders.ToList();

            return PartialView("_partial/orderspartial" ,order);


        }

        public IActionResult CreateNewOrderStatus(OrderStatus status)
        {
            _context.Add(status);
            _context.SaveChanges();
            return RedirectToAction("status");
        }

        public IActionResult OrderStatus()
        {
            var getdata = _context.status.ToList();

            return View(getdata);

        }










    }
}
