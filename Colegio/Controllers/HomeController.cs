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

namespace Colegio.Controllers
{
    public class HomeController : Controller
    {
        public async Task<dynamic> Index()
        {
            try
            {
                List<Alumnos> alumnosList = new List<Alumnos>();
                List<Asignaturas> asignaturasList= new List<Asignaturas>();

                HttpClient client = new HttpClient();
                
                    string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlCrud"];
                    HttpResponseMessage ResponseAlumno = await client.GetAsync(apiCrud);
                    if (ResponseAlumno.IsSuccessStatusCode)
                    {
                        string response = await ResponseAlumno.Content.ReadAsStringAsync();
                        DataTable data = JsonConvert.DeserializeObject<DataTable>(response);

                        for (int i=0; i<data.Rows.Count;i++)
                        {
                            Alumnos alumno = new Alumnos();
                            alumno.Identificacion = data.Rows[i].ItemArray[0].ToString();
                            alumno.Nombre = data.Rows[i].ItemArray[1].ToString();
                            alumno.Apellido = data.Rows[i].ItemArray[2].ToString();
                            alumno.Edad = Convert.ToInt16(data.Rows[i].ItemArray[3].ToString());
                            alumno.Direccion = data.Rows[i].ItemArray[4].ToString();
                            alumno.Telefono = data.Rows[i].ItemArray[5].ToString();
                            alumno.Cod_Asignatura = data.Rows[i].ItemArray[6].ToString();
                            alumno.calificacion = data.Rows[i].ItemArray[7].ToString();
                            alumnosList.Add(alumno);
                            
                        }
                        ViewBag.Message = "Hola Samuelito";
                        return View(alumnosList);
                    }
                    else
                    {
                        return View("Error");
                    }
                

                
            }
            catch(Exception ex) 
            {
                return View("Error", ex.Message);
            }
        }

        public ActionResult Calificar()
        {
            ViewBag.Message = "Pagina para calificar estudiantes.";

            return View();
        }

        public ActionResult RegistroAlumno()
        {
            ViewBag.Message = "Pagina de registro";

            return View();
        }
        public ActionResult RegistroProfesor()
        {
            ViewBag.Message = "Pagina de registro";

            return View();
        }
    }
}