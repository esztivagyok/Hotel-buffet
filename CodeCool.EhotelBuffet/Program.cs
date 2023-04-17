using CodeCool.EhotelBuffet.Buffet.Service;
using CodeCool.EhotelBuffet.Guests.Service;
using CodeCool.EhotelBuffet.Menu.Service;
using CodeCool.EhotelBuffet.Refill.Service;
using CodeCool.EhotelBuffet.Reservations.Service;
using CodeCool.EhotelBuffet.Simulator.Service;
using CodeCool.EhotelBuffet.Ui;

ITimeService timeService = new TimeService();
IMenuProvider menuProvider = new MenuProvider();
IRefillService refillService = new RefillService(timeService);
IGuestGroupProvider guestGroupProvider = new GuestGroupProvider();
IReservationProvider reservationProvider = new ReservationProvider();
IReservationManager reservationManager = new ReservationManager();

IBuffetService buffetService = new BuffetService(menuProvider, refillService);
IDiningSimulator diningSimulator =
    new BreakfastSimulator(buffetService, reservationManager, guestGroupProvider, timeService);

EhotelBuffetUi ui = new EhotelBuffetUi(reservationProvider, reservationManager, diningSimulator);

ui.Run();
