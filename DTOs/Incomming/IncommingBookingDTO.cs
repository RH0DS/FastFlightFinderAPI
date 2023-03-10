using System.ComponentModel.DataAnnotations;

public class IncommingBookingDTO{
    [Required]
    [Range (1 , 500, ErrorMessage =("The number of Seats needs to be atleast 1 and only positive integers. The maximum number of seats to book in a single request is 20") )]
    public int Seats { get; set; }

      [StringLength(50, MinimumLength = 1, ErrorMessage =("Please provide a firstname for the person responsible for the booking of type string, max 50chars") )]
    public string FirstName { get; set; }

    [StringLength(50, MinimumLength = 1, ErrorMessage =("Please provide a Lastname for the person responsible for the booking of type string, max 50chars") )]
    public string LastName { get; set; }

    [StringLength(50, MinimumLength = 1, ErrorMessage =("Please provide an email of type string, max 50chars") )]
    public string Email { get; set; }
    [Required (ErrorMessage = ("You must provide a list of flightIds. each flightId must be of type string"))]
    
    [FlightIdValidationAttribute ( ErrorMessage = "All flightIds must be of type string and of the right length")]
    public List<string> FlightNumbers { get; set; }

}