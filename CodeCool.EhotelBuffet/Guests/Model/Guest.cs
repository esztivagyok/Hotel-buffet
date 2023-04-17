using CodeCool.EhotelBuffet.Guests.Service;

namespace CodeCool.EhotelBuffet.Guests.Model;

public class Guest
{
    public string Name { get; }
    public GuestType GuestType { get; }
    public MealPreferences MealPreferences { get; }

    public Guest(string name, GuestType guestType)
    {
        Name = name;
        GuestType = guestType;
        MealPreferences = GetMealPreferences.getMealPreferences(guestType);
    }
    // public MealType[] MealPreferences { get; } = Array.Empty<MealType>();
}
