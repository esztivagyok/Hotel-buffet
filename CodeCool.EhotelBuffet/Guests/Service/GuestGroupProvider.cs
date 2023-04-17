using CodeCool.EhotelBuffet.Guests.Model;

namespace CodeCool.EhotelBuffet.Guests.Service;

public class GuestGroupProvider : IGuestGroupProvider
{
    private readonly Random _random = new Random();

    public IEnumerable<GuestGroup> SplitGuestsIntoGroups(IEnumerable<Guest> guests, int groupCount,
        int maxGuestPerGroup)
    {
        //Létrehozunk egy nested listet
        var groups = new List<List<Guest>>();

        for (int i = 0; i < groupCount; i++)
        {
            //Belepakolunk a fent létrehozot listába annyi listát amennyi a groupCount
            groups.Add(new List<Guest>());
        }

        //Átalakítjuk a IEnumerable<Guest> guests-et listává és csinálunk róla egy másolatot

        var guestList = guests.ToList();

        //Kimentjük egy változóba a vendégek számát

        int guestsLeft = guestList.Count;

        while (guestsLeft > 0)
        {
            if (guestsLeft != guests.Count() - (maxGuestPerGroup * groupCount))
            {
                foreach (var group in groups)
                {
                    if (group.Count <= maxGuestPerGroup && guestsLeft > 0)
                    {
                        int guestIndex = _random.Next(0, guestsLeft);
                        group.Add(guestList[guestIndex]);
                        guestList.RemoveAt(guestIndex);
                        guestsLeft--;

                        break;
                    }
                }
            }
            else
            {
                guestsLeft = 0;
            }
        }

        //A select metódus a group listából egy IEnumerable<T> hoz létre,
        //A groups összes elemére meghívja a newGuestGroup konstruktort
        return groups.Select(guestsList => new GuestGroup(0, guestsList));
    }
}