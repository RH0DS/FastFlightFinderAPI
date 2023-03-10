using System.ComponentModel.DataAnnotations;

public class IncommingBookingDTO{
    [Required]
    [Range (1 , 500, ErrorMessage =("Please provide a positive number of seats you want to book") )]
    public int Seats { get; set; }
    [Range (1 , 500, ErrorMessage =("Please provide a positive number of seats you want to book") )]
    public string? FirstName { get; set; }
    [Range (1 , 500, ErrorMessage =("Please provide a firstname for the person responsible for the booking") )]
    public string? LastName { get; set; }
    [Range (1 , 500, ErrorMessage =("Please provide a positive number of seats you want to book") )]
    public string? Email { get; set; }
    [Required]

    public List<string> FlightNumbers { get; set; }

}