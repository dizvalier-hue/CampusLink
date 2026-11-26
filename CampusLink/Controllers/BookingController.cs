using CampusLink.Data;
using CampusLink.Models;
using Microsoft.AspNetCore.Mvc;
using CampusLink.Filters; // <-- ADDED THIS

namespace CampusLink.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Booking/Create
        [RequireLogin] // <-- ADDED THIS
        public IActionResult Create(int listingId)
        {
            var booking = new Booking { ListingId = listingId };
            // Fetch the listing so we can show the title on the booking page
            ViewBag.ListingTitle = _db.Listings.Find(listingId)?.Title;
            return View(booking);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireLogin] // <-- ADDED THIS
        public IActionResult Create(Booking obj)
        {
            if (ModelState.IsValid)
            {
                _db.Bookings.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Service booked successfully!";
                return RedirectToAction("Index", "Listing");
            }
            ViewBag.ListingTitle = _db.Listings.Find(obj.ListingId)?.Title;
            return View(obj);
        }
    }
}