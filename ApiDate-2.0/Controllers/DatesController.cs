using System.Net;
using Microsoft.AspNetCore.Mvc;
using ApiDate_2._0.Models;
using ApiDate_2._0.Db;
using Microsoft.EntityFrameworkCore;

namespace ApiDate_2._0.Controllers;

[ApiController]
[Route("[controller]")]
public class DatesController : Controller
{

    private DatesContext _context;

    public DatesController(DatesContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetDays")]
    public async Task<ActionResult<IEnumerable<Days>>> Get()
    {
        return await _context.Days.ToListAsync();
    }

    [HttpPost(Name = "PostDay")]
    public async Task<ActionResult<Days>> Post(Days Day)
    {
        _context.Days.Add(Day);
        await _context.SaveChangesAsync();
        return Day;
    }

    [HttpGet("{id}", Name = "GetDay")]
    public async Task<ActionResult<Days>> Get(int id)
    {
        var Day = await _context.Days.FindAsync(id);
        if (Day == null)
        {
            return NotFound();
        }

        return Day;
    }

    [HttpPut("{id}", Name = "PutDay")]
    public async Task<ActionResult<Days>> Put(int id, Days Day)
    {
        if (id != Day.Id)
        {
            return BadRequest();
        }

        _context.Entry(Day).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpGet("HolidaysBetween/{startDate}/{endDate}", Name = "GetHolidaysBetween")]
    public async Task<ActionResult<IEnumerable<Days>>> GetHolidaysBetween(DateTime startDate, DateTime endDate)
    {
        var holidays = await _context.Days
            .Where(d => d.Date >= startDate && d.Date <= endDate && d.IsHoliday)
            .ToListAsync();

        return holidays;
    }
    
    [HttpGet("daysWorked/{startDate}/{endDate}", Name = "GetDaysWorked")]
    public async Task<ActionResult<IEnumerable<Days>>> GetDaysWorked(DateTime startDate, DateTime endDate)
    {
        var daysWorked = await _context.Days
            .Where(d => d.Date >= startDate && d.Date <= endDate && !d.IsHoliday)
            .ToListAsync();

        return daysWorked;
    }
}
    

