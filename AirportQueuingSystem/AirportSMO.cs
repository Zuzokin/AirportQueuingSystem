using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportQueuingSystem
{
    internal class AirportSMO
    {
        private Random random = new Random();
        private double averageInterval = 12;
        private int timeUntilNextPlane;
        private Brigade firstBrigade;
        private Brigade secondBrigade;
        public List<Plane> PlanesInUse { get; set; }
        public AirportSMO(int initialNumberOfPlanes, double averageInterval = 12)
        {
            this.averageInterval = averageInterval;

            for (int i = 0; i < initialNumberOfPlanes; i++)
            {
                PlanesInUse.Append(new Plane());
            }

            firstBrigade = new Brigade();
            secondBrigade = new Brigade();                 
        }
        public void SimulateTick()
        {
            if (timeUntilNextPlane == 0)
            {
                GetRandomPlane();
            }
        }

        public Plane GetRandomPlane()
        {
            return PlanesInUse.OrderBy(x => random.Next()).First();
        }

        private int GenerateRandomNumberInPoissonDistribution(double lambda)
        {
            Random rnd = new Random();
            double L = Math.Exp(-lambda);
            double p = 1.0;
            int k = 0;

            do
            {
                k++;
                double u = rnd.NextDouble();
                p *= u;
            } while (p >= L);

            return k - 1;
        }

        private int GenerateRandomNumberInNormalDistribution(double mean, double standardDeviation)
        {
            Random random = new Random();
            double u1 = random.NextDouble();
            double u2 = random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return Convert.ToInt32(mean + standardDeviation * randStdNormal);
        }
    }
}
