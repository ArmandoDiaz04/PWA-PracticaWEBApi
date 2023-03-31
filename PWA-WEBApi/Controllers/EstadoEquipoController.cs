using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA_WEBApi.Data;
using PWA_WEBApi.Models;

namespace PWA_WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoEquipoController : ControllerBase
    {
        private readonly EquipoContext _equipoContext;

        public EstadoEquipoController(EquipoContext equipoContext)
        {
            _equipoContext = equipoContext;
        }

        #region GET_ALL - GET
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            List<EstadosEquipos> listadoEstadoEquipos = _equipoContext.estados_equipo.Where(x => x.estado == "A").ToList();

            if (listadoEstadoEquipos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoEstadoEquipos);
        }
        #endregion

        #region AGREGAR - POST
        [HttpPost]
        [Route("Add")]
        public IActionResult Crear([FromBody] EstadosEquipos estadoEquipo)
        {

            try
            {
                _equipoContext.estados_equipo.Add(estadoEquipo);
                _equipoContext.SaveChanges();

                return Ok(estadoEquipo);

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
        public IActionResult actualizar(int id, [FromBody] EstadosEquipos estadosEquipos)
        {
            EstadosEquipos? estadosEquipoExistente = _equipoContext.estados_equipo.Find(id);

            if (estadosEquipoExistente == null)
            {
                return NotFound();
            }

            estadosEquipoExistente.descripcion = estadosEquipos.descripcion;
            estadosEquipoExistente.estado = estadosEquipos.estado;

            _equipoContext.Entry(estadosEquipoExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(estadosEquipoExistente);

        }
        #endregion

        #region ELIMINAR - DELETE 
        [HttpDelete]
        [Route("detelet/{id}")]
        public IActionResult EliminarTipoEquipo(int id)
        {
            EstadosEquipos? estadoEquipoExistente = _equipoContext.estados_equipo.Find(id);

            if (estadoEquipoExistente == null) return NotFound();

            estadoEquipoExistente.estado = "C";

            _equipoContext.Entry(estadoEquipoExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(estadoEquipoExistente);

        }
        #endregion
    }
}
