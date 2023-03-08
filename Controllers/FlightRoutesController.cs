using Microsoft.AspNetCore.Mvc;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using System.IO;
using System.Text.Json;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

  using Microsoft.EntityFrameworkCore;
namespace FastFlightFinderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightRoutesController : ControllerBase
    {
        

    private readonly IFlightRouteRepository _context;

    public FlightRoutesController( IFlightRouteRepository context)
    {
      
        _context = context;
        
    }


      [HttpGet]
    public async Task<ActionResult<IEnumerable<FlightRoute>>> GetFlightRoutes()
    {
        if (_context.FlightRouteExists == null)
        {
            return NotFound();
        }
    var  flightRoutes = await _context.GetAllFlightRoutes();
        return Ok(flightRoutes);
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<FlightRoute>> GetFlightRoute(string id)
    {
        if (_context.FlightRouteExists == null)
        {
            return NotFound();
        }
        var FlightRoute = await _context.GetFlightRoute(id);

        if (FlightRoute == null)
        {
            return NotFound();
        }

        return FlightRoute;
    }


    

    
    [HttpPost("nogot")] 
    public async Task<ActionResult<FlightRoute>> PostFlightRoute(FlightRoute FlightRoute)
    {

    
    if (_context.FlightRouteExists == null)
    {
        return Problem("Entity set 'DataContext.FlightRoute'  is null.");
    }
    await _context.CreateFlightRoute(FlightRoute);

    return CreatedAtAction("GetFlightRoute", new { id = FlightRoute.RouteId }, FlightRoute);
    }
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteProduct(string id)
    {
        if (_context.FlightRouteExists == null)
        {
            return NotFound();
        }
        var product = await _context.GetFlightRoute(id);
        if (product == null)
        {
            return NotFound();
        }

        await  _context.DeleteFlightRoute(id);
        

        return NoContent();
    }

  



    }
}