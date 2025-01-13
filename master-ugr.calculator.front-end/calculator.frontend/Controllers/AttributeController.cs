using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace calculator.frontend.Controllers
{
    public class AttributeController : Controller
    {
        private readonly string baseUrl =
            Environment.GetEnvironmentVariable("CALCULATOR_BACKEND_URL") ??
            "https://master-ugr-ci-backend-uat.azurewebsites.net";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        private KeyValuePair<string, string> ExecuteOperation(string number)
        {
            bool? rawPrime = null;
            bool? rawOdd = null;

            try
            {
                using var client = new HttpClient();
                var url = $"{baseUrl}/api/Calculator/number_attribute?number={number}";
                var response = client.GetAsync(url).Result;

                response.EnsureSuccessStatusCode(); // Ensure HTTP 2xx

                var body = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(body);

                rawPrime = json["prime"]?.Value<bool>();
                rawOdd = json["odd"]?.Value<bool>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during ExecuteOperation: {ex.Message}");
            }

            var isPrime = DetermineAttributeString(rawPrime);
            var isOdd = DetermineAttributeString(rawOdd);

            return new KeyValuePair<string, string>(isPrime, isOdd);
        }

        [HttpPost]
        public IActionResult Index(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                ModelState.AddModelError("number", "Please provide a valid number.");
                return View();
            }

            var result = ExecuteOperation(number);
            ViewBag.IsPrime = result.Key;
            ViewBag.IsOdd = result.Value;

            return View();
        }

        private string DetermineAttributeString(bool? attribute)
        {
            return attribute == true ? "Yes" : attribute == false ? "No" : "unknown";
        }
    }
}
