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
    public class CompanyController : Controller
    {
        private readonly TransactionManagerContext _context;

        public CompanyController(TransactionManagerContext context)
        {
            _context = context;
        }

        // GET: Company
        public async Task<IActionResult> Index(string status = "R")
        {

            switch (status)
            {
                case "U":
                    ViewData["AlertInfoVisible"] = "block";
                    ViewData["AlertMessage"] = "Successully updated the company";
                    break;
                case "D":
                    ViewData["AlertInfoVisible"] = "block";
                    ViewData["AlertMessage"] = "Successully deleted the company";
                    break;
                case "C":
                    ViewData["AlertInfoVisible"] = "block";
                    ViewData["AlertMessage"] = "Successully created the company";
                    break;
                default:
                    ViewData["AlertInfoVisible"] = "none";
                    break;
            }
            return View(await _context.Companies.ToListAsync());

        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (companyModel == null)
            {
                return NotFound();
            }

            return View(companyModel);
        }

        // GET: Company/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName,CompanyAddress,Ticker")] CompanyModel companyModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { status = "C" });
            }

            return View(companyModel);
        }

        // GET: Company/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _context.Companies.FindAsync(id);
            if (companyModel == null)
            {
                return NotFound();
            }

            return View(companyModel);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,CompanyName,CompanyAddress,Ticker")] CompanyModel companyModel)
        {
            if (id != companyModel.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyModelExists(companyModel.CompanyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index),new { status = "U" });
            }
            return View(companyModel);
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _context.Companies
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (companyModel == null)
            {
                return NotFound();
            }

            return View(companyModel);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyModel = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(companyModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { status = "D" });
        }

        private bool CompanyModelExists(int id)
        {
            return _context.Companies.Any(e => e.CompanyId == id);
        }
    }
}
