using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Menu.Service;

namespace CodeCool.EhotelBuffet.Refill.Service;

public class BasicRefillStrategy : IRefillStrategy
{
    private const int OptimalPortionCount = 3;
    private MenuProvider _menuProvider;

    IEnumerable<MenuItem> _menuItems = new[]
    {
        new MenuItem(MealType.ScrambledEggs, 70, MealDurability.Short),
        new MenuItem(MealType.SunnySideUp, 70, MealDurability.Short),
        new MenuItem(MealType.FriedSausage, 100, MealDurability.Short),
        new MenuItem(MealType.FriedBacon, 70, MealDurability.Short),
        new MenuItem(MealType.Pancake, 40, MealDurability.Short),
        new MenuItem(MealType.Croissant, 40, MealDurability.Short),
        new MenuItem(MealType.MashedPotato, 20, MealDurability.Medium),
        new MenuItem(MealType.Muffin, 20, MealDurability.Medium),
        new MenuItem(MealType.Bun, 10, MealDurability.Medium),
        new MenuItem(MealType.Cereal, 30, MealDurability.Long),
        new MenuItem(MealType.Milk, 10, MealDurability.Long),
    };


    public Dictionary<MenuItem, int> GetInitialQuantities(IEnumerable<MenuItem> menuItems)
    {
        var ret = new Dictionary<MenuItem, int>();
        foreach (var menuItem in menuItems)
        {
            ret.Add(menuItem, OptimalPortionCount);
        }

        return ret;
    }

    public Dictionary<MenuItem, int> GetRefillQuantities(IEnumerable<Portion> currentPortions)
    {
        var sumOfPortions = SumMenuItemsFromCurrentPortions(currentPortions);

        var allMenuItems = AddMenuItemsOmittedFromCurrentPortions(sumOfPortions);

        return CalculateRefillQuantities(allMenuItems);
    }
    
    
    

    private Dictionary<MenuItem, int> SumMenuItemsFromCurrentPortions(IEnumerable<Portion> currentPortions)
    {
        var sumOfPortions = new Dictionary<MenuItem, int>();

        foreach (var portion in currentPortions)
        {
            if (sumOfPortions.ContainsKey(portion.MenuItem))
            {
                sumOfPortions[portion.MenuItem] += 1;
            }
            else
            {
                sumOfPortions.Add(portion.MenuItem, 1);
            }
        }

        return sumOfPortions;
    }
    

    private Dictionary<MenuItem, int> AddMenuItemsOmittedFromCurrentPortions(Dictionary<MenuItem, int> sumOfPortions)
    {
        if (sumOfPortions.Count == 0)
        {
            sumOfPortions = GetInitialQuantities(_menuItems);
            return sumOfPortions;
        }

        foreach (var menuItem in _menuItems)
        {
            bool containsMenuItem = false;
            foreach (var portion in sumOfPortions)
            {
                if (menuItem == portion.Key)
                {
                    containsMenuItem = true;
                }
            }

            if (!containsMenuItem)
            {
                sumOfPortions.Add(menuItem, OptimalPortionCount);
            }
        }
        return sumOfPortions;
    }
    

    private Dictionary<MenuItem, int> CalculateRefillQuantities(Dictionary<MenuItem, int> sumOfPortions)
    {
        var result = new Dictionary<MenuItem, int>();

        foreach (var item in sumOfPortions)
        {
            switch (item.Value)
            {
                case < OptimalPortionCount:
                    int refillQuantity = OptimalPortionCount - item.Value;
                    result.Add(item.Key, refillQuantity);
                    break;
                case OptimalPortionCount:
                    result.Add(item.Key, OptimalPortionCount);
                    break;
                default:
                    result.Add(item.Key, item.Value);
                    break;
            }
        }

        return result;
    }
}