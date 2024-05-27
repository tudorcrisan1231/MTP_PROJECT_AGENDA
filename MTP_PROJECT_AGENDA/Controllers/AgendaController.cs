using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTP_PROJECT_AGENDA.Areas.Identity.Data;
using MTP_PROJECT_AGENDA.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MTP_PROJECT_AGENDA.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MTP_PROJECT_AGENDA.Controllers
{
    public class AgendaController : Controller
    {
        private readonly AuthDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AgendaController(AuthDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: AgendaController1
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not found
            }

            // Get agendas for the current user
            var agendas = await _context.agenda
                .Where(a => a.UserId == user.Id)
                .ToListAsync();

            return View(agendas);
        }

        // GET: AgendaController1/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not found
            }

            // Get the agenda with the specified ID
            var agenda = await _context.agenda.FirstOrDefaultAsync(a => a.Id == id);

            // Check if the agenda exists
            if (agenda == null)
            {
                return NotFound(); // Return 404 Not Found if agenda is not found
            }

            // Check if the logged-in user has access to the agenda
            if (agenda.UserId != user.Id)
            {
                return Forbid(); // Return 403 Forbidden if user does not have access
            }

            return View(agenda);
        }

        // GET: AgendaController1/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgendaController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Agenda agenda)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                agenda.UserId = user.Id;
                agenda.CreatedAt = DateTime.Now;
                _context.Add(agenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AgendaController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not found
            }

            // Get the agenda with the specified ID
            var agenda = await _context.agenda.FirstOrDefaultAsync(a => a.Id == id);

            // Check if the agenda exists
            if (agenda == null)
            {
                return NotFound(); // Return 404 Not Found if agenda is not found
            }

            // Check if the logged-in user has access to edit the agenda
            if (agenda.UserId != user.Id)
            {
                return Forbid(); // Return 403 Forbidden if user does not have access
            }

            return View(agenda);
        }

        // POST: AgendaController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Agenda agenda)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not found
            }

            // Check if the agenda ID matches the provided ID
            if (id != agenda.Id)
            {
                return NotFound(); // Return 404 Not Found if agenda ID doesn't match
            }


            try
            {
                agenda.UserId = user.Id;
                _context.Update(agenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaExists(agenda.Id))
                {
                    return NotFound(); // Return 404 Not Found if agenda does not exist
                }
                else
                {
                    throw; // Rethrow exception if concurrency conflict occurs
                }
            }
            return View(agenda);
        }

        private bool AgendaExists(int id)
        {
            return _context.agenda.Any(e => e.Id == id);
        }

        // GET: AgendaController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not found
            }

            // Get the agenda with the specified ID
            var agenda = await _context.agenda.FirstOrDefaultAsync(a => a.Id == id);

            // Check if the agenda exists
            if (agenda == null)
            {
                return NotFound(); // Return 404 Not Found if agenda is not found
            }

            // Check if the logged-in user has access to the agenda
            if (agenda.UserId != user.Id)
            {
                return Forbid(); // Return 403 Forbidden if user does not have access
            }

            return View(agenda);
        }

        // POST: AgendaController/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if user is not found
            }

            // Get the agenda with the specified ID
            var agenda = await _context.agenda.FindAsync(id);

            // Check if the agenda exists
            if (agenda == null)
            {
                return NotFound(); // Return 404 Not Found if agenda is not found
            }

            // Check if the logged-in user has access to delete the agenda
            if (agenda.UserId != user.Id)
            {
                return Forbid(); // Return 403 Forbidden if user does not have access
            }

            _context.agenda.Remove(agenda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
