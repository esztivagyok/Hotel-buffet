using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;

namespace CodeCool.EhotelBuffet.Buffet.Service;

public class BuffetService : IBuffetService
{
    private readonly IMenuProvider _menuProvider;
    private readonly IRefillService _refillService;
    private readonly List<Portion> _currentPortions = new();

    private bool _isInitialized;

    public BuffetService(IMenuProvider menuProvider, IRefillService refillService)
    {
        _menuProvider = menuProvider;
        _refillService = refillService;
    }

    public void Refill(IRefillStrategy refillStrategy)
    {
        var menu = refillStrategy.GetRefillQuantities(_currentPortions);

        IEnumerable<Portion> portions = _refillService.AskForRefill(menu);

        foreach (var portion in portions)
        {
            _currentPortions.Add(portion);
        }
    }

    public void Reset()
    {
        while (_currentPortions.Count != 0)
        {
            _currentPortions.RemoveAt(0);
        }
    }

    public bool Consume(MealType mealType)
    {
        var freshestPortion = _currentPortions
            .Where(portion => portion.MenuItem.MealType == mealType)
            .MaxBy(portion => portion.TimeStamp);

        if (freshestPortion == null) return false;

        _currentPortions.Remove(freshestPortion);

        return true;
    }

    public int CollectWaste(MealDurability mealDurability, DateTime currentDate)
    {
        int wastePrice = 0;

        for (int i = 0; i < _currentPortions.Count; i++)
        {
            if (_currentPortions[i].MenuItem.MealDurability == mealDurability
                && _currentPortions[i].TimeStamp.AddMinutes(_currentPortions[i].MenuItem.MealDurabilityInMinutes) >=
                currentDate)
            {
                wastePrice += _currentPortions[i].MenuItem.Cost;
                _currentPortions.RemoveAt(i);
            }
        }

        return wastePrice;
    }
}