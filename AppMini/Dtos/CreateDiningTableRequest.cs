namespace AppMini.Dtos;

public class CreateDiningTableRequest
{
    public int RestaurantId { get; set; }
    public int DiningTableNumber { get; set; }
    public int Capacity { get; set; }
}