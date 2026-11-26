using CampusLink.Data;
using CampusLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CampusLink.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Account/Register
        public IActionResult Register() => View();

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Student obj)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existing = _db.Students.FirstOrDefault(s => s.Email == obj.Email);
                if (existing != null)
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(obj);
                }

                _db.Students.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Account created! Please log in.";
                return RedirectToAction("Login");
            }
            return View(obj);
        }

        // GET: Account/Login
        public IActionResult Login() => View();

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var student = _db.Students.FirstOrDefault(s => s.Email == email && s.Password == password);
            if (student != null)
            {
                // Save to cookie session
                HttpContext.Session.SetString("UserEmail", student.Email);
                TempData["success"] = "Welcome back!";
                return RedirectToAction("Index", "Listing");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        // POST: Account/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["success"] = "Logged out successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}