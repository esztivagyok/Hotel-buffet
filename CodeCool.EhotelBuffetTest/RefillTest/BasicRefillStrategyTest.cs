using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Refill.Service;

namespace CodeCool.EhotelBuffetTest.RefillTest;

public class BasicRefillStrategyTest
{
    private readonly BasicRefillStrategy _basicRefillStrategy = new();

    private readonly IEnumerable<Portion> _testMenuPortions = new[]
    {
        new Portion(new MenuItem(MealType.ScrambledEggs, 70, MealDurability.Short), DateTime.Now),
        new Portion(new MenuItem(MealType.SunnySideUp, 70, MealDurability.Short), DateTime.Now),
        new Portion(new MenuItem(MealType.SunnySideUp, 70, MealDurability.Short), DateTime.Now),
        new Portion(new MenuItem(MealType.SunnySideUp, 70, MealDurability.Short), DateTime.Now),
        new Portion(new MenuItem(MealType.SunnySideUp, 70, MealDurability.Short), DateTime.Now),
        new Portion(new MenuItem(MealType.FriedSausage, 100, MealDurability.Short), DateTime.Now),
        new Portion(new MenuItem(MealType.FriedSausage, 100, MealDurability.Short), DateTime.Now)
    };

    [Test]
    public void TestMenuPortionsNeeds2MoreScrambledEggs()
    {

        var scrambledEggsNeeded = _basicRefillStrategy.GetRefillQuantities(_testMenuPortions)[new MenuItem(MealType.ScrambledEggs, 70, MealDurability.Short)];

        Assert.That(scrambledEggsNeeded, Is.EqualTo(2));
    }

    [Test]
    public void TestNumberOfMenuItemsInOutput()
    {
        var result = _basicRefillStrategy.GetRefillQuantities(_testMenuPortions);
        
        Assert.That(result, Has.Count.EqualTo(11));
    }

}