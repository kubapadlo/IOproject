using System.ComponentModel.DataAnnotations;

namespace myapp.api.Models
{
    public class Parcel
    {
        [Key]
        public int ID { get; set; }
        public string Sender_Adress { get; set; }
        public string Receiver_Adress { get; set; }
        public bool Is_broken { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime SendingDate { get; set; }
    }
}

