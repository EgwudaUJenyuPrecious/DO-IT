using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Doit.Data;
using Doit.Models;
using Microsoft.AspNetCore.Hosting;
using Doit.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Doit.Controllers
{
    [Authorize]
    public class MeditationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment environment;
        FileHelper _fileHelper;

        public MeditationsController(ApplicationDbContext context,IWebHostEnvironment environment)
        {
            _context = context;
            this.environment = environment;
            _fileHelper = new FileHelper(environment);
        }

        // GET: Meditations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Meditations.ToListAsync());
        }

        // GET: Meditations/Details/5
        public async Task<IActionResult> MeditationDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meditation = await _context.Meditations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meditation == null)
            {
                return NotFound();
            }
            
            return View(meditation);
        }

        // GET: Meditations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meditations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meditation meditation)
        {
            if (ModelState.IsValid)
            {
                if(meditation.VideoFile !=  null)
                {
                    meditation.VideoUrl = _fileHelper.UploadFile(meditation.VideoFile);
                    
                }
                if(meditation.AudioFile != null)
                {
                    meditation.AudioUrl = _fileHelper.UploadFile(meditation.AudioFile);
                }

                _context.Add(meditation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
         
            return View(meditation);
        }

        // GET: Meditations/Edit/5
        public async Task<IActionResult> EditMeditation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meditation = await _context.Meditations.FindAsync(id);
            if (meditation == null)
            {
                return NotFound();
            }
            return View(meditation);
        }

        // POST: Meditations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMeditation(int id, Meditation meditation)
        {
            if (id != meditation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (meditation.VideoFile != null)
                    {
                        meditation.VideoUrl = _fileHelper.UploadFile(meditation.VideoFile);

                    }
                    if (meditation.AudioFile != null)
                    {
                        meditation.AudioUrl = _fileHelper.UploadFile(meditation.AudioFile);
                    }

                    _context.Update(meditation);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    if (!MeditationExists(meditation.Id))
                    {
                        return NotFound(ex.Message);
                    }
                    else
                    {
                        ViewData["msg"] = "oops! Something went wrong.";
                        
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        

        // GET: Meditations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meditation = await _context.Meditations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meditation == null)
            {
                return NotFound();
            }

            return View(meditation);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meditation = await _context.Meditations.FindAsync(id);
            _context.Meditations.Remove(meditation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeditationExists(int id)
        {
            return _context.Meditations.Any(e => e.Id == id);
        }
    }
}
