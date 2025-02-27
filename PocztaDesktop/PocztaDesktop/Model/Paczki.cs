using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocztaDesktop.Model
{
    public class Paczki
    {
        public int IdPaczki { get; set; }
        public bool? Gabaryt { get; set; }
        public string KodPocztowyNadawcy { get; set; }
        public string KodPocztowyOdbiorcy { get; set; }
        public string DaneOdbiorcy { get; set; }
        public string DaneNadawcy { get; set; }
        public string AdresNadawcy { get; set; }
        public string AdresOdbiorcy { get; set; }
        public DateTime? DataNadania { get; set; }
        public string Status { get; set; }
        public byte[] SsmaTimeStamp { get; set; }
        public bool? CzyZniszczona { get; set; }
    }

}
