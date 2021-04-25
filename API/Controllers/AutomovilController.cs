using API.Services;
using Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    /*Se especifica el APIController para la publicacion de servicios*/
    [ApiController]

    /*Se especifica la ruta Prefix del controller*/
    [Route("api/[controller]")]
    public class AutomovilController : ControllerBase
    {
        private readonly AutomovilService _service;

        public AutomovilController(AutomovilService service)
        {
            _service = service;
        }



        [HttpGet(Name = "IndexAutomovil")]
        public async Task<IActionResult> Read()
        {
            return Ok(await _service.GetAutomovilAsync());
        }



        [HttpGet("{placa}", Name = "DetailsAutomovil")]
        public async Task<IActionResult> Details(string? placa)
        {
            var auto = await _service.DetailsAutomovilAsync(placa);

            if (auto == null)
            {
                return NotFound();
            }
            return Ok(auto);
        }



        [HttpPost(Name = "CreateAutomovil")]
        public async Task<IActionResult> CreateAutomovil(Automovil auto)
        {
            return Ok(await _service.CreateAutomovilAsync(auto));
        }



        [HttpPut("{placa}", Name = "EditAutomovil")]
        public async Task<IActionResult> Edit(string placa, Automovil auto)
        {
            await _service.EditAutomovilAsync(placa, auto);
            return CreatedAtRoute("DetallarAutomovil", new { placa = auto.Placa }, auto);
        }



        [HttpDelete("{placa}", Name = "Deleteutomovil")]
        public async Task<IActionResult> DeleteConfirmed(string placa)
        {
            return Ok(await _service.DeleteAutomvilAsync(placa));
        }

    }
}
