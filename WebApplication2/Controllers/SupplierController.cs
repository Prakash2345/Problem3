using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            List<SupplierViewModel> supplier = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");
                //HTTP GET
                var responseTask = client.GetAsync("SUPPLIERs");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<SupplierViewModel>>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    supplier = new List<SupplierViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(supplier);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SupplierViewModel supplierViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/SUPPLIERs");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<SupplierViewModel>("SUPPLIERs", supplierViewModel);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(supplierViewModel);
        }
        public ActionResult Edit(string id)
        {
            SupplierViewModel supplierViewModel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");
                //HTTP GET
                var responseTask = client.GetAsync("SUPPLIERs?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SupplierViewModel>();
                    readTask.Wait();

                    supplierViewModel = readTask.Result;
                }
            }
            return View(supplierViewModel);
        }
        [HttpPost]
        public ActionResult Edit(SupplierViewModel supplierViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/SUPPLIERs");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<SupplierViewModel>("SUPPLIERs", supplierViewModel);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(supplierViewModel);
        }

        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("SUPPLIERs/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}