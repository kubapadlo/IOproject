using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocztaDesktop.EndPoint
{
    //pomocniczna klasa ktorej instacji zostana przyporzadkowane dane z bazy
    public class LoginRequest
    {
        public string Username { get; set; }   
        public string Password { get; set; }
    }

}
