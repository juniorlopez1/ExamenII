using Datos;
using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IAutomovilService
    {
        Task<Automovil> CreateAutomovilAsync(Automovil automovil);
        Task<List<Automovil>> GetAutomovilAsync();
        Task<Automovil> DetailsAutomovilAsync(string placa);
        Task<Automovil> EditAutomovilAsync(string placa, Automovil automovil);
        Task<Automovil> DeleteAutomvilAsync(string placa);
    }

    public class AutomovilService : IAutomovilService
    {
        private readonly Context _context;

        public AutomovilService(Context context)
        {
            _context = context;
        }

        public async Task<Automovil> CreateAutomovilAsync(Automovil automovil)
        {
            /* https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio#update-the-posttodoitem-create-method */

            _context.AutomovilContext.Add(automovil);
            await _context.SaveChangesAsync();
            return automovil;
        }

        public async Task<List<Automovil>> GetAutomovilAsync()
        {
            /* https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio#prevent-over-posting */
            return await _context.AutomovilContext.ToListAsync();
        }

        public async Task<Automovil> DetailsAutomovilAsync(string placa)
        {
            /* https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio#routing-and-url-paths */

            var auto = await _context.AutomovilContext.FindAsync(placa);

            if (auto == null)
            {
                return null;
            }

            return auto;
        }

        public async Task<Automovil> EditAutomovilAsync(string placa, Automovil automovil)
        {
            /* https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio#the-puttodoitem-method */

            if (placa != automovil.Placa)
            {
                return null;
            }

            _context.Entry(automovil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutomovilExists(placa))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return automovil;
        }

        public async Task<Automovil> DeleteAutomvilAsync(string placa)
        {
            /* https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio#prevent-over-posting */

            var auto = await _context.AutomovilContext.FindAsync(placa);
            if (auto == null)
            {
                return null;
            }

            _context.AutomovilContext.Remove(auto);
            await _context.SaveChangesAsync();

            return null;
        }

        /* Utilizado para U (Update - Edit) */
        private bool AutomovilExists(string placa) => _context.AutomovilContext.Any(e => e.Placa == placa);

    }
}
