using BusinessLayer.Concrete;
using DataAccsessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using DataAccsessLayer;
using DataAccsessLayer.EntityFreamework;
using EntityLayer.Concrete;
using BusinessLayer.FluentValidation;
using FluentValidation.Results;

namespace Demo_Product.Controllers
{
    public class ProductController : Controller
    {
        ProductManager productManager = new ProductManager(new EfProductDal());

        public IActionResult Index()
        {
            var values = productManager.TGetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product p)
        {
            ProductValidator validationRules = new ProductValidator();
            ValidationResult result = validationRules.Validate(p);//how i validate it
            if (result.IsValid)
            {
                productManager.TInsert(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
        public IActionResult DeleteProduct(int id)
        {
            var value = productManager.TGetById(id);
            productManager.TDelete(value);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult UpdateProduct(int id) 
        {
            var value=productManager.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product p)
        {          
            productManager.TUpdate(p);
            return RedirectToAction("Index");
        }
    }
}
