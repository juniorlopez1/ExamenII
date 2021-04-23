using Entidades;
using Microsoft.AspNetCore.Mvc;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    /* Este es el controller */

    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraController : ControllerBase
    {
        #region Property - Constructors

        private readonly BitacoraService _bitacoraService;

        public BitacoraController(BitacoraService bitacoraService)

        {
            _bitacoraService = bitacoraService;
        }
        #endregion

        #region CRUD
        [HttpGet]
        public ActionResult<List<Bitacora>> Get() =>
            _bitacoraService.Get();

        [HttpPost]
        public ActionResult<Bitacora> Create(Bitacora registro)
        {
            _bitacoraService.Create(registro);

            return CreatedAtRoute("GetBook", new { id = registro.Id.ToString() }, registro);
        }

        #endregion

        #region CRUD Unused
        //[HttpGet("{id:length(24)}", Name = "GetBook")]
        //public ActionResult<Bitacora> Get(string id)
        //{
        //    var book = _bitacoraService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}
        //[HttpPut("{id:length(24)}")]
        //public IActionResult Update(string id, Bitacora bookIn)
        //{
        //    var book = _bitacoraService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _bitacoraService.Update(id, bookIn);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public IActionResult Delete(string id)
        //{
        //    var book = _bitacoraService.Get(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _bitacoraService.Remove(book.Id);

        //    return NoContent();
        //}
        #endregion
    }
}
