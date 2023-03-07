using Microsoft.AspNetCore.Mvc;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

  using Microsoft.EntityFrameworkCore;
namespace FastFlightFinderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly DataContext _context;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        // [HttpGet(Name = "GetWeatherForecast")]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //         TemperatureC = Random.Shared.Next(-20, 55),
        //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }

         [HttpGet]
    
        public async Task<ActionResult<IEnumerable<FlightRoute>>> GetProducts()
        {
            if (_context.FlightRoutes == null)
            {
                return NotFound();
            }
        var  flightRoutes = await _context.FlightRoutes.ToListAsync();


            return Ok(flightRoutes);
        }

          [HttpPost]
      [Authorize (Roles ="admin, super-admin")  ]
    public async Task<ActionResult<FlightRoute>> PostFlightRoute(FlightRoute FlightRoute)
    {
        if (_context.FlightRoutes == null)
        {
            return Problem("Entity set 'DataContext.FlightRoute'  is null.");
        }
        _context.FlightRoutes.Add(FlightRoute);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (FlightRouteExists(FlightRoute.RouteId))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        
        return CreatedAtAction("GetProduct", new { id = FlightRoute.RouteId }, FlightRoute);
    }


        private bool FlightRouteExists(string id)
    {
        return (_context.FlightRoutes?.Any(e => e.RouteId == id)).GetValueOrDefault();
    }



    }
}