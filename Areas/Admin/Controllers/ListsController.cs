using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalToDoList.Data.Context;
using PersonalToDoList.Data.Entities;

namespace PersonalToDoList.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ListsController : Controller
    {
        private readonly Context _context;

        public ListsController(Context context)
        {
            _context = context;
        }

        // GET: Admin/Lists
        public async Task<IActionResult> Index()
        {
              return _context.Lists != null ? 
                          View(await _context.Lists.ToListAsync()) :
                          Problem("Entity set 'Context.Lists'  is null.");
        }

        // GET: Admin/Lists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var list = await _context.Lists
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        // GET: Admin/Lists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Lists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListId,ToDo,Time,Category")] List list)
        {
            
                _context.Add(list);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Admin/Lists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }

        // POST: Admin/Lists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListId,ToDo,Time,Category")] List list)
        {
            if (id != list.ListId)
            {
                return NotFound();
            }
 
                try
                {
                    _context.Update(list);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListExists(list.ListId))
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

        // GET: Admin/Lists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var list = await _context.Lists
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        // POST: Admin/Lists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lists == null)
            {
                return Problem("Entity set 'Context.Lists'  is null.");
            }
            var list = await _context.Lists.FindAsync(id);
            if (list != null)
            {
                _context.Lists.Remove(list);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListExists(int id)
        {
          return (_context.Lists?.Any(e => e.ListId == id)).GetValueOrDefault();
        }
    }
}
