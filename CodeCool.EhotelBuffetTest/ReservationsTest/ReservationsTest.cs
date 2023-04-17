using CodeCool.EhotelBuffet.Guests.Model;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Reservations.Model;


namespace CodeCool.EhotelBuffetTest.ReservationsTest;

public class ReservationsTests
{
    private IReservationProvider _reservationProvider;
    private IReservationManager _reservationManager;
    
    [SetUp]
    public void Setup()
    {
        _reservationProvider = new ReservationProvider();
        _reservationManager = new ReservationManager();
    }

    [Test]
    public void TestReservationStartDate_ToBeGreaterThanSeasonStart()
    {
        //arrange
        Guest guest = new Guest("Joe", GuestType.Business);
        var seasonStart = new DateTime(2022, 5, 1);
        var seasonEnd = new DateTime(2022, 5, 20);
        //act
        Reservation reservation = _reservationProvider.Provide(guest, seasonStart, seasonEnd);
        //assert
        Assert.That(reservation.Start, Is.GreaterThan(seasonStart));
    }

    [Test]

    public void TestReservationEndDate_ToBeLessThanSeasonEnd()
    {
        //arrange
        Guest guest = new Guest("Bill", GuestType.Kid);
        var seasonStart = new DateTime(2022, 1, 1);
        var seasonEnd = new DateTime(2022, 1, 5);
        //act
        Reservation reservation = _reservationProvider.Provide(guest, seasonStart, seasonEnd);
        //assert
        Assert.That(reservation.End, Is.LessThanOrEqualTo(seasonEnd));
    }
    
    [Test]

    public void TestReservationLength_ToBeLessThanAWeek()
    {
        //arrange
        Guest guest = new Guest("Bill", GuestType.Kid);
        var seasonStart = new DateTime(2022, 1, 1);
        var seasonEnd = new DateTime(2022, 1, 5);
        //act
        Reservation reservation = _reservationProvider.Provide(guest, seasonStart, seasonEnd);
        var reservationLength = reservation.End - reservation.Start;
        //assert
        Assert.That(reservationLength.Days, Is.LessThanOrEqualTo(7));
    }

    [Test]

    public void TestReservationList_AfterAddingReservation()
    {
        //arrange
        Guest guest = new Guest("Jane", GuestType.Tourist);
        var start = new DateTime(2022, 1, 1);
        var end = new DateTime(2022, 1, 5);
        //act
        var reservation = _reservationProvider.Provide(guest, start, end);
        _reservationManager.AddReservation(reservation);
        IEnumerable<Reservation> list = _reservationManager.GetAll();
        //assert
        Assert.That(list.Count(), Is.EqualTo(1));
    }
}


