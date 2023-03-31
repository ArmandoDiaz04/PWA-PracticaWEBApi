using System.ComponentModel.DataAnnotations;

namespace PWA_WEBApi.Models
{
    public class Marca
    {

        [Key]
        public int id_marcas { get; set; }

        public string? nombre_marca { get; set; }

        public string? estados { get; set; }
    }
}
