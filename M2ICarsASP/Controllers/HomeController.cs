﻿using M2ICarsASP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace M2ICarsASP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string msg)
        {
            ViewBag.Info = msg;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email, string pass)
        {
            string token = null;
            Task.Run(async () =>
            {
                token = await GetToken(email,pass); // vals form
            }).Wait();
            if (token == "TokenError")
            {
                ViewBag.alert = "auth-error";
                return View("Index");
            }
            else
            {
                Session["token"] = token;
                Client cli = null;
                cli = await GetUser(email);
                Session["clientId"] = cli.UserId;

                return RedirectToAction("Index", "Search");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", new { msg = "Déconnexion réussie !" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginPrest(string prestemail, string prestpass)
        {
            string token = null;
            Task.Run(async () =>
            {
                token = await GetTokenDriver(prestemail, prestpass); // vals form
            }).Wait();
            if (token == "TokenError")
            {
                ViewBag.alert = "auth-error";
                return View("Index");
            }
            else
            {
                Session["token"] = token;
                Driver driv = null;
                driv = await GetDriver(prestemail);

                //ViewBag.alert = "auth-success";
                ViewBag.script = "ClientAccount.js";
                return View("DriverAccount", driv);
                // vue ecran connecté
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> ClientAccount()
        {
            Client c = await APIService.Instance.Request<Client>("GET", $"api/User/{Session["clientId"]}");
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ClientAccount([Bind(Include = "Email, FirstName, LastName, Gender, Phone, Birthday")] Client client, HttpPostedFileBase PhotoUrl)
        {
            if (PhotoUrl != null && PhotoUrl.ContentLength > 0)
            {
                string FileName = Path.GetFileName(PhotoUrl.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Images"), FileName);
                PhotoUrl.SaveAs(path);
                client.PhotoUrl = FileName;
            }

            client.UserId = (int)Session["clientId"];
            await APIService.Instance.Request("PUT", $"api/User/{client.UserId}", client);
            Client c = await APIService.Instance.Request<Client>("GET", $"api/User/{Session["clientId"]}");
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUserPassword(string oldpass, string newpass)
        {
            Client c = await APIService.Instance.Request<Client>("GET", $"api/User/{Session["clientId"]}");
            
            await APIService.Instance.Request("PUT", $"api/User/Pass/{Session["clientId"]}", new {
                userId=Session["clientId"],
                oldPassword = oldpass,
                newPassword = newpass
            });
            
            return View("ClientAccount", c);
        }

        public ActionResult DriverAccount()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            Client c = new Client();
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Email, Password, FirstName, LastName, Gender, Phone, Birthday")] Client c, HttpPostedFileBase PhotoUrl)
        {
            if (PhotoUrl != null && PhotoUrl.ContentLength > 0)
            {
                string FileName = Path.GetFileName(PhotoUrl.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Images"), FileName);
                PhotoUrl.SaveAs(path);
                c.PhotoUrl = FileName;
            }

            Client client = await APIService.Instance.Request("POST", "api/Users", c);
            if (client != null)
                return RedirectToAction("Index", new { msg = "Inscription réussie ! Vous pouvez maintenant vous connecter" });

            return View("Register", c);
        }


        public ActionResult RegisterPrest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterPrest(Driver d)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:64548/api/Drivers");

                var postTask = client.PostAsJsonAsync<Driver>("Drivers", d);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Champ manquant ou imcomplet");

            return View("RegisterPrest");
        }


        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Exemple de requête vers l'api
        public async Task<string> DriverInfo(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Drivers/{id}");
            if (response.IsSuccessStatusCode)
            {                
                s = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(s);

                return (string)jsonObject["Lastname"];
            }
            return s;
        }

         // Méthode asynchrone servant à récupérer le token pour un utilisateur
        public async Task<string> GetToken(string mail, string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Account/User/Authenticate?mail={mail}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<string>();
            }
            return s;
        }

        public async Task<string> GetTokenDriver(string mail, string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Account/Driver/Authenticate?mail={mail}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<string>();
            }
            return s;
        }

        // Exemple de requête vers l'api prenant en compte si l'utilisateur a un token ou non
        public async Task<string> TestToken()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

            HttpResponseMessage response = await client.GetAsync($"api/Account/User/Get?id=1");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsAsync<string>();
            }
            else
            {
                s = Session["token"].ToString();
            }
            return s;
        }

        public async Task<Client> GetUser(string email)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64548/");
            client.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

            string firstPart = email.Split('.')[0];
            string secondPart = email.Split('.')[1];
            HttpResponseMessage response = await client.GetAsync($"api/User/mail/{firstPart}/{secondPart}");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                Client c = JsonConvert.DeserializeObject<Client>(s);
                return c;
            }
            return null;
        }

        public async Task<Driver> GetDriver(string email)
        {
            HttpClient driver = new HttpClient();
            driver.BaseAddress = new Uri("http://localhost:64548/");
            driver.DefaultRequestHeaders.Accept.Clear();
            string s = null;
            driver.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            driver.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["token"].ToString());

            string firstPart = email.Split('.')[0];
            string secondPart = email.Split('.')[1];
            HttpResponseMessage response = await driver.GetAsync($"api/Driver/mail/{firstPart}/{secondPart}");
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                Driver d = JsonConvert.DeserializeObject<Driver>(s);
                return d;
            }
            return null;
        }


    }
}