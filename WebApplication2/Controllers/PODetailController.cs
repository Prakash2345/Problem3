using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PODetailController : Controller
    {
        // GET: PODetail
        public ActionResult Index()
        {
            List<PODetailViewModel> supplier = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");
                //HTTP GET
                var responseTask = client.GetAsync("PODETAILs");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<PODetailViewModel>>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    supplier = new List<PODetailViewModel>();

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
        public ActionResult Create(PODetailViewModel PODetailViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/PODETAILs");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<PODetailViewModel>("PODETAILs", PODetailViewModel);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(PODetailViewModel);
        }
        public ActionResult Edit(string id)
        {
            PODetailViewModel PODetailViewModel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");
                //HTTP GET
                var responseTask = client.GetAsync("PODETAILs?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PODetailViewModel>();
                    readTask.Wait();

                    PODetailViewModel = readTask.Result;
                }
            }
            return View(PODetailViewModel);
        }
        [HttpPost]
        public ActionResult Edit(PODetailViewModel PODetailViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/PODETAILs");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<PODetailViewModel>("PODETAILs", PODetailViewModel);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(PODetailViewModel);
        }

        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("PODETAILs/" + id);
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