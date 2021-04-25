using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelsNotUse;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class AutomovilController : Controller
    {
        /* context de sql */
        private readonly _Context _context;
        /* servicio de mongo */
        private readonly IBitacoraWebService _service;

        public AutomovilController(_Context context, IBitacoraWebService service)
        {
            _context = context;
            _service = service;
        }

        #region CRUD

        /* ------------------------------------------------------- CREATE */
        // GET: Automovils/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Automovils/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Placa,Modelo,Marca,Capacidad,TipoMarcha")] AutomovilViewModel automovil)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Add(automovil);
            await _context.SaveChangesAsync();


            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                Placa = automovil.Placa,
                Marca = automovil.Marca,
                Capacidad = automovil.Capacidad,
                TipoMarcha = automovil.TipoMarcha,
                /* sobreescribe el metodo ToString en el view model Entidad */
                Detalle = automovil.ToString(),
                Accion = "Create Automovil"

            };
            await _service.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */

            return View(automovil);
        }



        /* ------------------------------------------------------- READ */
        // GET: Automovils
        public async Task<IActionResult> Index()
        {
            return View(await _context.AutomovilContext.ToListAsync());
        }

        // GET: Automovils/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automovil = await _context.AutomovilContext
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (automovil == null)
            {
                return NotFound();
            }

            return View(automovil);
        }



        /* ------------------------------------------------------- UPDATE */
        // GET: Automovils/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automovil = await _context.AutomovilContext.FindAsync(id);
            if (automovil == null)
            {
                return NotFound();
            }
            return View(automovil);
        }

        // PUT: Automovils/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Placa,Modelo,Marca,Capacidad,TipoMarcha")] AutomovilViewModel automovil)
        {
            if (id != automovil.Placa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(automovil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutomovilExists(automovil.Placa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                    /*  --------------------------- para mongodb registro bitacora */
                    var bitacora = new BitacoraViewModel()
                    {
                        Placa = automovil.Placa,
                        Marca = automovil.Marca,
                        Capacidad = automovil.Capacidad,
                        TipoMarcha = automovil.TipoMarcha,
                        /* sobreescribe el metodo ToString en el view model Entidad */
                        Detalle = automovil.ToString(),
                        Accion = "Update Automovil"

                    };
                    await _service.Create(bitacora);
                    /*  --------------------------- para mongodb registro bitacora */
                }
                return RedirectToAction(nameof(Index));
            }
            return View(automovil);
        }


        /* ------------------------------------------------------- DELETE */
        // DELETE: Automovils/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automovil = await _context.AutomovilContext
                .FirstOrDefaultAsync(m => m.Placa == id);
            if (automovil == null)
            {
                return NotFound();
            }

            /*  --------------------------- para mongodb registro bitacora */
            var bitacora = new BitacoraViewModel()
            {
                Placa = automovil.Placa,
                Marca = automovil.Marca,
                Capacidad = automovil.Capacidad,
                TipoMarcha = automovil.TipoMarcha,
                /* sobreescribe el metodo ToString en el view model Entidad */
                Detalle = automovil.ToString(),
                Accion = "Delete Automovil"

            };
            await _service.Create(bitacora);
            /*  --------------------------- para mongodb registro bitacora */

            return View(automovil);
        }

        // DELETE: Automovils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var automovil = await _context.AutomovilContext.FindAsync(id);
            _context.AutomovilContext.Remove(automovil);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private bool AutomovilExists(string id)
        {
            return _context.AutomovilContext.Any(e => e.Placa == id);
        }
    }
}
