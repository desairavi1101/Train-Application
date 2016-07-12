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

        public List<ITrain> ViewTrains(string source,string destination) {
            var trains = from t in Trains.OfType<Train>()
                         where t.Source == source && t.Destination == destination
                         select t;
            return trains.OfType<ITrain>().ToList();
        }

        public int SearchTrains(string source, string destination, int noOfPassenger)
        {
            var trains = from t in Trains.OfType<Train>()
                         where t.Source == source && t.Destination == destination
                         select t;
            foreach (Train t in trains)
            {
                var train = (from ticket in Tickets.OfType<Ticket>()
                                    where ticket.TrainId == t.Id
                                    group ticket by ticket.TrainId into g 
                                    select new {TrainId = g.Key , RemainingSeates = 200 - g.Sum(ta => ta.NoOfPassenger) }).FirstOrDefault();

                if (train.RemainingSeates >= noOfPassenger)
                {
                    return train.TrainId;
                }
            }
            return 0;

        }






    }
}
