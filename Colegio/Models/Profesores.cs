using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegio.Models
{
    public class Profesores
    {
        public string Id_Profesor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Asignatura { get; set; }
    }
}