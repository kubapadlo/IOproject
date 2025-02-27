using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocztaDesktop.Model
{
    public class LadunekDetailsDto
    {
        public int Id_Ladunku { get; set; }
        public int IdSamochodu { get; set; }
        public string Status { get; set; }
        public int LiczbaPaczek { get; set; }

    }
}
