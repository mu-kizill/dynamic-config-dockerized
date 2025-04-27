using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConfigApi.Data;
using ConfigApi.Models;

namespace ConfigApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConfigurationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ConfigurationsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/configurations?applicationName=SERVICE-A
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Configuration>>> GetConfigurations([FromQuery] string applicationName)
    {
        return await _context.Configurations
            .Where(c => c.ApplicationName == applicationName)
            .ToListAsync();
    }

    // GET: api/configurations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Configuration>> GetConfiguration(int id)
    {
        var config = await _context.Configurations.FindAsync(id);
        if (config == null) return NotFound();
        return config;
    }

    // POST: api/configurations
    [HttpPost]
    public async Task<ActionResult<Configuration>> PostConfiguration(Configuration config)
    {
        _context.Configurations.Add(config);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetConfiguration), new { id = config.Id }, config);
    }

    // PUT: api/configurations/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutConfiguration(int id, Configuration updated)
    {
        if (id != updated.Id) return BadRequest();

        _context.Entry(updated).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Configurations.Any(e => e.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }
}
