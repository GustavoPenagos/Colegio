using Antlr.Runtime.Misc;
using Colegio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Colegio.Controllers
{
    public class CrudController : Controller
    {
        public ActionResult Eliminar(string id, string name)
        {
            return RedirectToAction("ListaAlumnos", "Lists");
        }

        public async Task<dynamic> Editar(string id, string asg, string name)
        {
            var Alumno = await new ListsController().Buscar(id, asg, name, "Editar");

            string json = JsonConvert.SerializeObject(Alumno);

            return RedirectToAction("RegistroAlumno", "Register", new { alumno = json});
        }

        public async Task<dynamic> ActualizarAlumno(string identificacion, string nombre, string apellido, string edad, string direccion, string telefono, string asignatura, string calificacion = null)
        {
            try
            {
                Alumno alumno = new Alumno();
                alumno.Id_Alumno = identificacion;
                alumno.Nombre = nombre;
                alumno.Apellido = apellido;
                alumno.Edad = edad;
                alumno.Direccion = direccion;
                alumno.Telefono = telefono;
                alumno.Asignatura = asignatura;
                alumno.Calificacion = calificacion == null ? "" : calificacion;

                var jsonAlumno = new StringContent(JsonConvert.SerializeObject(alumno), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/actualizar/alumnos";
                HttpResponseMessage httpResponse = await client.PostAsync(apiCrud, jsonAlumno);
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "¡Datos guardados correctamente!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("ListaAlumnos", "Lists");
                }
                else
                {
                    TempData["AlertMessage"] = "¡Datos no guardados!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("ListaAlumnos", "Lists");
                }

            }
            catch(Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> EliminarAlumno(string id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/eliminar/alumno?id=" + id;
                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if(httpResponse.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "¡Datos guardados correctamente!";
                    TempData["AlertType"] = "success";   
                }
                else
                {
                    TempData["AlertMessage"] = "¡Datos no guardados!";
                    TempData["AlertType"] = "error";
                }
                return RedirectToAction("ListaAlumnos", "Lists");

            }
            catch(Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

    }
}
