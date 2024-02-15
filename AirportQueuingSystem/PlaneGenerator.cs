using System;

namespace AirportQueuingSystem
{
    internal class PlaneGenerator
    {
        public PlaneGenerator() { }
        public int ArrivingInterval { get; set; }
        public Plane GeneratePlane()
        {
            return new Plane();
        }
        public int GenerateRandomNumberInPoissonDistribution(double lambda)
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

        //public int GenerateRandomNumberInNormalDistribution(double mean, double standardDeviation)
        //{
        //    Random random = new Random();
        //    double u1 = random.NextDouble();
        //    double u2 = random.NextDouble();
        //    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        //    return Convert.ToInt32(mean + standardDeviation * randStdNormal);
        //}
    }
}
