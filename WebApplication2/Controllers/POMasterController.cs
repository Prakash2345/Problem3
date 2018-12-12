using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class POMasterController : Controller
    {
        // GET: POMaster
        public ActionResult Index()
        {
            List<POMasterViewModel> supplier = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");
                //HTTP GET
                var responseTask = client.GetAsync("POMASTERs");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<POMasterViewModel>>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    supplier = new List<POMasterViewModel>();

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
        public ActionResult Create(POMasterViewModel POMasterViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/POMASTERs");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<POMasterViewModel>("POMASTERs", POMasterViewModel);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(POMasterViewModel);
        }
        public ActionResult Edit(string id)
        {
            POMasterViewModel POMasterViewModel = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");
                //HTTP GET
                var responseTask = client.GetAsync("POMASTERs?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<POMasterViewModel>();
                    readTask.Wait();

                    POMasterViewModel = readTask.Result;
                }
            }
            return View(POMasterViewModel);
        }
        [HttpPost]
        public ActionResult Edit(POMasterViewModel POMasterViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/POMASTERs");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<POMasterViewModel>("POMASTERs", POMasterViewModel);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(POMasterViewModel);
        }

        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:19450/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("POMASTERs/" + id);
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