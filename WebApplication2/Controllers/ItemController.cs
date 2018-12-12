using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            List<ItemViewModel> supplier = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");
                //HTTP GET
                var responseTask = client.GetAsync("ITEMs");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<ItemViewModel>>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    supplier = new List<ItemViewModel>();

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
        public ActionResult Create(ItemViewModel ItemViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/ITEMs");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ItemViewModel>("ITEMs", ItemViewModel);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(ItemViewModel);
        }
        public ActionResult Edit(string id)
        {
            ItemViewModel ItemViewModel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");
                //HTTP GET
                var responseTask = client.GetAsync("ITEMs?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ItemViewModel>();
                    readTask.Wait();

                    ItemViewModel = readTask.Result;
                }
            }
            return View(ItemViewModel);
        }
        [HttpPost]
        public ActionResult Edit(ItemViewModel ItemViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/ITEMs");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<ItemViewModel>("ITEMs", ItemViewModel);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(ItemViewModel);
        }

        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("ITEMs/" + id);
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