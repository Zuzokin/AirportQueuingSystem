using System;

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
