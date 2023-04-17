using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Simulator.Service;

namespace CodeCool.EhotelBuffetTest.RefillTest;

public class RefillServiceTest
{
    private readonly RefillService _refillService = new(timeService: new TimeService());
    
    private readonly Dictionary<MenuItem, int> _testMenu = new()
    {
            { new MenuItem(MealType.ScrambledEggs, 1500, MealDurability.Short), 1 },
            { new MenuItem(MealType.SunnySideUp, 1300, MealDurability.Short), 4 },
            { new MenuItem(MealType.FriedSausage, 1800, MealDurability.Medium), 2 }
        };

   
    [Test]
    public void TestMenuHas7Portions() {
        
        var actualPortions = _refillService.AskForRefill(_testMenu);

        Assert.That(actualPortions.Count(), Is.EqualTo(7));
    }
    
    [Test]
    public void TestMenuHas4SunnySideUp() {
        
        var sunnySideUpCount = _refillService.AskForRefill(_testMenu).Count(portion => portion.MenuItem.MealType == MealType.SunnySideUp);

        Assert.That(sunnySideUpCount, Is.EqualTo(4));
    }
}