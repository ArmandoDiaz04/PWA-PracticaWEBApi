using System.ComponentModel.DataAnnotations;

namespace PWA_WEBApi.Models
{
    public class Facultades
    {

        [Key]
        public int facultad_id { get; set; }

        public string? nombre_facultad { get; set; }

    }
}
