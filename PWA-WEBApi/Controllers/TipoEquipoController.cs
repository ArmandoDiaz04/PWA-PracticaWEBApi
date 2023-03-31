using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA_WEBApi.Data;
using PWA_WEBApi.Models;

namespace PWA_WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEquipoController : ControllerBase
    {

        private readonly EquipoContext _equipoContext;

        public TipoEquipoController(EquipoContext equipoContext)
        {
            _equipoContext = equipoContext;
        }

        #region GET_ALL - GET
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            List<TipoEquipos> listadoTipoEquipo = _equipoContext.tipo_equipo.Where(x => x.estado == "A").ToList();

            if (listadoTipoEquipo.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoTipoEquipo);
        }
        #endregion

        #region AGREGAR - POST
        [HttpPost]
        [Route("Add")]
        public IActionResult Crear([FromBody] TipoEquipos tipoEquipo)
        {

            try
            {
                _equipoContext.tipo_equipo.Add(tipoEquipo);
                _equipoContext.SaveChanges();

                return Ok(tipoEquipo);

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
        public IActionResult actualizar(int id, [FromBody] TipoEquipos tipoEquipo)
        {
            TipoEquipos? tipoEquipoExistente = _equipoContext.tipo_equipo.Find(id);

            if (tipoEquipoExistente == null)
            {
                return NotFound();
            }

            tipoEquipoExistente.descripcion = tipoEquipo.descripcion;
            tipoEquipoExistente.estado = tipoEquipo.estado;

            _equipoContext.Entry(tipoEquipoExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(tipoEquipoExistente);

        }

        #endregion

        #region ELIMINAR - DELETE 
        [HttpDelete]
        [Route("detelet/{id}")]
        public IActionResult EliminarTipoEquipo(int id)
        {
            TipoEquipos? tipoEquipoExistente = _equipoContext.tipo_equipo.Find(id);

            if (tipoEquipoExistente == null) return NotFound();

            tipoEquipoExistente.estado = "C";

            _equipoContext.Entry(tipoEquipoExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(tipoEquipoExistente);

        }
        #endregion
    }
}
