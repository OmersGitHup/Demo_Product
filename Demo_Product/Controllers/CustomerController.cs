using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccsessLayer.EntityFreamework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo_Product.Controllers
{
    public class CustomerController : Controller
    {
        JobManager jobManager = new JobManager(new EfJobDal());
        CustomerManager customerManager = new CustomerManager(new EfCustomerDal());

        public IActionResult Index()
        {
            var values = customerManager.GetCustomersListWithJob();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCustomer() 
        {
           
            List<SelectListItem> values = (from x in jobManager.TGetList()
                                          select new SelectListItem
                                          {
                                              Text = x.Name,
                                              Value=x.JobID.ToString(),
                                          }).ToList();//Using dropdown menu with values
            ViewBag.v=values;
            return View();
        }
        public IActionResult AddCustomer(Customer p)
        {
            CustomerValidator validationRules = new CustomerValidator();
            ValidationResult result = validationRules.Validate(p);//how i validate it
            if (result.IsValid)
            {
                customerManager.TInsert(p);
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

        public IActionResult Delete(int id) 
        {
            var value =customerManager.TGetById(id);
            customerManager.TDelete(value);
            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult UpdateCustomer(int id)
        {
            List<SelectListItem> values = (from x in jobManager.TGetList()
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.JobID.ToString(),
                                           }).ToList();//Using dropdown menu with values
            ViewBag.v = values;
            var value = customerManager.TGetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer p)
        {
            customerManager.TUpdate(p);
            return RedirectToAction("Index");
        }
    }
}
