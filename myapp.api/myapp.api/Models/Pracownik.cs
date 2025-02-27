using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace myapp.api.Models
{
    public class Pracownik
    {
        [Key]
        [Column("Id_Pracownika")]
        public int IdPracownika { get; set; }

        [Column("Imię")]
        [MaxLength(50)]
        public string Imie { get; set; }

        [Column("Nazwisko")]
        [MaxLength(50)]
        public string Nazwisko { get; set; }

        [Column("Adres")]
        [MaxLength(100)]
        public string Adres { get; set; }

        [Column("Nr_telefonu")]
        [MaxLength(15)]
        public string NrTelefonu { get; set; }

        [Column("Login")]
        [MaxLength(50)]
        public string Login { get; set; }

        [Column("Hasło")]
        [MaxLength(50)]
        public string Haslo { get; set; }

        [Column("Id_Uprawnienia")]
        public int IdUprawnienia { get; set; }
    }

}
