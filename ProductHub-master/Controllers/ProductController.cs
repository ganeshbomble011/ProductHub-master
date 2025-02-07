using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using System.Data.Entity;


using PagedList;
using System.Data.Entity.Migrations;

namespace ProductHub_master.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductHubDbContext db= new ProductHubDbContext();


        // GET: Product
       public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var products = db.Products.Include(p => p.Category)
                                      .OrderBy(p => p.ProductId)
                                      .Skip((pageNumber - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalRecords = db.Products.Count();
            return View(products);
        }



        // GET: Product/Create
        public ActionResult Create()
        {


            /* var a = db.Categories.Select(x => new SelectListItem
             {
                 Value=x.CategoryId.ToString(),  
                 Text=x.CategoryName,

             }).ToList();*/

            /* ViewBag.tempcat = a;*/

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", "Categorybrand");



            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // delete Product
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);

              db.Products.Remove(product);
            db.SaveChanges();
              return RedirectToAction("Index"); 
        }

        //  Edit product  Record

        public  ActionResult Edit(int id)
        {
            var product=db.Products.Find(id);
            
            if(product == null)
            {
                return HttpNotFound();  
            }
            
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);

        }
        // post  Edit

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                Product p1 = new Product();


                p1.CategoryId = product.CategoryId;

                p1.ProductName = product.ProductName;
                p1.ProductId = product.ProductId;
                db.Products.AddOrUpdate(p1);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(product);       
          
        }









    }
}