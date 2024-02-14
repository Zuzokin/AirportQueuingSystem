using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportQueuingSystem
{
    internal class Brigade
    {
        public BrigadeStatus Status { get; set; }
        public BrigadeQueue Queue { get; }
        public Plane ServicedPlane { get; set; }
        public Brigade()
        {
            Status = BrigadeStatus.Waiting;
        }
    }
}
