using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ElvishTranslator.Models;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.AspNetCore.Http;
using System.Web; 

namespace ElvishTranslator.Controllers
{
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult DynamicImages()
        {
            return View(); 
        }

        //Method that is evoked when 'export' button is hit. It creates a file "export.xml" and exports all data on current page.
        public IActionResult Export()
        {

            XmlSerializer xmlSubmit = new XmlSerializer(typeof(List<ElvishTranslatorModel>));
            var model = GetWeather();
            string xmlstring = ""; 

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSubmit.Serialize(writer, model);
                    xmlstring = sww.ToString();                    

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlstring);
                    doc.Save("export.xml"); 

                }
            }

            return View(); 

        }

        //Returns view of Index
        public IActionResult Index()
        {
            return View();
        }

        //Returns view of WeatherView, using objects that are created in the GetWeather Method
        public IActionResult WeatherView()
        {
            return View(GetWeather()); 
        }

        //Method that returns a list of data, using an API
        public List<ElvishTranslatorModel> GetWeather()
        {
            List<ElvishTranslatorModel> weatherList = null;

            string strurltest = string.Format("http://www.7timer.info/bin/astro.php?lon=113.2&lat=23.1&ac=0&unit=metric&output=json&tzshift=0");
            WebRequest requestGet = WebRequest.Create(strurltest);
            requestGet.Method = "GET";
            HttpWebResponse responseGet = null;
            responseGet = (HttpWebResponse)requestGet.GetResponse();

            string strresulttest = null;
            
            //streamreader reads the stream, and the result is parsed into a JObject. After the result is put into a JArray and a list of objects is created, using this array.
            using (Stream stream = responseGet.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();
                sr.Close();

                JObject result = JObject.Parse(strresulttest);
                JArray array = (JArray)result["dataseries"];

                weatherList = array.ToObject<List<ElvishTranslatorModel>>();
            }

            return weatherList; 

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
