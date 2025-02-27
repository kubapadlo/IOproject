using myapp.api.Models;

namespace myapp.api.DTO
{   
    public class loginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class CreateUserRequest
    {
        public string Imię { get; set; }

        public string Nazwisko { get; set; }

        public string Adres { get; set; }

        public string NrTelefonu { get; set; }

        public string Login { get; set; }

        public string Hasło { get; set; }

        public int? IdUprawnienia { get; set; }
    }

    public class ParcelRequest
    {
        public bool? Gabaryt { get; set; }

        public string KodPocztowyNadawcy { get; set; }

        public string KodPocztowyOdbiorcy { get; set; }

        public string DaneOdbiorcy { get; set; }

        public string DaneNadawcy { get; set; }

        public string AdresNadawcy { get; set; }

        public string AdresOdbiorcy { get; set; }

        public string Status { get; set; }

        public bool? CzyZniszczona { get; set; }
    }

    public class LadunekDetailsDto
    {
        public int Id_Ladunku { get; set; }
        public int? IdSamochodu { get; set; }
        public string Status { get; set; }
       // public List<int> Pracownicy { get; set; }
        public List<int> Paczki { get; set; }
    }
}
