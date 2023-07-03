using Colegio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Colegio.Controllers
{
    public class SearchController : Controller
    {
        public async Task<dynamic> Actualizar()
        {
            var dataAsinatura = await new RegisterController().Asignaturas("U");
            ViewBag.Opciones = dataAsinatura;
            return View(dataAsinatura);
        }
    }
}
