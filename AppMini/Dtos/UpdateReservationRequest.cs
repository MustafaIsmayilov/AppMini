namespace AppMini.Dtos;

public class UpdateReservationRequest
{
    public int Id { get; set; }
    public int GuestCount { get; set; }
    public DateTime ReservationDate { get; set; }
}
