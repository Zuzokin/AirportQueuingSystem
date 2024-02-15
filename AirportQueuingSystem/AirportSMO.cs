using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportQueuingSystem
{
    internal class AirportSMO
    {
        public List<Plane> PlanesInUse { get; set; } = new List<Plane>();
        public StatisticManager statisticManager;
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
            planeGenerator = new PlaneGenerator();

            timeUntilNextPlane = UpdateTimeInterval(averageInterval);

        }

        private readonly Random random = new Random();
        private int averageTimeUntilNextPlane = 12;
        private int timeUntilNextPlane;
        private Brigade firstBrigade;
        private Brigade secondBrigade;
        private PlaneGenerator planeGenerator;
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
            return planeGenerator.GenerateRandomNumberInPoissonDistribution(timeInterval);
        }
    }
}
