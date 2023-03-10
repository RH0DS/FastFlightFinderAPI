using System.ComponentModel.DataAnnotations;

public class FlightIdValidationAttribute :  ValidationAttribute{

    public override bool IsValid(object value)
    {

        var flightIds = value as List<string>;

        if(flightIds == null){return false;}
        
        return flightIds.All(flightId => flightId  is string && flightId.Length == 8);

    }

}