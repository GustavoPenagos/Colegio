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
                //var dataAlumno = await new ListsController().ObtenerAlumnoAsync();

                return View();
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
                List<Asignaturas> asignaturasList = new List<Asignaturas>();

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "ListaAsignaura";
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach (DataRow row in data.Rows)
                    {
                        Asignaturas asignatura = new Asignaturas();
                        asignatura.Codigo = Convert.ToInt32(row["Codigo"].ToString());
                        asignatura.Nombre = row["Nombre"].ToString();
                        asignaturasList.Add(asignatura);
                    }
                    
                    return asignaturasList;                    
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
    }
}