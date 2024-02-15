using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportQueuingSystem
{
    internal class AirportSMO
    {
        private readonly Random random = new Random();
        private int averageTimeUntilNextPlane = 12;
        private int timeUntilNextPlane;
        private Brigade firstBrigade;
        private Brigade secondBrigade;
        private StatisticManager statisticManager;
        private PlaneGenerator planeGenerator;
        private int HoursCounter;
        public List<Plane> PlanesInUse { get; set; } = new List<Plane>();
        public AirportSMO(int initialNumberOfPlanes, int averageInterval = 12)
        {
            averageTimeUntilNextPlane = averageInterval;

            for (int i = 0; i < initialNumberOfPlanes; i++)
            {
                PlanesInUse.Add(new Plane());
            }

            firstBrigade = new Brigade(averageServeTime: 8);
            secondBrigade = new Brigade(averageServeTime: 6);

            var brigades = new List<Brigade>
            {
                firstBrigade,
                secondBrigade
            };

            statisticManager = new StatisticManager(brigades);

            timeUntilNextPlane = UpdateTimeInterval(averageInterval);
        }
        public void SimulateTick()
        {           
            //todo добавить клапан с логикой проверок
            if (timeUntilNextPlane == 0)
            {
                firstBrigade.Queue.AddPlane(GetRandomPlane());
                timeUntilNextPlane = UpdateTimeInterval(averageTimeUntilNextPlane);
            }

            if (firstBrigade.Status == BrigadeStatus.Working && firstBrigade.TimeUntilAircraftServiced == 0)
            {
                secondBrigade.Queue.AddPlane(firstBrigade.StopServePlane());
                statisticManager.totalNumberOfServedPlanes++;
            }
            if (firstBrigade.Status == BrigadeStatus.Waiting && !firstBrigade.Queue.IsEmpty)
            {
                firstBrigade.StartServePlane(firstBrigade.Queue.GetPlaneFromQueue());
                firstBrigade.TimeUntilAircraftServiced = UpdateTimeInterval(firstBrigade.averageServeTime);
            }

            if (secondBrigade.Status == BrigadeStatus.Working && secondBrigade.TimeUntilAircraftServiced == 0)
            {
                PlanesInUse.Add(secondBrigade.StopServePlane());
                statisticManager.totalNumberOfServedPlanes++;
            }
            if (secondBrigade.Status == BrigadeStatus.Waiting && !secondBrigade.Queue.IsEmpty)
            {
                secondBrigade.StartServePlane(secondBrigade.Queue.GetPlaneFromQueue());
                secondBrigade.TimeUntilAircraftServiced = UpdateTimeInterval(secondBrigade.averageServeTime);
            }

            statisticManager.UpdateStatistic();
            UpdateTime();
        }

        private Plane GetRandomPlane()
        {
            var randomPlane = PlanesInUse.OrderBy(x => random.Next()).First();
            PlanesInUse.Remove(randomPlane);
            return randomPlane;
        }

        private void UpdateTime()
        {
            timeUntilNextPlane--;
            firstBrigade.TimeUntilAircraftServiced--;
            secondBrigade.TimeUntilAircraftServiced--;
        }


        private int UpdateTimeInterval(int timeInterval)
        {
            return GenerateRandomNumberInPoissonDistribution(timeInterval);
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

        //private int GenerateRandomNumberInNormalDistribution(double mean, double standardDeviation)
        //{
        //    Random random = new Random();
        //    double u1 = random.NextDouble();
        //    double u2 = random.NextDouble();
        //    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        //    return Convert.ToInt32(mean + standardDeviation * randStdNormal);
        //}
    }
}
