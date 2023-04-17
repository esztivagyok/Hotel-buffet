using CodeCool.EhotelBuffet.Menu.Model;
using CodeCool.EhotelBuffet.Simulator.Service;

namespace CodeCool.EhotelBuffet.Refill.Service;

public class RefillService : IRefillService
{
    private readonly ITimeService _timeService;

    public RefillService(ITimeService timeService)
    {
        _timeService = timeService;
    }
    public IEnumerable<Portion> AskForRefill(Dictionary<MenuItem, int> menuItemToPortions)
    {
        List<Portion> output = new List<Portion>();

        foreach (var item in menuItemToPortions)
        {
            int counter = item.Value;
            while (counter > 0)
            {
                Portion portion = new Portion(item.Key, _timeService.GetCurrentTime());
                output.Add(portion);
                counter--;
            }
        }

        return output.AsEnumerable();
    }
}