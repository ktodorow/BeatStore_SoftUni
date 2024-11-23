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

    public async Task<IActionResult> Index()
    {
        var beats = await _context.Beats
            .Include(b => b.Artist) 
            .ToListAsync();


        foreach (var beat in beats)
        {
            if (string.IsNullOrEmpty(beat.CoverArtUrl))
            {
                beat.CoverArtUrl = "/images/default-cover-art.jpg";
            }
        }

        return View(beats);
    }
}