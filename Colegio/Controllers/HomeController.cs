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

namespace Colegio.Controllers
{
    public class HomeController : Controller
    {
        public async Task<dynamic> Index()
        {
            try
            {
                List<ListaFinal> listadoFinal = new List<ListaFinal>();

                var dataAlumno = await ObtenerAlumnoAsync();
                var dataAsignaura = await ObtenerAsignauraAsync();
                var dataProfesor = await ObtenerProfesorAsync();

                //var dataFinal = await ObtenerFinalAsync();


                return View(dataAlumno);
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

        public async Task<dynamic> ObtenerAlumnoAsync()
        {
            List<Alumnos> alumnosList = new List<Alumnos>();
            try
            {

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAlumnos"];
                HttpResponseMessage ResponseAlumno = await client.GetAsync(apiCrud);
                if (ResponseAlumno.IsSuccessStatusCode)
                {
                    string response = await ResponseAlumno.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach(DataRow row in data.Rows)
                    {
                        Alumnos alumno = new Alumnos();
                        alumno.Identificacion = row["Identificacion"].ToString();
                        alumno.Nombre = row["Nombre"].ToString();
                        alumno.Apellido = row["Apellido"].ToString();
                        alumno.Edad = Convert.ToInt32(row["Edad"].ToString());
                        alumno.Direccion = row["Direccion"].ToString();
                        alumno.Telefono = row["Telefono"].ToString();
                        alumno.Cod_Asignatura = Convert.ToInt32(row["Cod_Asignatura"].ToString());
                        alumno.calificacion = row["calificacion"].ToString();
                        alumnosList.Add(alumno);
                    }
                    return alumnosList;
                }
                else
                {
                    return alumnosList;
                }
            }
            catch (InvalidOperationException ex)
            {

                return alumnosList = null;
            }
        }

        public async Task<dynamic> ObtenerAsignauraAsync()
        {
            try
            {
                List<Asignaturas> asignaturasList = new List<Asignaturas>();

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAsignaturas"];
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach(DataRow row in data.Rows)
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

        public async Task<dynamic> ObtenerProfesorAsync()
        {
            try
            {
                List<Profesores> profesorList = new List<Profesores>();

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlProfosores"];
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach(DataRow row in data.Rows)
                    {
                        Profesores profesor = new Profesores();
                        profesor.Identificacion = row["Identificacion"].ToString();
                        profesor.Nombre = row["Nombre"].ToString();
                        profesor.Apellido = row["Apellido"].ToString();
                        profesor.Edad = Convert.ToInt16(row["Edad"].ToString());
                        profesor.Direccion = row["Direccion"].ToString();
                        profesor.Telefono = row["Telefono"].ToString();
                        profesor.Cod_Asignatura = Convert.ToInt32(row["Cod_Asignatura"].ToString());

                        profesorList.Add(profesor);
                    }
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        
                    }
                    return profesorList;
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

        public async Task<dynamic> ObtenerFinalAsync()
        {
            try
            {
                List<ListaFinal> ListaFinal = new List<ListaFinal>();

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlFinal"];
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);
                    foreach(DataRow row in data.Rows)
                    {
                        ListaFinal lista = new ListaFinal();
                        lista.IdentificacionAlumno = ";";
                        lista.NombreAlumno = ";";
                        lista.CodigoAignatura = ";";
                        lista.NombreAsignatura = ";";
                        lista.IdentificacionProfesor = ";";
                        lista.NombreProfesor = ";";
                        lista.CalificacionFinal = ";";
                        if(Convert.ToDouble(lista.CalificacionFinal) > 3.0 && Convert.ToDouble(lista.CalificacionFinal) < 5.0)
                        {
                            lista.Estado = "Si";
                        }
                        else
                        {
                            lista.Estado = "No";
                        }

                        ListaFinal.Add(lista);
                    }

                    return ListaFinal;
                }
                else
                {
                    return View("Error");
                }
            }catch(Exception ex)
            {
                return View("Error");
            }
}