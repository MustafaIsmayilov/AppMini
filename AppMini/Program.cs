using AppMini.Data;
using AppMini.Entities;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        using var db = new AppMiniDbContext();
        db.Database.Migrate();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("=== MENU ===");
            Console.WriteLine("1. Add Restaurant");
            Console.WriteLine("2. List Restaurants");
            Console.WriteLine("3. Delete Restaurant");
            Console.WriteLine("4. Add DiningTable to Restaurant");
            Console.WriteLine("5. List DiningTables by Restaurant");
            Console.WriteLine("6. Create Reservation");
            Console.WriteLine("7. List Reservations by Restaurant");
            Console.WriteLine("8. Update Reservation Date");
            Console.WriteLine("0. Exit");
            Console.Write("Choose option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddRestaurant(db); break;
                case "2": ListRestaurants(db); break;
                case "3": DeleteRestaurant(db); break;
                case "4": AddDiningTable(db); break;
                case "5": ListDiningTables(db); break;
                case "6": CreateReservation(db); break;
                case "7": ListReservations(db); break;
                case "8": UpdateReservationDate(db); break;
                case "0": exit = true; break;
                default: Console.WriteLine("Invalid option"); break;
            }

            Console.WriteLine();
        }
    }

    static void AddRestaurant(AppMiniDbContext db)
    {
        Console.Write("Name: ");
        var name = Console.ReadLine()!;
        Console.Write("City: ");
        var city = Console.ReadLine()!;

        var restaurant = new Restaurant
        {
            Name = name,
            City = city,
            CreatedAt = DateTime.UtcNow
        };

        db.Restaurants.Add(restaurant);
        db.SaveChanges();
        Console.WriteLine("Restaurant added!");
    }

    static void ListRestaurants(AppMiniDbContext db)
    {
        var restaurants = db.Restaurants.ToList();
        foreach (var r in restaurants)
        {
            Console.WriteLine($"Id: {r.Id}, Name: {r.Name}, City: {r.City}");
        }
    }

    static void DeleteRestaurant(AppMiniDbContext db)
    {
        Console.Write("Restaurant Id to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var restaurant = db.Restaurants.Find(id);
            if (restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
                db.SaveChanges();
                Console.WriteLine("Deleted!");
            }
            else Console.WriteLine("Restaurant not found");
        }
    }

    static void AddDiningTable(AppMiniDbContext db)
    {
        Console.Write("Restaurant Id: ");
        if (!int.TryParse(Console.ReadLine(), out int restId)) return;

        var restaurant = db.Restaurants.Include(r => r.DiningTables).FirstOrDefault(r => r.Id == restId);
        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found");
            return;
        }

        Console.Write("DiningTable Number: ");
        int tableNumber = int.Parse(Console.ReadLine()!);
        Console.Write("Capacity: ");
        int capacity = int.Parse(Console.ReadLine()!);

        var table = new DiningTable
        {
            DiningTableNumber = tableNumber,
            Capacity = capacity,
            RestaurantId = restId,
            IsActive = true
        };

        db.DiningTables.Add(table);
        db.SaveChanges();
        Console.WriteLine("DiningTable added!");
    }

    static void ListDiningTables(AppMiniDbContext db)
    {
        Console.Write("Restaurant Id: ");
        if (!int.TryParse(Console.ReadLine(), out int restId)) return;

        var tables = db.DiningTables.Where(t => t.RestaurantId == restId).ToList();
        foreach (var t in tables)
        {
            Console.WriteLine($"Id: {t.Id}, Number: {t.DiningTableNumber}, Capacity: {t.Capacity}, Active: {t.IsActive}");
        }
    }

    static void CreateReservation(AppMiniDbContext db)
    {
        Console.Write("Restaurant Id: ");
        int restId = int.Parse(Console.ReadLine()!);

        var restaurant = db.Restaurants
            .Include(r => r.Reservations)
            .FirstOrDefault(r => r.Id == restId);

        if (restaurant == null)
        {
            Console.WriteLine("Restaurant not found");
            return;
        }

        Console.Write("DiningTable Id: ");
        int tableId = int.Parse(Console.ReadLine()!);

        Console.Write("Customer Name: ");
        string customer = Console.ReadLine()!;

        Console.Write("Guest Count: ");
        int guests = int.Parse(Console.ReadLine()!);

        Console.Write("Reservation Date (yyyy-MM-dd HH:mm): ");
        DateTime date = DateTime.Parse(Console.ReadLine()!)
                                  .ToUniversalTime();

        var reservation = new Reservation
        {
            RestaurantId = restId,
            DiningTableId = tableId,
            CustomerName = customer,
            GuestCount = guests,
            ReservationDate = date,
            CreatedAt = DateTime.UtcNow
        };

        db.Reservations.Add(reservation);
        db.SaveChanges();

        Console.WriteLine("Reservation created!");
    }


    static void ListReservations(AppMiniDbContext db)
    {
        Console.Write("Restaurant Id: ");
        int restId = int.Parse(Console.ReadLine()!);

        var reservations = db.Reservations
            .Where(r => r.RestaurantId == restId)
            .Include(r => r.DiningTable)
            .ToList();

        foreach (var r in reservations)
        {
            Console.WriteLine($"Id: {r.Id}, Table: {r.DiningTable?.DiningTableNumber}, Customer: {r.CustomerName}, Guests: {r.GuestCount}, Date: {r.ReservationDate}");
        }
    }

    static void UpdateReservationDate(AppMiniDbContext db)
    {
        Console.Write("Reservation Id: ");
        int id = int.Parse(Console.ReadLine()!);

        var reservation = db.Reservations.Find(id);
        if (reservation == null)
        {
            Console.WriteLine("Reservation not found");
            return;
        }

        Console.Write("New Date (yyyy-MM-dd HH:mm): ");
        reservation.ReservationDate =DateTime.Parse(Console.ReadLine()!).ToUniversalTime();


        db.SaveChanges();
        Console.WriteLine("Reservation updated!");
    }
}
