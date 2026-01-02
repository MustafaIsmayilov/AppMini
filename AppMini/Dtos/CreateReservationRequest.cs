namespace AppMini.Dtos;

public class CreateReservationRequest
{
    public int RestaurantId { get; set; }
    public int DiningTableId { get; set; }
    public string CustomerName { get; set; } = null!;
    public int GuestCount { get; set; }
    public DateTime ReservationDate { get; set; }
}
