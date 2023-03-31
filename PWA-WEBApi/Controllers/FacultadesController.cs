using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA_WEBApi.Data;
using PWA_WEBApi.Models;

namespace PWA_WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultadesController : ControllerBase
    {
        private readonly EquipoContext _equipoContext;

        public FacultadesController(EquipoContext equipoContext)
        {
            _equipoContext = equipoContext;
        }

        #region GET_ALL - GET
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            List<Facultades> listadoFacultades = _equipoContext.facultades.ToList();

            if (listadoFacultades.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoFacultades);
        }

        #endregion

        #region AGREGAR - POST
        [HttpPost]
        [Route("Add")]
        public IActionResult crear([FromBody] Facultades facultad)
        {

            try
            {
                _equipoContext.facultades.Add(facultad);
                _equipoContext.SaveChanges();

                return Ok(facultad);

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
        public IActionResult actualizar(int id, [FromBody] Facultades facultad)
        {
            Facultades? facultadExistente = _equipoContext.facultades.Find(id);

            if (facultadExistente == null)
            {
                return NotFound();
            }

            facultadExistente.nombre_facultad = facultad.nombre_facultad;

            _equipoContext.Entry(facultadExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(facultadExistente);

        }

        #endregion

        #region ELIMINAR - DELETE 
        [HttpDelete]
        [Route("detelet/{id}")]
        public IActionResult EliminarMarca(int id)
        {
            Facultades? facultadExistente = _equipoContext.facultades.Find(id);

            if (facultadExistente == null) return NotFound();

            _equipoContext.Entry(facultadExistente).State = EntityState.Deleted;
            _equipoContext.SaveChanges();

            return Ok(facultadExistente);

        }
        #endregion

    }
}
