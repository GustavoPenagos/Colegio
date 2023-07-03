using Antlr.Runtime;
using Colegio.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Colegio.Controllers
{
    public class RegisterController : Controller
    {
       public async Task<dynamic> RegistroAlumno(string alumno = null)
        {
            try
            {
                if (alumno == null)
                {
                    await Asignaturas("A");
                    return View();
                }

                List<Alumno> json = JsonConvert.DeserializeObject<List<Alumno>>(alumno);
                string asignatura = await Asignaturas("X");
                List<Asignatura> jsonAsg = JsonConvert.DeserializeObject<List<Asignatura>>(asignatura);

                //
                List<Alumno> alumnos = new List<Alumno>();
                List<Asignatura> asignaturas = new List<Asignatura>();
                var lisAsg = await Asignaturas("U");
                List<Asignatura> asgLis = JsonConvert.DeserializeObject<List<Asignatura>>(lisAsg);
                foreach (var alm in json)
                {
                    foreach (var item in jsonAsg)
                    {
                        if (alm.Asignatura == null)
                        {
                            alm.Asignatura = item.Nombre;
                        }
                        if (item.Codigo.Equals(alm.Asignatura))
                        {
                            asignaturas.Add(item);
                        }
                    }
                    alumnos.Add(alm);
                }
                //
                ViewBag.Alumno = alumnos;
                ViewBag.Datos = asgLis;
                ViewBag.Opciones = asignaturas;

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

        public async Task<dynamic> InsertAlumno(string identificacion, string nombre, string apellido, string edad, string direccion, string telefono, string asignatura, string calificacion = null)
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
                alumno.Asignatura= asignatura;
                alumno.Calificacion = calificacion == null ? "" : calificacion;


                if(await ValidarAlumno(alumno))
                {
                    var jsonAlumno = new StringContent(JsonConvert.SerializeObject(alumno), Encoding.UTF8, "application/json");

                    HttpClient client = new HttpClient();
                    string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/registro/alumnos";
                    HttpResponseMessage httpResponse = await client.PostAsync(apiCrud, jsonAlumno);
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        TempData["AlertMessage"] = "¡Datos guardados correctamente!";
                        TempData["AlertType"] = "success";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "¡Datos no guardados!";
                        TempData["AlertType"] = "error";
                    }
                }
                return RedirectToAction("RegistroAlumno", "Register");
            }
            catch(Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> InsertProfesor(string identificacion, string nombre, string apellido, string edad, string direccion, string telefono, string asignatura)
        {
            try
            {
                Profesores profesores = new Profesores();
                profesores.Id_Profesor = identificacion;
                profesores.Nombre = nombre;
                profesores.Apellido = apellido;
                profesores.Edad = edad;
                profesores.Direccion = direccion;
                profesores.Telefono = telefono;
                profesores.Asignatura = asignatura;

                var jsonProfesor = new StringContent(JsonConvert.SerializeObject(profesores), Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/registro/profesores";
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
                Asignatura asignatura = new Asignatura();

                asignatura.Codigo = codigo;
                asignatura.Nombre = nombre;

                var jsonAsignatura = new StringContent(JsonConvert.SerializeObject(asignatura), Encoding.UTF8, "application/json");


                HttpClient client = new HttpClient();
                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/registro/asignaturas";
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

        #region Obtener datos de tablas
        /// <summary>
        /// Metodo para obtener datos
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>

        public async Task<dynamic> Asignaturas(string controller)
        {
            try
            {
                var dataAsinatura = await new HomeController().ObtenerAsignauraAsync();

                switch (controller)
                {
                    case "A":
                        ViewBag.Opciones = dataAsinatura;
                        return View("RegistroAlumno");
                    case "U":
                        return JsonConvert.SerializeObject(dataAsinatura);
                    case "X":
                        var jsonAsg = JsonConvert.SerializeObject(dataAsinatura);
                        return jsonAsg;
                    default:
                        return RedirectToAction("RegistroProfesor", "Register");
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }


        public async Task<dynamic> ValidarAlumno(Alumno alumno)
        {
            try
            {
                var json = JsonConvert.SerializeObject(await new ListsController().Buscar(alumno.Id_Alumno, alumno.Asignatura, "Alumno", "registro"));
                List<Alumno> dJson = JsonConvert.DeserializeObject<List<Alumno>>(json);
                //

                //
                if (dJson != null)
                {
                    foreach (var item in dJson)
                    {
                        if ((alumno.Id_Alumno.Equals(item.Id_Alumno)) && (alumno.Asignatura.Equals(item.Asignatura)))
                        {
                            return false;
                        }
                        
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        #endregion
    }
}
