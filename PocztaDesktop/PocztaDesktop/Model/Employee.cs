using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocztaDesktop.Model
{
    public class Employee
    {
        public int IdPracownika { get; set; }

        public string Imię { get; set; }

        public string Nazwisko { get; set; }

        public string Adres { get; set; }

        public string NrTelefonu { get; set; }

        public string Login { get; set; }

        public string Hasło { get; set; }

        public int? IdUprawnienia { get; set; }
    }

}
