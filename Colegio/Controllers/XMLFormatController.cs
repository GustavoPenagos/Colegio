using Colegio.Models;
using Colegio.Models.XMLFormat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Colegio.Controllers
{
    public class XMLFormatController : Controller
    {
       public async Task<dynamic> XMLFormat()
        {
            try
            {
                List<XMLFormat> xmlList = new List<XMLFormat>();
                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/XML";
                HttpResponseMessage httpResponse = await client.GetAsync(apiCrud);
                if(httpResponse.IsSuccessStatusCode)
                {
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    List<XMLFormat> format = JsonConvert.DeserializeObject<List<XMLFormat>>(response);
                    

                    return View(format);
                }
                return View();
            }
            catch(Exception ex)
            {
                return View("Error", ex.Message);
            }
        }
    }
}
