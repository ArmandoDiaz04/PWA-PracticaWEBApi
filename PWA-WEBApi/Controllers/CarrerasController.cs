using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA_WEBApi.Data;
using PWA_WEBApi.Models;

namespace PWA_WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrerasController : ControllerBase
    {
        private readonly EquipoContext _equipoContext;

        public CarrerasController(EquipoContext equipoContext)
        {
            _equipoContext = equipoContext;
        }


        #region GET_ALL - GET
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            var listadoCarreras = (from carreara in _equipoContext.carreras 
                                   join facultad in _equipoContext.facultades on carreara.facultad_id equals facultad.facultad_id
                                   select new { 
                                        carreara.nombre_carrera,
                                        facultad.nombre_facultad
                                   }).ToList();

            if (listadoCarreras.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoCarreras);
        }

        #endregion

        #region AGREGAR - POST
        [HttpPost]
        [Route("Add")]
        public IActionResult crear([FromBody] Carreras carreras)
        {

            try
            {
                _equipoContext.carreras.Add(carreras);
                _equipoContext.SaveChanges();

                return Ok(carreras);

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
        public IActionResult actualizar(int id, [FromBody] Carreras carreras)
        {
            Carreras? carreraExistente = _equipoContext.carreras.Find(id);

            if (carreraExistente == null)
            {
                return NotFound();
            }

            carreraExistente.nombre_carrera = carreras.nombre_carrera;
            carreraExistente.facultad_id = carreras.facultad_id;

            _equipoContext.Entry(carreraExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(carreraExistente);

        }

        #endregion

        #region ELIMINAR - DELETE 
        [HttpDelete]
        [Route("detelet/{id}")]
        public IActionResult EliminarCarrera(int id)
        {
            Carreras? carreraExistente = _equipoContext.carreras.Find(id);

            if (carreraExistente == null) return NotFound();

            _equipoContext.Entry(carreraExistente).State = EntityState.Deleted;
            _equipoContext.SaveChanges();

            return Ok(carreraExistente);

        }
        #endregion
    }
}
