using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeatStore_SoftUni.Data;
using BeatStore_SoftUni.Data.Models;

public class BeatsController : Controller
{
    private readonly ApplicationDbContext _context;

    public BeatsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Beats
    public async Task<IActionResult> Index()
    {
        // Fetch all beats, including the artist details
        var beats = await _context.Beats
            .Include(b => b.Artist) // Ensure Artist details are included
            .ToListAsync();

        return View(beats);
    }
}