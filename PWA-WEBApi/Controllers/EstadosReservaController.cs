using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA_WEBApi.Data;
using PWA_WEBApi.Models;

namespace PWA_WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosReservaController : ControllerBase
    {
        private readonly EquipoContext _equipoContext;

        public EstadosReservaController(EquipoContext equipoContext)
        {
            _equipoContext = equipoContext;
        }

        #region GET_ALL - GET
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            List<EstadosReserva> listadoEstadoReserva = _equipoContext.estados_reserva.ToList();

            if (listadoEstadoReserva.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEstadoReserva);
        }

        #endregion

        #region AGREGAR - POST
        [HttpPost]
        [Route("Add")]
        public IActionResult crear([FromBody] EstadosReserva estadosReserva)
        {

            try
            {
                _equipoContext.estados_reserva.Add(estadosReserva);
                _equipoContext.SaveChanges();

                return Ok(estadosReserva);

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
        public IActionResult actualizar(int id, [FromBody] EstadosReserva estadosReserva)
        {
            EstadosReserva? estadoReservaExistente = _equipoContext.estados_reserva.Find(id);

            if (estadoReservaExistente == null)
            {
                return NotFound();
            }

            estadoReservaExistente.estado = estadosReserva.estado;

            _equipoContext.Entry(estadoReservaExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(estadoReservaExistente);

        }

        #endregion

        #region ELIMINAR - DELETE 
        [HttpDelete]
        [Route("detelet/{id}")]
        public IActionResult EliminarMarca(int id)
        {
            EstadosReserva? estadoReservaExistente = _equipoContext.estados_reserva.Find(id);

            if (estadoReservaExistente == null) return NotFound();


            _equipoContext.Entry(estadoReservaExistente).State = EntityState.Deleted;
            _equipoContext.SaveChanges();

            return Ok(estadoReservaExistente);

        }
        #endregion
    }
}
