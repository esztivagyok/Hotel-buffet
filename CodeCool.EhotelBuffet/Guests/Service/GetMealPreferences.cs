using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Guests.Model;
namespace CodeCool.EhotelBuffet.Guests.Service;

public static class GetMealPreferences
{
    public static MealPreferences getMealPreferences(GuestType GuestType)
    {
        switch (GuestType)
        {
            case GuestType.Business:
                return new MealPreferences(GuestType.Business,
                    new HashSet<MealType>()
                    {
                        MealType.ScrambledEggs, 
                        MealType.FriedBacon, 
                        MealType.Croissant
                    });
            case GuestType.Tourist:
                return new MealPreferences(GuestType.Tourist,
                    new HashSet<MealType>()
                    {
                        MealType.SunnySideUp, 
                        MealType.FriedSausage, 
                        MealType.MashedPotato, 
                        MealType.Bun, 
                        MealType.Muffin
                    });
            case GuestType.Kid:
                return new MealPreferences(GuestType.Kid,
                    new HashSet<MealType>()
                    {
                        MealType.Pancake, 
                        MealType.Cereal, 
                        MealType.Muffin, 
                        MealType.Milk
                    });
            default:
                return null;
        }
    }
}