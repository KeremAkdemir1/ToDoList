using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalToDoList.Data.Context;
using PersonalToDoList.Data.Entities;
using PersonalToDoList.Data.Repositories;

namespace PersonalToDoList.Controllers
{
    public class ListController : Controller
    {
        private readonly Context _context;

        public ListController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Set<List>().ToList());
        }
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

    }
}

