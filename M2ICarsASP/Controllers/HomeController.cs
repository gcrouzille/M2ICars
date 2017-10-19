﻿using M2ICarsASP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
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

                //ViewBag.alert = "auth-success";
                ViewBag.script = "ClientAccount.js";
                return View("ClientAccount", cli);
                // vue ecran connecté
            }
        }

        public ActionResult ClientAccount()
        {
            return View();
        }

        public ActionResult DriverAccount()
        {
            Driver dri = new Driver();
            return View(dri);
        }

        public ActionResult Register()
        {

            return View();
        }

        public ActionResult RegisterPrest()
        {
            return View();
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

    }
}