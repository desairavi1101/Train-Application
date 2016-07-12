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
        private static int NextTicketNo = 1;

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

        public int DeleteTrain(int trainId)
        {
            var ticket = (from t in Tickets.OfType<Ticket>()
                          where t.TrainId == trainId
                          select t).FirstOrDefault();

            if (ticket == default(Ticket))
            {
                return 0;
            }
            Trains.OfType<Train>().ToList().RemoveAll(t => t.Id == trainId);
            return 1;

        }

        public List<ITicket> ViewAllBookedTickets()
        {
            return Tickets;
        }

        public int BookTicket(int noOfPassenger, int trainId, out int totalPrice)
        {
            if (trainId == 0)
            {
                totalPrice = 0;
                return 0;
            }
            int ticketNo = NextTicketNo++;
            ITicket ticket = new Ticket()
            {
                TicketNo = ticketNo,
                TrainId = trainId,
                NoOfPassenger = noOfPassenger
            };

            Tickets.Add(ticket);
            totalPrice = 200 * noOfPassenger;
            return ticketNo;
        }

        public int EditTrain(int trainId, string source, string destination)
        {
            var train = (from t in Trains.OfType<Train>()
                         where t.Id == trainId
                         select t).FirstOrDefault();

            if (train == default(Train))
            {
                return 0 ;
            }

            train.Source = source;
            train.Destination = destination;

            return 1;        
        }

        public int EditTrain(int trainId, string name, string source, string destination)
        {
            var train = (from t in Trains.OfType<Train>()
                         where t.Id == trainId
                         select t).FirstOrDefault();

            if (train == default(Train))
            {
                return 0;
            }

            train.Source = source;
            train.Destination = destination;
            train.Name = name;

            return 1;
        }

    }
}
