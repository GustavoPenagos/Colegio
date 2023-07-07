using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using Colegio.Models;
using Microsoft.Ajax.Utilities;
using System.Collections.Specialized;
using System.Web.Razor.Generator;
using System.Web.WebPages;

namespace Colegio.Controllers
{
    public class HomeController : Controller
    {
        public async Task<dynamic> Index()
        {
            try
            {
                var dataAlumno = await Reporte();
                ViewBag.Message = "Reporte de estudiantes";
                return View(dataAlumno);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public ActionResult Calificar()
        {
            ViewBag.Message = "Pagina para calificar estudiantes.";

            return View();
        }

        public async Task<dynamic> ObtenerAsignauraAsync()
        {
            try
            {
                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/listar" + "?name=asignatura";
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    List<Asignatura> data = JsonConvert.DeserializeObject<List<Asignatura>>(response);
                    
                    return data;
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> Reporte()
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/reporte";
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    List<Reporte> data = JsonConvert.DeserializeObject<List<Reporte>>(response);


                    return data;
                }
                else
                {
                    return View("Error");
                }
            }
            catch(Exception ex)
            {
                return View("Error");
            }
        }
    }
}