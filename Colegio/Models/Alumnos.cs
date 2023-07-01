using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegio.Models
{
    public class Alumnos
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Cod_Asignatura { get; set; }
        public string calificacion { get; set; }
    }
}