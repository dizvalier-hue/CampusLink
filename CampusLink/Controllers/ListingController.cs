using CampusLink.Data;
using CampusLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampusLink.Filters; // <-- ADDED THIS

namespace CampusLink.Controllers
{
    public class ListingController : Controller
    {
        private readonly ApplicationDbContext _db;

        // Dependency Injection in action!
        public ListingController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Listing
        public IActionResult Index()
        {
            // Fetch all listings and include the Category data so we can show the Category Name
            var listings = _db.Listings.Include(l => l.Category).ToList();
            return View(listings);
        }

        // GET: Listing/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var listing = _db.Listings.Include(l => l.Category).FirstOrDefault(l => l.Id == id);
            if (listing == null) return NotFound();

            return View(listing);
        }

        // GET: Listing/Create
        [RequireLogin] // <-- ADDED THIS
        public IActionResult Create()
        {
            // Pass the categories to the view so we can populate a dropdown list
            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "Name");
            return View();
        }

        // POST: Listing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireLogin] // <-- ADDED THIS
        public IActionResult Create(Listing obj)
        {
            if (ModelState.IsValid)
            {
                _db.Listings.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Listing created successfully";
                return RedirectToAction("Index");
            }

            // If validation fails, reload the dropdown list and return to the form
            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "Name");
            return View(obj);
        }

        // GET: Listing/Edit/5
        [RequireLogin] // <-- ADDED THIS
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var listing = _db.Listings.Find(id);
            if (listing == null) return NotFound();

            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "Name", listing.CategoryId);
            return View(listing);
        }

        // POST: Listing/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireLogin] // <-- ADDED THIS
        public IActionResult Edit(Listing obj)
        {
            if (ModelState.IsValid)
            {
                _db.Listings.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Listing updated successfully";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = new SelectList(_db.Categories, "Id", "Name");
            return View(obj);
        }

        // GET: Listing/Delete/5
        [RequireLogin] // <-- ADDED THIS
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var listing = _db.Listings.Include(l => l.Category)
                                       .FirstOrDefault(l => l.Id == id);
            if (listing == null) return NotFound();

            return View(listing);
        }

        // POST: Listing/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RequireLogin] // <-- ADDED THIS
        public IActionResult DeletePOST(int? id)
        {
            var listing = _db.Listings.Find(id);
            if (listing == null) return NotFound();

            _db.Listings.Remove(listing);
            _db.SaveChanges();
            TempData["success"] = "Listing deleted successfully";
            return RedirectToAction("Index");
        }
    }
}