using Colegio.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
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
                List<Alumno> data = (List<Alumno>)Session["Lista"];

                if (data != null)
                {
                    Session.Clear();
                    return View(data);
                }else
                {
                    var dataAlumno = await ObtenerAlumnoAsync();
                    return View(dataAlumno);
                }               
                
                
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
                List<Profesores> data = (List<Profesores>)Session["Lista"];

                if (data != null)
                {
                    Session.Clear();
                    return View(data);
                }
                else
                {
                    var dataProfesor = await ObtenerProfesorAsync();
                    return View(dataProfesor);
                }
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
                List<Asignatura> data = (List<Asignatura>)Session["Lista"];

                if (data != null)
                {
                    Session.Clear();
                    return View(data);
                }
                else
                {
                    var dataAsignatura = await ObtenerAsignaturaAsync();
                    return View(dataAsignatura);
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// Obtener la data de las diretentes tablas
        /// </summary>
        /// <returns></returns>
        /// 
        #region Operadores de las listas

        public async Task<dynamic> ObtenerAlumnoAsync()
        {
            List<Alumno> alumnosList = new List<Alumno>();
            try
            {

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/lista/alumnos";
                HttpResponseMessage ResponseAlumno = await client.GetAsync(apiCrud);
                if (ResponseAlumno.IsSuccessStatusCode)
                {
                    string response = await ResponseAlumno.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);
                    
                    foreach (DataRow row in data.Rows)
                    {
                        Alumno alumno = new Alumno();
                        alumno.Id = row["Id"].ToString();
                        alumno.Nombre = row["Nombre"].ToString();
                        alumno.Apellido = row["Apellido"].ToString();
                        alumno.Edad = row["Edad"].ToString();
                        alumno.Direccion = row["Direccion"].ToString();
                        alumno.Telefono = row["Telefono"].ToString();

                        //var materia = await new HomeController().ObtenerAsignauraAsync();
                        //foreach(var item in materia)
                        //{
                        //    if (item.Codigo.ToString().Equals(row["Asignatura"].ToString()))
                        //    {
                        //        alumno.Asignatura = item.Nombre;
                        //    }    
                        //}
                        alumno.Asignatura = row["Asignatura"].ToString();
                        alumno.Calificacion = row["Calificacion"].ToString();

                        alumnosList.Add(alumno);
                    }
                    return alumnosList;
                }
                else
                {
                    return View("Error");
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
                List<Profesores> profesorList = new List<Profesores>();

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/lista/profesores";
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach (DataRow row in data.Rows)
                    {
                        Profesores profesor = new Profesores();
                        profesor.Id = row["Id"].ToString();
                        profesor.Nombre = row["Nombre"].ToString();
                        profesor.Apellido = row["Apellido"].ToString();
                        profesor.Edad = row["Edad"].ToString();
                        profesor.Direccion = row["Direccion"].ToString();
                        profesor.Telefono = row["Telefono"].ToString();
                        profesor.Asignatura = row["Asignatura"].ToString();
                        //var materia = await new HomeController().ObtenerAsignauraAsync();
                        //foreach (var item in materia)
                        //{
                        //    if (item.Codigo.ToString().Equals(row["Asignatura"].ToString()))
                        //    {
                        //        profesor.Asignatura = item.Nombre;
                        //    }
                        //}

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
                List<Asignatura> AsignaturaList = new List<Asignatura>();

                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/lista/asignaturas";
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);

                    foreach (DataRow row in data.Rows)
                    {
                        Asignatura asignatura = new Asignatura();
                        asignatura.Id = row["Id"].ToString();
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

        public async Task<dynamic> Buscar(string buscar, string asg, string name, string accion=null)
        {
            try
            {
                List<Alumno> alumnosList = new List<Alumno>();
                List<Profesores> profesorList = new List<Profesores>();
                List<Asignatura> asignaturaList = new List<Asignatura>();

                HttpClient client = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/buscar" + "?Id=" + buscar + "&asg=" + asg + "&name=" + name;

                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    DataTable data = JsonConvert.DeserializeObject<DataTable>(response);
                    switch (data.Rows[0].ItemArray.Length)
                    {
                        case 2:
                            foreach (DataRow row in data.Rows)
                            {
                                Asignatura asignatura = new Asignatura();
                                asignatura.Id = row["Id"].ToString();
                                asignatura.Nombre = row["Nombre"].ToString();

                                asignaturaList.Add(asignatura);
                            }
                            if (accion != null)
                            {
                                return asignaturaList;
                            }
                            else
                            {
                                Session["Lista"] = asignaturaList;
                                return RedirectToAction("ListaAsignatura", "Lists");
                            }
                        case 7:
                            foreach (DataRow row in data.Rows)
                            {
                                Profesores profesor = new Profesores();
                                profesor.Id = row["Id"].ToString();
                                profesor.Nombre = row["Nombre"].ToString();
                                profesor.Apellido = row["Apellido"].ToString();
                                profesor.Edad = row["Edad"].ToString();
                                profesor.Direccion = row["Direccion"].ToString();
                                profesor.Telefono = row["Telefono"].ToString();
                                profesor.Asignatura = row["Asignatura"].ToString();

                                profesorList.Add(profesor);
                            }
                            if (accion != null)
                            {
                                return profesorList;
                            }
                            else
                            {
                                Session["Lista"] = profesorList;
                                return RedirectToAction("ListaProfesores", "Lists");
                            }
                        case 8:
                            foreach (DataRow row in data.Rows)
                            {
                                Alumno alumno = new Alumno();
                                alumno.Id = row["Id"].ToString();
                                alumno.Nombre = row["Nombre"].ToString();
                                alumno.Apellido = row["Apellido"].ToString();
                                alumno.Edad = row["Edad"].ToString();
                                alumno.Direccion = row["Direccion"].ToString();
                                alumno.Telefono = row["Telefono"].ToString();
                                alumno.Asignatura = row["Asignatura"].ToString();
                                alumno.Calificacion = row["Calificacion"].ToString();

                                alumnosList.Add(alumno);
                            }
                            if (accion != null)
                            {
                                return alumnosList;
                            }
                            else
                            {
                                Session["Lista"] = alumnosList;
                                return RedirectToAction("ListaAlumnos", "Lists");
                            }
                        
                        
                    }

                    //if (accion != null)
                    //{
                    //    switch (accion)
                    //    {
                    //        case "registro":
                    //            alumno.Asignatura = row["Asignatura"].ToString();
                    //            break;
                    //        case "Editar":
                    //            var materia = await new HomeController().ObtenerAsignauraAsync();
                    //            foreach (var item in materia)
                    //            {
                    //                if (item.Codigo.ToString().Equals(row["Asignatura"].ToString()))
                    //                {
                    //                    alumno.Asignatura = item.Codigo;
                    //                }
                    //            }
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                    //    var materia = await new HomeController().ObtenerAsignauraAsync();
                    //    foreach (var item in materia)
                    //    {
                    //        if (item.Codigo.ToString().Equals(row["Asignatura"].ToString()))
                    //        {
                    //            alumno.Asignatura = item.Nombre;
                    //        }
                    //    }
                    //}                      

                    //alumno.Calificacion = row["Calificacion"].ToString();

                }
                if(alumnosList != null)
                {
                    return RedirectToAction("ListaAlumnos", "Lists");
                }
                else
                {
                    return RedirectToAction("ListaProfesores", "Lists");
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
