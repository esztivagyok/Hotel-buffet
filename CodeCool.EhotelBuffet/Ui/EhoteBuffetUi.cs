using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Model;
using CodeCool.EhotelBuffet.Simulator.Service;

namespace CodeCool.EhotelBuffet.Ui;

public class EhotelBuffetUi
{
    private readonly IReservationManager _reservationManager;
    private readonly IDiningSimulator _diningSimulator;

    private readonly IReservationProvider _reservationProvider;

    public EhotelBuffetUi(
        IReservationProvider reservationProvider,
        IReservationManager reservationManager,
        IDiningSimulator diningSimulator)
    {
        _reservationProvider = reservationProvider;
        _reservationManager = reservationManager;
        _diningSimulator = diningSimulator;
    }

    public void Run()
    {
        int guestCount = 20;
        DateTime seasonStart = DateTime.Today;
        DateTime seasonEnd = DateTime.Today.AddDays(3);

        var guests = GetGuests(guestCount);
        CreateReservations(guests, seasonStart, seasonEnd);

        PrintGuestsWithReservations();

        var currentDate = seasonStart;

        while (currentDate <= seasonEnd)
        {
            var simulatorConfig = new DiningSimulatorConfig(
                currentDate.AddHours(6),
                currentDate.AddHours(10),
                30,
                3);

            var results = _diningSimulator.Run(simulatorConfig);
            PrintSimulationResults(results);
            currentDate = currentDate.AddDays(1);
        }

        Console.ReadLine();
    }

    private IEnumerable<Guest> GetGuests(int guestCount)
    {
        RandomGuestGenerator generator = new RandomGuestGenerator();

        IEnumerable<Guest> guests = generator.Provide(guestCount);

        return guests;
    }

    private void CreateReservations(IEnumerable<Guest> guests, DateTime seasonStart, DateTime seasonEnd)
    {
        foreach (var guest in guests)
        {
            var reservation = _reservationProvider.Provide(guest, seasonStart, seasonEnd);
            _reservationManager.AddReservation(reservation);
        }
    }

    private void PrintGuestsWithReservations()
    {
        Console.WriteLine("Guests with reservations: \n");
        var reservations = _reservationManager.GetAll();
        foreach (var reservation in reservations)
        {
            Console.WriteLine("{0,70}",
                $"{reservation.Guest.Name} \t reservation: \t {reservation.Start} - {reservation.End}");
        }
    }

    private static void PrintSimulationResults(DiningSimulationResults results)
    {
        Console.WriteLine($"\n\t[{results.Date}] " +
                          $"\n\n\t- {results.UnhappyGuests.Count()}\t unhappy guest " +
                          $"\n\t- {results.HappyGuests.Count()}\t happy guest " +
                          $"\n\t- {results.TotalGuests}\t total guests. " +
                          $"\n\t- {results.FoodWasteCost}\t euro waste.\n" +
                          $"\t______________________\n");
    }
}