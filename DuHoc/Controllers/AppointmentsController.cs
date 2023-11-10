using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuHoc.Data;
using DuHoc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DuHoc.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly DuHocContext _context;

        public AppointmentsController(DuHocContext context)
        {
            _context = context;
        }

        // GET: Appointments
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var duHocContext = _context.Appointment.Include(a => a.User);
            return View(await duHocContext.ToListAsync());
        }

        public async Task<IActionResult> Appointment()
        {
            var duHocContext = _context.Appointment.Include(a => a.User);
            return View(await duHocContext.OrderByDescending(y=>y.Appointment_Id).ToListAsync());
        }

        //Handle Status
        [HttpPost]
        public IActionResult UpdateStatus(int Appointment_Id, int Status)
        {
            var appointment = _context.Appointment.Find(Appointment_Id);

            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Status = Status;
            _context.SaveChanges();

            return Ok();
        }


        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Appointment_Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Appointment_Id,user_id,Appointment_Date,Title,Decription,Create_At,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id", appointment.user_id);
            return View(appointment);
        }

        // GET: Appointments/Create_Appointment
        public IActionResult Appointment_Create()
        {
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Appointment_Create([Bind("Appointment_Id,Appointment_Date,Title,Decription,Create_At,Status")] Appointment appointment)
        {
            var currentName = User.Identity.Name;
            var user = _context.User.SingleOrDefault(u => u.user_name == currentName);
            if (ModelState.IsValid)
            {
                var currentId = user.user_id;
                appointment.user_id = currentId;

                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Appointment));
            }

            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id");
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id", appointment.user_id);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Appointment_Id,user_id,Appointment_Date,Title,Decription,Create_At,Status")] Appointment appointment)
        {
            if (id != appointment.Appointment_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Appointment_Id))
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
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id", appointment.user_id);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Appointment_Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointment == null)
            {
                return Problem("Entity set 'DuHocContext.Appointment'  is null.");
            }
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointment.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return _context.Appointment.Any(e => e.Appointment_Id == id);
        }
    }
}
