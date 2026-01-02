namespace AppMini.Entities;

public class Reservation
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public int DiningTableId { get; set; }
    public DiningTable DiningTable { get; set; } = null!;

    public string CustomerName { get; set; } = null!;
    public int GuestCount { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime CreatedAt { get; set; }
}