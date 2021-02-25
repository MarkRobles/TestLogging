using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestLogging.Domain;

namespace TestLogging.Data.IRepositories
{
   public interface IPokemonRepositorio
    {
        Task<int> Create(Pokemon Pokemon);
        Task<List<Pokemon>> GetAll();
        Task<int> Update(Pokemon Pokemon);
        Task<int> Delete(Pokemon Pokemon);
        Task<Pokemon> GetById(string PokemonId);
        Task<bool> Exists(string id);
    }
}
