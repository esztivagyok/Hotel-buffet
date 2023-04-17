using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class RandomGuestGenerator : IGuestProvider
{
    private static readonly Random Random = new();

    private static readonly string[] Names =
    {
        "Georgia", "Savannah", "Phoenix", "Winona", "Carol", "Brooklyn", "Talullah", "Scarlett", "Ruby", "Lola",
        "Cleo", "Beatrix", "Mika", "Everly", "Kiki", "Rae", "Arya", "Elsa", "Lulu", "Zelda",
        "Felix", "Finn", "Theo", "Hugo", "Archie", "Magnus", "Lucian", "Enzo", "Otto", "Nico", "Rhys",
        "Rupert", "Hugh", "Finley", "Ralph", "Lewis", "Wilbur", "Alfie", "Ernest", "Chester", "Ziggy"
    };

    public IEnumerable<Guest> Provide(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            yield return GenerateRandomGuest();
        }
    }

    private static Guest GenerateRandomGuest()
    {
        return new Guest(GetRandomName(), GetRandomType());
    }

    private static string GetRandomName()
    {
        int randomIndex = Random.Next(0, Names.Length);
        string randomName = Names[randomIndex];
        return randomName;
    }

    private static GuestType GetRandomType()
    {
        int randomIndex = Random.Next(0, Enum.GetNames(typeof(GuestType)).Length);
        GuestType randomGuestType = (GuestType)randomIndex;

        return randomGuestType;
    }
}
