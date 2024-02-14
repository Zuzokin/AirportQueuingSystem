using System;
using System.Collections.Generic;
namespace AirportQueuingSystem
{
    internal class Brigade
    {
        // todo сделать behaviur object
        public BrigadeStatus Status { get; set; } = BrigadeStatus.Waiting;
        public readonly int averageServeTime;
        public BrigadeQueue Queue { get; }
        public int TimeUntilAircraftServiced { get; set; } = 0;
        private Plane currentlyServicedPlane; 
        
        public Brigade(int averageServeTime)
        {
            this.averageServeTime = averageServeTime;
            Status = BrigadeStatus.Waiting;
            Queue = new BrigadeQueue();
        }

        public void StartServePlane(Plane plane)
        {
            currentlyServicedPlane = plane;
            Status = BrigadeStatus.Working;
        }

        public Plane StopServePlane()
        {
            var servicedPlane = currentlyServicedPlane;
            currentlyServicedPlane = null;
            Status = BrigadeStatus.Waiting;
            return servicedPlane;
        }
    }
}
