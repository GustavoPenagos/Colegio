using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegio.Models.Views
{
    public class View_Alumnos
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Asignatura { get; set; }
        public string Calificacion { get; set; }
    }
}