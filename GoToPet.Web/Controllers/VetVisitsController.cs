using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoToPet.Web.Data;

namespace GoToPet.Web.Controllers;

public class VetVisitsController : Controller
{
    private readonly GoToPetContext _db;

    public VetVisitsController(GoToPetContext db) => _db = db;

    public async Task<IActionResult> Index(string? visitType)
    {
        var query = _db.VetVisits
            .Include(v => v.Pet)
            .Include(v => v.Vaccinations)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(visitType))
            query = query.Where(v => v.VisitType == visitType);

        var visits = await query
            .OrderByDescending(v => v.VisitDate)
            .Take(100)
            .ToListAsync();

        ViewBag.VisitType = visitType;
        return View(visits);
    }
}
