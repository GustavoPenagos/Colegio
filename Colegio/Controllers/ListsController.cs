using Colegio.Models;
using Colegio.Models.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Colegio.Controllers
{
    public class ListsController : Controller
    {
        #region Vistas de las listas
        /// <summary>
        /// Retirno de datos a la vista
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ListaAlumnos()
        {
            try
            {
                var dataAlumno = await ObtenerAlumnoAsync();
                
                return View(dataAlumno);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<dynamic> ListaProfesores()
        {
            try
            {
                var dataProfesor = await ObtenerProfesorAsync();
                return View(dataProfesor);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<ActionResult> ListaAsignatura()
        {
            try
            {
                var dataAsignatura = await ObtenerAsignaturaAsync();
                return View(dataAsignatura);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public async Task<ActionResult> listarClase()
        {
            try
            {
                var dataClases = await ObtenerClases();

                return View(dataClases);

            }catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }
        #endregion

        #region Operadores de las listas
        /// <summary>
        /// Obtener la data de las diretentes tablas
        /// </summary>
        /// <returns></returns>

        public async Task<dynamic> ObtenerAlumnoAsync()
        {
            List<Alumnos> alumnosList = new List<Alumnos>();
            try
            {

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "ListaAlumnos";
                HttpResponseMessage ResponseAlumno = await client.GetAsync(apiCrud);
                if (ResponseAlumno.IsSuccessStatusCode)
                {
                    string response = await ResponseAlumno.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);
                    
                    foreach (DataRow row in data.Rows)
                    {
                        Alumnos alumno = new Alumnos();
                        alumno.Identificacion = row["Identificacion"].ToString();
                        alumno.Nombre = row["Nombre"].ToString();
                        alumno.Apellido = row["Apellido"].ToString();
                        alumno.Edad = Convert.ToInt32(row["Edad"].ToString());
                        alumno.Direccion = row["Direccion"].ToString();
                        alumno.Telefono = row["Telefono"].ToString();
                        
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

        public async Task<dynamic> ObtenerProfesorAsync()
        {
            try
            {
                List<View_Profesores> profesorList = new List<View_Profesores>();

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "ListaProfesores";
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach (DataRow row in data.Rows)
                    {
                        View_Profesores profesor = new View_Profesores();
                        profesor.Identificacion = row["Identificacion"].ToString();
                        profesor.Nombre = row["Nombre"].ToString();
                        profesor.Edad = Convert.ToInt16(row["Edad"].ToString());
                        profesor.Direccion = row["Direccion"].ToString();
                        profesor.Telefono = row["Telefono"].ToString();
                        profesor.Asignatura = row["Asignatura"].ToString();

                        profesorList.Add(profesor);
                    }
                    
                    return profesorList;
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

        public async Task<dynamic> ObtenerAsignaturaAsync()
        {
            try
            {
                List<Asignaturas> AsignaturaList = new List<Asignaturas>();

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

                        AsignaturaList.Add(asignatura);
                    }
                    
                    return AsignaturaList;
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

        public async Task<dynamic> ObtenerClases()
        {
            try
            {
                List<View_Clases> clasesList = new List<View_Clases>();

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "ListaClases";
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach (DataRow row in data.Rows)
                    {
                        View_Clases clases = new View_Clases();
                        clases.Id_Alumno = row["Id_Alumno"].ToString();
                        clases.Nombre_Alumno = row["NombreAlumno"].ToString();
                        clases.Id_Profesor = row["Id_Profesor"].ToString();
                        clases.Nombre_Profesor = row["NombreProfesor"].ToString();
                        clases.Asignatura = row["Asignatura"].ToString();

                        clasesList.Add(clases);
                    }

                    return clasesList;
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

        #endregion
    }
}
