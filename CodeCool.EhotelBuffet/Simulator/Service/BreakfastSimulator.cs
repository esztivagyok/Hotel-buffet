using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Model;

namespace CodeCool.EhotelBuffet.Simulator.Service;

public class BreakfastSimulator : IDiningSimulator
{
    private static readonly Random Random = new();

    private readonly IBuffetService _buffetService;
    private readonly IReservationManager _reservationManager;
    private readonly IGuestGroupProvider _guestGroupProvider;
    private readonly ITimeService _timeService;

    private readonly List<Guest> _happyGuests = new();
    private readonly List<Guest> _unhappyGuests = new();

    private int _foodWasteCost;

    public BreakfastSimulator(
        IBuffetService buffetService,
        IReservationManager reservationManager,
        IGuestGroupProvider guestGroupProvider,
        ITimeService timeService)
    {
        _buffetService = buffetService;
        _reservationManager = reservationManager;
        _guestGroupProvider = guestGroupProvider;
        _timeService = timeService;
    }

    public DiningSimulationResults Run(DiningSimulatorConfig config)
    {
        ResetState();
        
        _timeService.SetCurrentTime(config.Start);
        
        var guests = _reservationManager.GetGuestsForDate(config.Start);
        var maxGuestPerGroup = (int)Math.Ceiling((double)(guests.Count() / config.MinimumGroupCount));

        IRefillStrategy refillStrategy = new BasicRefillStrategy();

        var guestGroups = _guestGroupProvider.SplitGuestsIntoGroups(guests, config.MinimumGroupCount, maxGuestPerGroup);

        foreach (GuestGroup guestGroup in guestGroups)
        {
            _buffetService.Refill(refillStrategy);

            ServeGuests(guestGroup);

            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Short, _timeService.GetCurrentTime());
            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Medium, _timeService.GetCurrentTime());
            _foodWasteCost += _buffetService.CollectWaste(MealDurability.Long, _timeService.GetCurrentTime());
            
            _timeService.IncreaseCurrentTime(config.CycleLengthInMinutes);
        }

        var diningSimulationResult = new DiningSimulationResults(_timeService.GetCurrentTime(), guests.Count(), _foodWasteCost, _happyGuests, _unhappyGuests);

        return diningSimulationResult;
    }

    private void ResetState()
    {
        _foodWasteCost = 0;
        _happyGuests.Clear();
        _unhappyGuests.Clear();
        _buffetService.Reset();
    }

    private void ServeGuests(GuestGroup guestGroup)
    {
        foreach (Guest guest in guestGroup.Guests)
        {
            CategorizeGuest(CheckHappiness(guest), guest);
        }
    }

    private bool CheckHappiness(Guest guest)
    {
        var guestPreferredMealTypes = guest.MealPreferences.MealTypes.ToList();
        var randomMealTypeIndex = Random.Next(guestPreferredMealTypes.Count - 1);
        var guestAtePreferredFood = _buffetService.Consume(guestPreferredMealTypes[randomMealTypeIndex]);
        return guestAtePreferredFood;
    }

    private void CategorizeGuest(bool guestAtePreferredFood, Guest guest)
    {
        if (guestAtePreferredFood)
        {
            _happyGuests.Add(guest);
        }
        else
        {
            _unhappyGuests.Add(guest);
        }
    }
}
