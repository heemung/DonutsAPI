using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

namespace DonutAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HttpWebRequest requestDonut = WebRequest.CreateHttp(
            "https://grandcircusco.github.io/demo-apis/donuts.json");

            //making the useragent
            requestDonut.UserAgent = "Mozilla / 5.0(Windows NT 6.1; WOW64; rv: 64.0) Gecko / 20100101 Firefox / 64.0";

            // getting reponse
            HttpWebResponse response = (HttpWebResponse)requestDonut.GetResponse();

            //check for code 200
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //get response stream
                StreamReader reader = new StreamReader(response.GetResponseStream());

                //store it in string
                string readerString = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //convert to Json Object
                JObject jsonDonuts = JObject.Parse(readerString);

                ViewBag.OuterDonut = jsonDonuts["results"];
                return View();
            }
            else
            {
                ViewBag.WhatError = "Error getting API";
                return View("/Shared/Error");
            }

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

        public ActionResult Details(int id)
        {
            HttpWebRequest requestDonutDetails = WebRequest.CreateHttp(
            "https://grandcircusco.github.io/demo-apis/donuts/"+id+".json");

            //making the useragent
            requestDonutDetails.UserAgent = "Mozilla / 5.0(Windows NT 6.1; WOW64; rv: 64.0) Gecko / 20100101 Firefox / 64.0";

            // getting reponse
            HttpWebResponse response = (HttpWebResponse)requestDonutDetails.GetResponse();

            //check for code 200
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //get response stream
                StreamReader reader = new StreamReader(response.GetResponseStream());

                //store it in string
                string readerString = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //convert to Json Object
                JObject jsonDonut = JObject.Parse(readerString);

                ViewBag.InnerDonut = jsonDonut;
                return View();
            }
            else
            {
                ViewBag.WhatError = "Error getting detailed donut";
                return View("/Shared/Error");
            }

        }
    }
}