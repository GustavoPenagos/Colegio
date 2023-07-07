using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Colegio.Models
{
    public class Reporte
    {
        public string Id_Alumno { get; set; }
        public string Nombre_Alumno { get; set; }
        public string Codigo_Materia { get; set; }
        public string Nombre_Materia { get; set; }
        public string Id_Profesor { get; set; }
        public string Nombre_Profesor { get; set; }
        public double Calificaicon_Final{ get; set; }

    }
}