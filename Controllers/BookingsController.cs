using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]

public class BookingsController : ControllerBase{

    
    private readonly IBookingRepository _context;
    private readonly IMapper _mapper;

    public BookingsController( IBookingRepository context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    [HttpPost("Create")] 
    public async Task<ActionResult> CreateAsync( IncommingBookingDTO incommingBooking )
    {
    
        
        foreach (var flightId in incommingBooking.FlightNumbers)
        {
            if(! await _context.SeatsAvailableAsync(flightId, incommingBooking.Seats)){
                return NotFound("Not enough available seats where found");
            }
        }

    return await _context.CreateBooking(incommingBooking) ? Ok("Your bookinig has been created") : BadRequest("Something went wrong");
    }




}


