﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaginaPessoal_BD.Data;
using PaginaPessoal_BD.Models;

namespace PaginaPessoal_BD.Controllers
{
    
    public class ExperienciasController : Controller
    {
        private readonly PaginaPessoalBDContext _context;

        public ExperienciasController(PaginaPessoalBDContext context)
        {
            _context = context;
        }

        // GET: Experiencias
        public async Task<IActionResult> Index(string nomePesquisa, int pagina = 1)
        {

            Paginacao paginacao = new Paginacao
            {
                
                TotalItems = await _context.Experiencia.Where(e => nomePesquisa == null || e.NomeEmpresa.Contains(nomePesquisa)).CountAsync(),
                PaginaAtual = pagina
            };

            List<Experiencia> experiencias = await _context.Experiencia.Where(e => nomePesquisa == null || e.NomeEmpresa.Contains(nomePesquisa))

                .OrderByDescending(e => e.DataInicio)
                .Skip(paginacao.ItemsPorPagina * (pagina - 1)) //para passar para os proximos registos
                .Take(paginacao.ItemsPorPagina) //indicação de quantos registos vai buscar por pagina
                .ToListAsync();


            ListaExperienciasVIewModel modelo = new ListaExperienciasVIewModel
            {
                Paginacao = paginacao,
                Experiencias = experiencias,
                NomePesquisa = nomePesquisa
            };

            return base.View(modelo); 
        }

        [Authorize(Roles = "admin, visitantes")]
        // GET: Experiencias/Details/5
        public async Task<IActionResult> Details(int? id) // O ponto de interrogação em (int?) diz que é opcional
        {
            if (id == null) // ir buscar o produto. se não encontrar devolve NotFound()
            {
                return View("Inexistente"); // estou a mandá-los para a view (página) Inexistente.
            }

            var experiencia = await _context.Experiencia
                .FirstOrDefaultAsync(m => m.ExperienciaId == id);
            if (experiencia == null)
            {
                return NotFound();
            }

            return View(experiencia);
        }

        [Authorize(Roles = "admin")]
        // GET: Experiencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Experiencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExperienciaId,NomeEmpresa,Cargo,DataInicio,DataFim,Funcoes,Foto")] Experiencia experiencia, IFormFile fotoFicheiro)
        {

            //se o ficheiro da foto é diferente de nulo, ou seja, se tem lá uma foto. E se tem um tamanho de bites maior que zero
            if (fotoFicheiro != null && fotoFicheiro.Length > 0)
            {
                using (var ficheiroMemoria = new MemoryStream()) // o using faz com que qdo não precisar disto, vai desaparecer da memória
                {
                    fotoFicheiro.CopyTo(ficheiroMemoria); //estamos a passar o ficheiro que está do lado do usuario para o servidor
                    experiencia.Foto = ficheiroMemoria.ToArray();
                }
            }


            if (ModelState.IsValid) //valida o modelo. diz-me se é válido e apresenta o seguinte
            {
                _context.Add(experiencia);
                await _context.SaveChangesAsync();
                return View("Sucesso");
            }


            //_context.Add(experiencia);
            //await _context.SaveChangesAsync();

            return View(experiencia); // se não for válido, devolve isto
        }

        [Authorize(Roles = "admin")]
        // GET: Experiencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiencia = await _context.Experiencia.FindAsync(id);
            if (experiencia == null)
            {
                return View("Inexistente");
            }
            return View(experiencia);
        }

        // POST: Experiencias/Edit/5 AÇAO QUANDO SE MANDA GRAVAR UMA EXPERIENCIA
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExperienciaId,NomeEmpresa,Cargo,DataInicio,DataFim,Funcoes,Foto")] Experiencia experiencia, IFormFile fotoFicheiro)
        {
            if (id != experiencia.ExperienciaId) //se o id não for igual à experienciaid acaba logo aqui
            {
                return NotFound();
            }



            if (!ModelState.IsValid)
            {
                return View(experiencia); }


                try
                {
                    if (fotoFicheiro != null && fotoFicheiro.Length > 0)
                    {
                        using (var ficheiroMemoria = new MemoryStream())
                        {
                            fotoFicheiro.CopyTo(ficheiroMemoria);
                            experiencia.Foto = ficheiroMemoria.ToArray();
                        }
                    }

                    _context.Update(experiencia);
                    await _context.SaveChangesAsync();
                }


                catch (DbUpdateConcurrencyException) // Esta exepçao o que apanha é se duas pessoas estão a tentar editar o mesmo registo ao mesmo tempo
                {
                    if (!ExperienciaExists(experiencia.ExperienciaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                    return View(experiencia);
                    }
                }
          
            return View("Sucesso");
        }

        [Authorize(Roles = "admin")]
        // GET: Experiencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiencia = await _context.Experiencia
                .FirstOrDefaultAsync(m => m.ExperienciaId == id);
            if (experiencia == null)
            {
                return NotFound();
            }

            return View(experiencia);
        }

        // POST: Experiencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experiencia = await _context.Experiencia.FindAsync(id); // vai buscar a experiencia que estou a tentar encontrar
            _context.Experiencia.Remove(experiencia);
            await _context.SaveChangesAsync();

            ViewBag.Mensage = "O produto foi eliminado com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        private bool ExperienciaExists(int id)
        {
            return _context.Experiencia.Any(e => e.ExperienciaId == id); // procura na base de dados se existe alguma experiencia com aquele id. O "e" poderia ser qualquer letra, ou até palavra. Associa a uma experiencia. Tamos aqui a definir a mesma. É como se fosse um "i" num ciclo for. É um lambda.
        }
    }
}
