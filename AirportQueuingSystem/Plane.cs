using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportQueuingSystem
{
    internal class Plane
    {
        public readonly Guid planeId = Guid.NewGuid();
        public PlaneStatus Status { get; set; } = PlaneStatus.InUse;

        public Plane(PlaneStatus Status = PlaneStatus.InUse) 
        {
            this.Status = Status;
        }
    }
}
