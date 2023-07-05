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
                List<Profesor> data = (List<Profesor>)Session["Lista"];

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
            try
            {
                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/listar" + "?name=alumno";
                HttpResponseMessage ResponseAlumno = await client.GetAsync(apiCrud);
                if (ResponseAlumno.IsSuccessStatusCode)
                {
                    string response = await ResponseAlumno.Content.ReadAsStringAsync();
                    List<Alumno> data = JsonConvert.DeserializeObject<List<Alumno>>(response);

                    return data;
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<dynamic> ObtenerProfesorAsync()
        {
            try
            {
                HttpClient client = new HttpClient();

                string apiCrud = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/listar" + "?name=profesor";
                HttpResponseMessage ResponseAsignatura = await client.GetAsync(apiCrud);
                if (ResponseAsignatura.IsSuccessStatusCode)
                {
                    string response = await ResponseAsignatura.Content.ReadAsStringAsync();
                    List<Profesor> data = JsonConvert.DeserializeObject<List<Profesor>>(response);
                    
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

        public async Task<dynamic> ObtenerAsignaturaAsync()
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
                return null;
            }
        }

        public async Task<dynamic> Buscar(string buscar, string asg, string name, string accion=null)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"] + "api/buscar" + "?Id=" + buscar + "&asg=" + asg + "&name=" + name;

                HttpResponseMessage httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    switch (name)
                    {
                        case "alumno":
                            List<Alumno> dataAlm = JsonConvert.DeserializeObject<List<Alumno>>(response);
                            if (accion != null)
                            {
                                return dataAlm;
                            }
                            else
                            {
                                Session["Lista"] = dataAlm;
                                return RedirectToAction("ListaAlumnos", "Lists");
                            }
                        case "profesor":
                            List<Profesor> dataPrf = JsonConvert.DeserializeObject<List<Profesor>>(response);
                            if (accion != null)
                            {
                                return dataPrf;
                            }
                            else
                            {
                                Session["Lista"] = dataPrf;
                                return RedirectToAction("ListaProfesores", "Lists");
                            }
                        case "asignatura":
                            List<Asignatura> dataAsg = JsonConvert.DeserializeObject<List<Asignatura>>(response);
                            Session["Lista"] = dataAsg;
                            return RedirectToAction("ListaAsignatura", "Lists");
                        default: return RedirectToAction("ListaAlumnos", "Lists");
                    }
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
