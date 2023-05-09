﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD.Application.Extensions;
using SD.Persistence.Repositories.DBContext;
using SD.WebApp.Extensions;
using Wifi.SD.Core.Application.Movies.Queries;
using Wifi.SD.Core.Application.Movies.Results;
using Wifi.SD.Core.Entities.Movies;

namespace SD.WebApp.Controllers
{
    //[Authorize (Roles = "Admin")]  // so wäre der ganz controller für alle ausser Admins gesperrt
    public class MoviesController : MediatRBaseController
    {
        private readonly MovieDbContext _context;

        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        //[Authorize]
        [AllowAnonymous] // würde diese methode vom Authorize ausnehmen
        public async Task<string> HelloWorld(string name)
        {
            
                return await Task.FromResult($"Hello {name}");
          
        }


        // GET: Movies
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var movieQuery = new GetMovieDtosQuery();
            var result = await base.Mediator.Send(movieQuery, cancellationToken);
            return View(result);

        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(Guid? id, CancellationToken cancellationToken)
        {
            var movieQuery = new GetMovieDtoQuery { Id = id.Value };
            var result = await base.Mediator.Send(movieQuery, cancellationToken);
            return View(result);

        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            ViewData["MediumTypeCode"] = new SelectList(_context.MediumTypes, "Code", "Code");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Price,GenreId,MediumTypeCode,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.Id = Guid.NewGuid();
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            ViewData["MediumTypeCode"] = new SelectList(_context.MediumTypes, "Code", "Code", movie.MediumTypeCode);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(Guid? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieQuery = new GetMovieDtoQuery { Id = id.Value };
            var result = await base.Mediator.Send(movieQuery, cancellationToken);
            
            if (result == null)
            {
                return NotFound();
            }


            //ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            //ViewData["MediumTypeCode"] = new SelectList(_context.MediumTypes, "Code", "Code", movie.MediumTypeCode);
            await this.InitMasterDataViewData(result, cancellationToken);

            return View(result);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,ReleaseDate,Price,GenreId,MediumTypeCode,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", movie.GenreId);
            ViewData["MediumTypeCode"] = new SelectList(_context.MediumTypes, "Code", "Code", movie.MediumTypeCode);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.MediumType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'MovieDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        #region Some private helpers

        private bool MovieExists(Guid id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task InitMasterDataViewData(MovieDto movieDto, CancellationToken cancellationToken)
        {
            SelectList genreSelectList = null;
            SelectList MediumTypeSelectList = null;



            var genres = this.HttpContext.Session.Get<IEnumerable<Genre>>("Genres");
            if(genres == null)
            {
                genres = await this.Mediator.Send(new GetGenresQuery(), cancellationToken);
                this.HttpContext.Session.Set<IEnumerable<Genre>>("Genres", genres);
            }
       
            var mediumTypes = this.HttpContext.Session.Get<IEnumerable<MediumType>>("MediumTypes");
            if (mediumTypes == null)
            {
                mediumTypes = await this.Mediator.Send(new GetMediumTypesQuery(), cancellationToken);
                this.HttpContext.Session.Set<IEnumerable<MediumType>>("MediumTypes", mediumTypes);
            }

            var localizedRatings = this.HttpContext.Session.Get<List<KeyValuePair<object, string>>>("Ratings");
            if (localizedRatings == null)
            {
                localizedRatings = EnumExtension.EnumToList<Ratings>();
                this.HttpContext.Session.Set<List<KeyValuePair<object, string>>>("Ratings", localizedRatings);
            }


            // zwei Wege .... beide Zeilen machen quasi dasselbe
            ViewBag.Genres = new SelectList(genres, nameof(Genre.Id), nameof(Genre.Name), movieDto.GenreId);
            ViewData.Add("MediumTypes", new SelectList(mediumTypes, nameof(MediumType.Code), nameof(MediumType.Name), movieDto.MediumTypeCode));
            ViewBag.Ratings = new SelectList(localizedRatings, "Key", "Value", movieDto.Rating);
        }

        #endregion
    }
}
