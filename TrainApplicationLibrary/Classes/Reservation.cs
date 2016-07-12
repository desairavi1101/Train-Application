using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainApplicationLibrary.Classes
{
    public class Reservation : IReservation
    {
        private static int NextTrainId = 1;
        private static int NextTicketId = 1;

        private List<ITrain> Trains;
        private List<ITicket> Tickets;

        public int AddTrain(ITrain train)
        {
            int id = NextTrainId++;
            Train t = train as Train;
            Trains.Add(t);
            t.Id = id;
            return id;
        }


    }
}
