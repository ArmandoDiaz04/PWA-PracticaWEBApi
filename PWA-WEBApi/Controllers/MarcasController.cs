using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA_WEBApi.Data;
using PWA_WEBApi.Models;

namespace PWA_WEBApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {

        private readonly EquipoContext _equipoContext;

        public MarcasController(EquipoContext equipoContext)
        {
            _equipoContext = equipoContext;
        }

        #region GET_ALL - GET
        [HttpGet]
        [Route("GetAll")]
        public ActionResult Get()
        {
            List<Marca> listadoMarcas = _equipoContext.Marcas.Where(x => x.estados == "A").ToList();

            if (listadoMarcas.Count == 0)
            {
                return NotFound();
            }

            return Ok(listadoMarcas);
        }

        #endregion

        #region AGREGAR - POST
        [HttpPost]
        [Route("Add")]
        public IActionResult crear([FromBody] Marca marca)
        {

            try
            {
                _equipoContext.Marcas.Add(marca);
                _equipoContext.SaveChanges();

                return Ok(marca);

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
        public IActionResult actualizar(int id, [FromBody] Marca marca)
        {
            Marca? marcaExistente = _equipoContext.Marcas.Find(id);

            if (marcaExistente == null)
            {
                return NotFound();
            }

            marcaExistente.nombre_marca = marca.nombre_marca;
            marcaExistente.estados = marca.estados;

            _equipoContext.Entry(marcaExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(marcaExistente);

        }

        #endregion

        #region ELIMINAR - DELETE 
        [HttpDelete]
        [Route("detelet/{id}")]
        public IActionResult EliminarMarca(int id)
        {
            Marca? marcaExistente = _equipoContext.Marcas.Find(id);

            if (marcaExistente == null) return NotFound();

            marcaExistente.estados = "C";

            _equipoContext.Entry(marcaExistente).State = EntityState.Modified;
            _equipoContext.SaveChanges();

            return Ok(marcaExistente);

        }
        #endregion
    }
}
