using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocztaDesktop.Model
{
    public class Ladunek
    {
        public int IdLadunek { get; set; }

        public int? IdSamochod { get; set; }

        public string Status { get; set; }
        public int LiczbaPaczek { get; set; } 
        //public virtual Samochody IdSamochodNavigation { get; set; }

    }
}
