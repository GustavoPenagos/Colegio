using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegio.Models.XMLFormat
{
    public class XMLFormat
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
        public string Iva { get; set; }
    }
}