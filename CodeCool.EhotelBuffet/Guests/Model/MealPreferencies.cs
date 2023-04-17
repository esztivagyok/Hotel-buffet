using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Menu.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class MealPreferences
{
    public GuestType GuestType { get; set; }
    public HashSet<MealType> MealTypes { get; set; }

    public MealPreferences(GuestType guestType, HashSet<MealType> mealTypes)
    {
        GuestType = guestType;
        MealTypes = mealTypes;
    }
}