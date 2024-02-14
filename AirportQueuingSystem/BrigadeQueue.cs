using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportQueuingSystem
{
    internal class BrigadeQueue
    {
        private bool isEmpty = true;

        private readonly Queue<Plane> queue;

        public void AddPlane(Plane plane)
        {
            queue.Enqueue(plane);
            isEmpty = false;
        }

        public Plane GetPlaneFromQueue()
        {
            var returnPlane = queue.Dequeue();
            if (queue.Count == 0)
            {
                isEmpty = true;
            }
            return returnPlane;
        }
    }
}
