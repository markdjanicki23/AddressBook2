using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressBook2.Data;
using AddressBook2.Models;
using Microsoft.AspNetCore.Http;
using AddressBook2.Utilities;

namespace AddressBook2.Controllers
{
    public class AddressBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddressBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AddressBooks
        public async Task<IActionResult> Index()
        {          
            return View(await _context.AddressBook.ToListAsync());
        }

        // GET: AddressBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBook = await _context.AddressBook
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressBook == null)
            {
                return NotFound();
            }

            return View(addressBook);
        }

        // GET: AddressBooks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AddressBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Address1,Address2,City,State,ZipCode,PhoneNumber")] AddressBook addressBook, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                addressBook.DateAdded = DateTime.Now;

                if (image != null) 
                { 
                addressBook.ImageData = AvatarHelper.PutImage(image);

                addressBook.ImagePath = image.FileName;
                }

                _context.Add(addressBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(addressBook);
        }

        // GET: AddressBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBook = await _context.AddressBook.FindAsync(id);
            if (addressBook == null)
            {
                return NotFound();
            }
            return View(addressBook);
        }

        // POST: AddressBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,ImagePath,ImageData,Address1,Address2,City,State,ZipCode,PhoneNumber")] AddressBook addressBook, IFormFile image)
        {
            if (id != addressBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (image != null)
                    {
                        addressBook.ImageData = AvatarHelper.PutImage(image);

                        addressBook.ImagePath = image.FileName;
                    }

                    _context.Update(addressBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressBookExists(addressBook.Id))
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
            return View(addressBook);
        }

        // GET: AddressBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addressBook = await _context.AddressBook
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressBook == null)
            {
                return NotFound();
            }

            return View(addressBook);
        }

        // POST: AddressBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var addressBook = await _context.AddressBook.FindAsync(id);
            _context.AddressBook.Remove(addressBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressBookExists(int id)
        {
            return _context.AddressBook.Any(e => e.Id == id);
        }
    }
}
