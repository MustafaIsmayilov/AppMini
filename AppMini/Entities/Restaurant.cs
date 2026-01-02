namespace AppMini.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public ICollection<DiningTable> DiningTables { get; set; } = new List<DiningTable>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
