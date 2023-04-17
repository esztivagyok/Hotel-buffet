using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;


namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationProvider : IReservationProvider
{
    public Reservation Provide(Guest guest, DateTime seasonStart, DateTime seasonEnd)
    {
        Random random = new Random();

        int maximumStay = 7;
        int lastBreakfastHours = 8;
        int seasonDays = seasonEnd.Subtract(seasonStart).Days;
        int randomizedStay = seasonDays<= maximumStay
            ? random.Next(1, seasonDays +1)
            : random.Next(1, maximumStay +1);

        int allowedDelay = seasonDays - randomizedStay;
        int firstDay = random.Next(allowedDelay + 1);

        DateTime start = seasonStart.AddDays(firstDay);
        DateTime end = start.AddDays(randomizedStay).AddHours(lastBreakfastHours);
        
        

        return new Reservation(start, end, guest);
      
    }
}