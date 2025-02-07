using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductHub_master.Controllers
{
    public class CategoryController : Controller
    {
        private ProductHubDbContext db = new ProductHubDbContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }
        // POst  Category

        [HttpPost]  
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }



       // edit Get
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);


            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            Category c1 = new Category();   
               c1.CategoryId=category.CategoryId;   
                c1.CategoryName=category.CategoryName;

                db.Categories.AddOrUpdate(c1);  
                db.SaveChanges();

                return RedirectToAction("Index");
         }


        public ActionResult delete(int id)
        {
            var category=db.Categories.Find(id);    

            db.Categories.Remove(category); 
            db.SaveChanges();   
            return RedirectToAction("Index");

        }


    }
}