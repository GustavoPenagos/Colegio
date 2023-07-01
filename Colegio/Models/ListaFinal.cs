using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegio.Models
{
    public class ListaFinal
    {
        public string IdentificacionAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string CodigoAignatura { get; set; }
        public string NombreAsignatura { get; set; }
        public string IdentificacionProfesor { get; set; }
        public string NombreProfesor { get; set; }
        public string CalificacionFinal { get; set; }
        public string Estado { get; set; }
    }
}