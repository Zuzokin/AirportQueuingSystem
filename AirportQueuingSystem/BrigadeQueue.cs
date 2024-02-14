using System.Collections.Generic;

namespace AirportQueuingSystem
{
    internal class BrigadeQueue
    {
        public bool IsEmpty { get; set; } = true;

        private readonly Queue<Plane> queue = new Queue<Plane>();

        public void AddPlane(Plane plane)
        {
            queue.Enqueue(plane);
            IsEmpty = false;            
        }

        public Plane GetPlaneFromQueue()
        {
            var returnPlane = queue.Dequeue();
            if (queue.Count == 0)
            {
                IsEmpty = true;
            }
            return returnPlane;
        }

        public int NumberOfPlanes()
        {
            return queue.Count;
        }
    }
}
