using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Model;


namespace CodeCool.EhotelBuffet.Reservations.Service;

public class ReservationManager : IReservationManager
{
    private readonly List<Reservation> _reservations = new List<Reservation>();

    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }

    public IEnumerable<Reservation> GetAll()
    {
        return _reservations;
    }
    public IEnumerable<Guest> GetGuestsForDate(DateTime date)
    {
        List<Guest> guests = new List<Guest>();
        foreach (var reservation in _reservations)
        {
            if (DateTime.Compare(reservation.Start, date) <= 0 && DateTime.Compare(reservation.End, date) >= 0)
            {
                guests.Add(reservation.Guest);
            }
        }

        return guests;
    }
}