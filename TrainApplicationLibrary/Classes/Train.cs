using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainApplicationLibrary
{
    public class Train : ITrain
    {
        public int Id;
        public string Name;
        public string Source;
        public string Destination;
    }
}
