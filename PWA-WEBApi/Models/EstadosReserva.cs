using System.ComponentModel.DataAnnotations;

namespace PWA_WEBApi.Models
{
    public class EstadosReserva
    {

        [Key]
        public int estado_res_id { get; set; }

        public string? estado { get; set; }

    }
}
