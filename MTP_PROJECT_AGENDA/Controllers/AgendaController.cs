using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTP_PROJECT_AGENDA.Areas.Identity.Data;
using MTP_PROJECT_AGENDA.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MTP_PROJECT_AGENDA.Data;

namespace MTP_PROJECT_AGENDA.Controllers
{
    public class AgendaController : Controller
    {
        private readonly AuthDBContext _context;

        public AgendaController(AuthDBContext context)
        {
            _context = context;
        }
        // GET: AgendaController1
        public async Task<IActionResult> Index()
        {
            var agendas = await _context.agenda.ToListAsync(); 
            return View(agendas);
        }

        // GET: AgendaController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AgendaController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgendaController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AgendaController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AgendaController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AgendaController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AgendaController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
