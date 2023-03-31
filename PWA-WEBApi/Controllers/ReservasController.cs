using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA_WEBApi.Data;
using PWA_WEBApi.Models;

namespace PWA_WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {

        private readonly EquipoContext _equipoContext;

        public ReservasController(EquipoContext equipoContext)
        {
            _equipoContext = equipoContext;
        }

        #region GET_ALL - GET
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            var listadoReserva = (from reserva in _equipoContext.reservas 
                                  join equipo in _equipoContext.Equipos on reserva.equipo_id equals equipo.id_equipos
                                  join estado in _equipoContext.estados_reserva on reserva.reserva_id equals estado.estado_res_id
                                  join usuario in _equipoContext.usuarios on reserva.usuario_id equals usuario.usuario_id
                                  select new {
                                    nombreEquipo = equipo.nombre,
                                    nombreUsuario = usuario.nombre,
                                    reserva.fecha_salida,
                                    reserva.hora_salida,
                                    reserva.tiempo_reserva,
                                    estado.estado,
                                    reserva.fecha_retorno,
                                    reserva.hora_retorno
                                  }).ToList();

            if (listadoReserva.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoReserva);
        }

        #endregion

        #region AGREGAR - POST
        [HttpPost]
        [Route("Add")]
        public IActionResult crear([FromBody] Reserva _reserva)
        {

            try
            {
                _equipoContext.reservas.Add(_reserva);
                _equipoContext.SaveChanges();

                return Ok(_reserva);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region ACTUALIZAR - POST

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult actualizar(int id, [FromBody] Reserva reserva)
        {
            Reserva? reservaExistente = _equipoContext.reservas.Find(id);

            if (reservaExistente == null)
            {
                return NotFound();
            }

            reservaExistente.equipo_id = reserva.equipo_id;
            reservaExistente.usuario_id = reserva.usuario_id;
            reservaExistente.fecha_salida = reserva.fecha_salida;
            reservaExistente.hora_salida = reserva.hora_salida;
            reservaExistente.tiempo_reserva = reserva.tiempo_reserva;
            reservaExistente.estado_reserva_id = reserva.estado_reserva_id;
            reservaExistente.fecha_retorno = reserva.fecha_retorno;
            reservaExistente.hora_retorno = reserva.hora_retorno;
            

            _equipoContext.Entry(reservaExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(reservaExistente);

        }

        #endregion

        #region ELIMINAR - DELETE 
        [HttpDelete]
        [Route("detelet/{id}")]
        public IActionResult EliminarMarca(int id)
        {
            Reserva? reservaExistente = _equipoContext.reservas.Find(id);

            if (reservaExistente == null) return NotFound();

            _equipoContext.Entry(reservaExistente).State = EntityState.Deleted;
            _equipoContext.SaveChanges();

            return Ok(reservaExistente);

        }
        #endregion

    }
}
