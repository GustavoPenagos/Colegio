using Colegio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Colegio.Controllers
{
    public class RegisterController : Controller
    {
       public async Task<dynamic> RegistroAlumno()
        {
            try
            {
                await Asignaturas("A");

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> RegistroProfesor()
        {
            try
            {
                await Asignaturas("P");

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public ActionResult RegistroAsignatura()
        {

            return View();
        }

        public ActionResult RegistrarClase()
        {
            return View();
        }

        public async Task<dynamic> InsertAlumno(string identificacion, string nombre, string apellido, string edad, string direccion, string telefono)
        {
            try
            {
                Alumnos alumno = new Alumnos();

                alumno.Identificacion = identificacion;
                alumno.Nombre = nombre;
                alumno.Apellido = apellido;
                alumno.Edad = Convert.ToInt32(edad);
                alumno.Direccion = direccion;
                alumno.Telefono = telefono;

                var jsonAlumno = new StringContent(JsonConvert.SerializeObject(alumno), Encoding.UTF8, "application/json");


                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "RigistroAlumno";
                HttpResponseMessage httpResponse = await client.PostAsync(apiCrud, jsonAlumno);
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "¡Datos guardados correctamente!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("RegistroAlumno", "Register");
                }
                else
                {
                    TempData["AlertMessage"] = "¡Datos no guardados!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("RegistroAlumno", "Register");
                }
            }
            catch(Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> InsertProfesor(string identificacion, string nombre, string apellido, string edad, string direccion, string telefono, string opciones)
        {
            try
            {
                Profesores profesores = new Profesores();

                profesores.Identificacion = identificacion;
                profesores.Nombre = nombre;
                profesores.Apellido = apellido;
                profesores.Edad = Convert.ToInt32(edad);
                profesores.Direccion = direccion;
                profesores.Telefono = telefono;
                profesores.Cod_Asignatura = Convert.ToInt32(opciones);
                

                var jsonProfesor = new StringContent(JsonConvert.SerializeObject(profesores), Encoding.UTF8, "application/json");


                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "RigistroProfesor";
                HttpResponseMessage httpResponse = await client.PostAsync(apiCrud, jsonProfesor);
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "¡Datos guardados correctamente!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("RegistroProfesor", "Register");
                }
                else
                {
                    TempData["AlertMessage"] = "¡Datos no guardados!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("RegistroProfesor", "Register");
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> InsertAsignatura(string codigo, string nombre)
        {
            try
            {
                Asignaturas asignatura = new Asignaturas();

                asignatura.Codigo = Convert.ToInt32(codigo);
                asignatura.Nombre = nombre;

                var jsonAsignatura = new StringContent(JsonConvert.SerializeObject(asignatura), Encoding.UTF8, "application/json");


                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "RigistroAsignatura";
                HttpResponseMessage httpResponse = await client.PostAsync(apiCrud, jsonAsignatura);
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "¡Datos guardados correctamente!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("RegistroAsignatura", "Register");
                }
                else
                {
                    TempData["AlertMessage"] = "¡Datos no guardados!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("RegistroAsignatura", "Register");
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> InsertClase(string Id_Alumno, string Id_Profesor)
        {
            try
            {
                Clases clases = new Clases();
                clases.Id_Alumno = Id_Alumno;
                clases.Id_Profesor = Id_Profesor;

                var jsonClases = new StringContent(JsonConvert.SerializeObject(clases), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "RegistroClases";
                HttpResponseMessage httpResponse = await client.PostAsync(apiCrud, jsonClases);
                if (httpResponse.IsSuccessStatusCode)
                {
                    TempData["AlertMessage"] = "¡Datos guardados correctamente!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("RegistrarClase", "Register");
                }
                else
                {
                    TempData["AlertMessage"] = "¡Datos no guardados!";
                    TempData["AlertType"] = "success";
                    return RedirectToAction("RegistrarClase", "Register");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public async Task<dynamic> Asignaturas(string controller)
        {
            try
            {
                List<Asignaturas> opciones = new List<Asignaturas>();
                var dataAsinatura = await new HomeController().ObtenerAsignauraAsync();
                foreach (var item in dataAsinatura)
                {
                    opciones.Add(item);
                }
                ViewBag.Opciones = opciones;
                if (controller.Equals("A", StringComparison.OrdinalIgnoreCase))
                {
                    return View("RegistroAlumno");
                }
                else
                {
                    return View("RegistroProfesor");
                }                
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }
    }
}
