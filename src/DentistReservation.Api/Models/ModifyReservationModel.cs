namespace DentistReservation.Api.Models;

public class ModifyReservationModel
{
    public string Description { get; set; }
    public int Number { get; set; }
    public int StartHour { get; set; }
    public int StartMinute { get; set; }
    public int EndHour { get; set; }
    public int EndMinute { get; set; }
    public int AverageDuration { get; set; }
    public int AverageSetupInMinutes { get; set; } 
}