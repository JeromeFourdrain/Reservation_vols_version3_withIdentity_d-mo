#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservation_vols_version2_withEF.Models;
using Reservation_vols_version3_withIdentity_démo.Models;

namespace Reservation_vols_version2_withEF.Controllers
{
    public class TicketsController : Controller
    {
        private readonly Reservation_volsContext _context;

        public TicketsController(Reservation_volsContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var reservation_volsContext = _context.Tickets.Include(t => t.Client).Include(t => t.Flight).Include(t => t.Passenger);
            return View(await reservation_volsContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Client)
                .Include(t => t.Flight)
                .Include(t => t.Passenger)
                .FirstOrDefaultAsync(m => m.Ticketid == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Clientid", "Clientid");
            ViewData["FlightId"] = new SelectList(_context.Flights, "Flightid", "Flightid");
            ViewData["PassengerId"] = new SelectList(_context.Clients, "Clientid", "Clientid");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ticketid,Isconfirmed,FlightId,ClientId,PassengerId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Clientid", "Clientid", ticket.ClientId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "Flightid", "Flightid", ticket.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Clients, "Clientid", "Clientid", ticket.PassengerId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Clientid", "Clientid", ticket.ClientId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "Flightid", "Flightid", ticket.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Clients, "Clientid", "Clientid", ticket.PassengerId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Ticketid,Isconfirmed,FlightId,ClientId,PassengerId")] Ticket ticket)
        {
            if (id != ticket.Ticketid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Ticketid))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Clientid", "Clientid", ticket.ClientId);
            ViewData["FlightId"] = new SelectList(_context.Flights, "Flightid", "Flightid", ticket.FlightId);
            ViewData["PassengerId"] = new SelectList(_context.Clients, "Clientid", "Clientid", ticket.PassengerId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Client)
                .Include(t => t.Flight)
                .Include(t => t.Passenger)
                .FirstOrDefaultAsync(m => m.Ticketid == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Ticketid == id);
        }
    }
}
