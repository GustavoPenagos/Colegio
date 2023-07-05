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
        //public ActionResult Eliminar(string id, string name)
        //{
        //    return RedirectToAction("ListaAlumnos", "Lists");
        //}

        public async Task<dynamic> Editar(string id, string asg, string name)
        {
            var result = await new ListsController().Buscar(id, asg, name, "Editar");

            string json = JsonConvert.SerializeObject(result);

            switch (name)
            {
                case "alumno":
                    return RedirectToAction("RegistroAlumno", "Register", new { alumno = json });
                case "profesor":
                    return RedirectToAction("RegistroProfesor", "Register", new { profesor = json });
                case "asignatura":
                    return RedirectToAction("RegistroAsignatura", "Register", new { asignatura = json });
                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        public async Task<dynamic> ActualizarAlumno(string id, string nombre, string apellido, string edad, string direccion, string telefono, string asignatura, string calificacion = null)
        {
            try
            {
                Alumno alumno = new Alumno();
 
                alumno.Nombre = nombre;
                alumno.Apellido = apellido;
                alumno.Edad = edad;
                alumno.Direccion = direccion;
                alumno.Telefono = telefono;
                alumno.Asignatura = asignatura;
                alumno.Calificacion = calificacion == null ? "" : calificacion;

                var jsonAlumno = new StringContent(JsonConvert.SerializeObject(alumno), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/actualizar/alumno" + "?id=" + id + "&asg=" + asignatura;
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

        public async Task<dynamic> ActualizarProfesor(string id, string nombre, string apellido, string edad, string direccion, string telefono, string asignatura)
        {
            try
            {
                Profesor alumno = new Profesor();

                alumno.Nombre = nombre;
                alumno.Apellido = apellido;
                alumno.Edad = edad;
                alumno.Direccion = direccion;
                alumno.Telefono = telefono;
                alumno.Asignatura = asignatura;


                var jsonProfesor = new StringContent(JsonConvert.SerializeObject(alumno), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/actualizar/profesor" + "?id=" + id;
                HttpResponseMessage httpResponse = await client.PostAsync(apiCrud, jsonProfesor);
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "¡Datos guardados correctamente!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("ListaProfesores", "Lists");
                }
                else
                {
                    TempData["AlertMessage"] = "¡Datos no guardados!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("ListaProfesores", "Lists");
                }

            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> ActualizarAsignatura(string id, string nombre)
        {
            try
            {
                Asignatura alumno = new Asignatura();

                alumno.Nombre = nombre;

                var jsonAsignatura = new StringContent(JsonConvert.SerializeObject(alumno), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/actualizar/asignatura" + "?id=" + id;
                HttpResponseMessage httpResponse = await client.PostAsync(apiCrud, jsonAsignatura);
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "¡Datos guardados correctamente!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("ListaAsignatura", "Lists");
                }
                else
                {
                    TempData["AlertMessage"] = "¡Datos no guardados!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("ListaAsignatura", "Lists");
                }

            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> Eliminar(string id, string asg, string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/eliminar" + "?id=" + id + "&asg=" + asg + "&name=" + name;
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
