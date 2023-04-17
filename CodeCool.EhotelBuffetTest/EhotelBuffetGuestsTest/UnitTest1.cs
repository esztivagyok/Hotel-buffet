using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Guests.Service;

namespace CodeCool.EhotelBuffetTest.EhotelBuffetGuestsTest;

public class Tests
{

    private IGuestGroupProvider _guestGroupProvider;
    private IGuestProvider _guestProvider;
    
    
    [SetUp]
    public void Setup()
    {
        _guestProvider = new RandomGuestGenerator();
        _guestGroupProvider = new GuestGroupProvider();

    }

[Test]
public void Test1()
{
    IEnumerable<Guest> guests = _guestProvider.Provide(20);
    IEnumerable<GuestGroup> splitedGroups = _guestGroupProvider.SplitGuestsIntoGroups(guests, 3, 7);

    bool isOkay = false;
    foreach (var splitedGroup in splitedGroups)
    {
        if (splitedGroup.Guests.Count() <= 7)
        {
            isOkay = true;
        }
    }
    
    Assert.IsTrue(isOkay);
}

}