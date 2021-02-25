using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestLogging.Data.IRepositories;
using TestLogging.Domain;

namespace TestLogging.Data.Repositories
{
    public class PokemonRepositorio : IPokemonRepositorio
    {
        private readonly TestDbContext _context;

        public PokemonRepositorio(TestDbContext context)
        {
            _context = context;

        }

        public Task<List<Pokemon>> GetAll()
        {

            return _context.Pokemones.ToListAsync();


        }

        public async Task<int> Create(Pokemon Pokemon)
        {
            await _context.AddAsync(Pokemon);
            var resultado = await _context.SaveChangesAsync();
            return resultado;
        }

        public async Task<int> Delete(Pokemon Pokemon)
        {
            _context.Remove(Pokemon);
            var resultado = await _context.SaveChangesAsync();
            return resultado;
        }

        public async Task<Pokemon> GetById(string PokemonId)
        {
            return await _context.Pokemones.FindAsync(PokemonId);
        }

        public async Task<int> Update(Pokemon Pokemon)
        {
            _context.Update(Pokemon);
            var resultado = await _context.SaveChangesAsync();
            return resultado;
        }


        public async Task<bool> Exists(string id)
        {
            return await _context.Pokemones.AnyAsync(e => e.Id == id);
        }
    }
}
