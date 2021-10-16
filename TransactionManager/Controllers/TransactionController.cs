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
    public class TransactionController : Controller
    {
        private readonly TransactionManagerContext _context;

        public TransactionController(TransactionManagerContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index(int companyId = 0, bool orderBy = true, string status = "R")
        {
            ViewBag.OrderBy = orderBy;

            switch (status)
            {
                case "U":
                    ViewData["AlertInfoVisible"] = "block";
                    ViewData["AlertMessage"] = "Successully updated the transaction";
                    break;
                case "D":
                    ViewData["AlertInfoVisible"] = "block";
                    ViewData["AlertMessage"] = "Successully deleted the transaction";
                    break;
                case "C":
                    ViewData["AlertInfoVisible"] = "block";
                    ViewData["AlertMessage"] = "Successully created the transaction";
                    break;
                default:
                    ViewData["AlertInfoVisible"] = "none";
                    break;
            }

            if (companyId > 0)
            {
                var transactionManagerContext = orderBy ? _context.Transactions.Include(t => t.Company).Include(t => t.TransactionType).Where(t => t.CompanyId == companyId).OrderBy(o => o.Company.CompanyName) : _context.Transactions.Include(t => t.Company).Include(t => t.TransactionType).Where(t => t.CompanyId == companyId).OrderByDescending(d => d.Company.CompanyName);
                ViewBag.IsCompanySelected = true;
                return View(await transactionManagerContext.ToListAsync());

            }
            else
            {
                var transactionManagerContext = orderBy ? _context.Transactions.Include(t => t.Company).Include(t => t.TransactionType).OrderBy(o => o.Company.CompanyName) : _context.Transactions.Include(t => t.Company).Include(t => t.TransactionType).OrderByDescending(d => d.Company.CompanyName);
                ViewBag.IsCompanySelected = false;
                return View(await transactionManagerContext.ToListAsync());

            }

        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionRecordModel = await _context.Transactions
                .Include(t => t.Company)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(m => m.TransactionRecordId == id);
            if (transactionRecordModel == null)
            {
                return NotFound();
            }

            return View(transactionRecordModel);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName");
            ViewData["TransactionTypeId"] = new SelectList(_context.Set<TransactionTypeModel>(), "TransactionTypeId", "Name");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionRecordId,Quantity,SharePrice,CompanyId,TransactionTypeId")] TransactionRecordModel transactionRecordModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionRecordModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { status = "C"});
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", transactionRecordModel.CompanyId);
            ViewData["TransactionTypeId"] = new SelectList(_context.Set<TransactionTypeModel>(), "TransactionTypeId", "TransactionTypeId", transactionRecordModel.TransactionTypeId);
            return View(transactionRecordModel);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionRecordModel = await _context.Transactions.FindAsync(id);
            if (transactionRecordModel == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName");
            ViewData["TransactionTypeId"] = new SelectList(_context.Set<TransactionTypeModel>(), "TransactionTypeId", "Name");
            return View(transactionRecordModel);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionRecordId,Quantity,SharePrice,CompanyId,TransactionTypeId")] TransactionRecordModel transactionRecordModel)
        {
            if (id != transactionRecordModel.TransactionRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionRecordModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionRecordModelExists(transactionRecordModel.TransactionRecordId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { status = "U" });
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", transactionRecordModel.CompanyId);
            ViewData["TransactionTypeId"] = new SelectList(_context.Set<TransactionTypeModel>(), "TransactionTypeId", "TransactionTypeId", transactionRecordModel.TransactionTypeId);
            return View(transactionRecordModel);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionRecordModel = await _context.Transactions
                .Include(t => t.Company)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(m => m.TransactionRecordId == id);
            if (transactionRecordModel == null)
            {
                return NotFound();
            }

            return View(transactionRecordModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionRecordModel = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transactionRecordModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { status = "D" });
        }

        private bool TransactionRecordModelExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionRecordId == id);
        }
    }
}
