using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestLogging.Data.IRepositories;
using TestLogging.Domain;

namespace TestLogging.Web.Controllers
{
    public class PokemonesController : Controller
    {
        private readonly IPokemonRepositorio _PokemonRepositorio;
        private readonly ILogger<Pokemon> _logger;

        public PokemonesController(IPokemonRepositorio PokemonRepositorio,ILogger<Pokemon> logger)
        {
            _PokemonRepositorio = PokemonRepositorio;
            _logger = logger;
        }

        // GET: Pokemon
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Ver lista de pokemones");
            var Pokemones = await _PokemonRepositorio.GetAll();
            return View(Pokemones);
        }

        // GET: Pokemon/Details/5
        public async Task<IActionResult> Details(string id)
        {

            _logger.LogInformation("Intentar ver detalles del pokemon");
            if (id == null)
            {
              
                return NotFound();
            }

            var Pokemon = await _PokemonRepositorio.GetById(id);
            if (Pokemon == null)
            {
                _logger.LogInformation("No se encontro el pokemon");
                return NotFound();
            }

            return View(Pokemon);
        }

        // GET: Pokemon/Create
 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pokemon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public async Task<IActionResult> Create([Bind("Id,Name")] Pokemon Pokemon)
        {

          
            if (ModelState.IsValid)
            {
                var resultado = await _PokemonRepositorio.Create(Pokemon);
                if ((resultado >= 1))
                {  
                    _logger.LogInformation("Creacion exitosa de pokemon: {NombrePokemon}",Pokemon.Name);
                    TempData["Success"] = "¡Datos guardados correctamente!";
                }
                else
                {
                    _logger.LogInformation("Error al intentar crear pokemon: {NombrePokemon}", Pokemon.Name);
                    TempData["Error"] = "¡Ocurrio un error al guardar los datos!";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Pokemon);
        }

        // GET: Pokemon/Edit/5
       // [Authorize(Policy = "Editar Pokemon")]
        public async Task<IActionResult> Edit(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var Pokemon = await _PokemonRepositorio.GetById(id);
            if (Pokemon == null)
            {
                return NotFound();
            }
            return View(Pokemon);
        }

        // POST: Pokemon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      //  [Authorize(Policy = "Editar Pokemon")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name")] Pokemon Pokemon)
        {
            if (id != Pokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var resultado = await _PokemonRepositorio.Update(Pokemon);
                    if ((resultado >= 1))
                    {
                        _logger.LogInformation("Edicion exitosa de pokemon: {NombrePokemon}", Pokemon.Name);
                        TempData["Success"] = "¡Datos guardados correctamente!";
                    }
                    else
                    {
                        _logger.LogInformation("Error al intentar editar pokemon: {NombrePokemon}", Pokemon.Name);
                        TempData["Error"] = "¡Ocurrio un error al guardar los datos!";
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    var existe = _PokemonRepositorio.Exists(Pokemon.Id);
                    if (!existe.Result)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Pokemon);
        }

        // GET: Pokemon/Delete/5
      //  [Authorize(Policy = "Eliminar Pokemon")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Pokemon = await _PokemonRepositorio.GetById(id);
            if (Pokemon == null)
            {
                return NotFound();
            }

            return View(Pokemon);
        }

        // POST: Pokemon/Delete/5
        //[Authorize(Policy = "Eliminar Pokemon")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var Pokemon = await _PokemonRepositorio.GetById(id);
            var resultado = await _PokemonRepositorio.Delete(Pokemon);
            if ((resultado >= 1))
            {
                _logger.LogInformation("Eliminacion exitosa de pokemon: {NombrePokemon}", Pokemon.Name);
                TempData["Success"] = "¡Datos guardados correctamente!";
            }
            else
            {
                _logger.LogInformation("Error al intentar eliminar pokemon: {NombrePokemon}", Pokemon.Name);
                TempData["Error"] = "¡Ocurrio un error al guardar los datos!";
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
