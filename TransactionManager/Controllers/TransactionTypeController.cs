using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TransactionManager.Data;
using TransactionManager.Models;

namespace TransactionManager.Controllers
{
    public class TransactionTypeController : Controller
    {
        private readonly TransactionManagerContext _context;

        public TransactionTypeController(TransactionManagerContext context)
        {
            _context = context;
        }

        // GET: TransactionType
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransactionTypes.ToListAsync());
        }

        // GET: TransactionType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTypeModel = await _context.TransactionTypes
                .FirstOrDefaultAsync(m => m.TransactionTypeId == id);
            if (transactionTypeModel == null)
            {
                return NotFound();
            }

            return View(transactionTypeModel);
        }

        // GET: TransactionType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransactionType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionTypeId,Name,Commission")] TransactionTypeModel transactionTypeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionTypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionTypeModel);
        }

        // GET: TransactionType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTypeModel = await _context.TransactionTypes.FindAsync(id);
            if (transactionTypeModel == null)
            {
                return NotFound();
            }
            return View(transactionTypeModel);
        }

        // POST: TransactionType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionTypeId,Name,Commission")] TransactionTypeModel transactionTypeModel)
        {
            if (id != transactionTypeModel.TransactionTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionTypeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionTypeModelExists(transactionTypeModel.TransactionTypeId))
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
            return View(transactionTypeModel);
        }

        // GET: TransactionType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionTypeModel = await _context.TransactionTypes
                .FirstOrDefaultAsync(m => m.TransactionTypeId == id);
            if (transactionTypeModel == null)
            {
                return NotFound();
            }

            return View(transactionTypeModel);
        }

        // POST: TransactionType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionTypeModel = await _context.TransactionTypes.FindAsync(id);
            _context.TransactionTypes.Remove(transactionTypeModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionTypeModelExists(int id)
        {
            return _context.TransactionTypes.Any(e => e.TransactionTypeId == id);
        }
    }
}
