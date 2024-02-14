using System.Security.Cryptography.X509Certificates;

namespace AirportQueuingSystem
{
    public class StatisticManager
    {
        // todo change getter to get string and multiply by 24
        public double ProbabilityTwoBrigadesWaiting { get; set; } = 0;
        public double AveragePlanesInServiceSystem { get; set; } = 0;
        public double AverageServedPlanes { get; set; } = 0;
        public double AveragePlanesWaitingForService { get; set; } = 0;
        public int FirstBrigadeDowntime { get; set; } = 0;
        public int SecondBrigadeDowntime { get; set; } = 0;
        
        public int HoursCounter { get; set; } = 0;
        public int DaysCounter { get; set; } = 0;

        private int totalNumberOfPlanesPassingThroughSystem = 0;
        private int totalNumberOfPlanesPlanesInServiceSystem = 0;
        private int totalNumberOfPlanesWaitingForService = 0;

        public StatisticManager() { }

        public void UpdateStatistic(int PlanesWaitingForService, int PlanesInServiceSystem)
        {
            var CurrentPlanesInSystem = PlanesWaitingForService + PlanesInServiceSystem;
            HoursCounter++;
            DaysCounter = HoursCounter % 24;
            totalNumberOfPlanesPassingThroughSystem += CurrentPlanesInSystem;
            totalNumberOfPlanesPlanesInServiceSystem += PlanesInServiceSystem;
            totalNumberOfPlanesWaitingForService += PlanesWaitingForService;
        }

        public int CalculateCurrentAirplanesInSystem()
        {
            return 0;
        }

        private void CalculateTwoBrigadesWaitingProbability()
        {

        }

        private void CalculateAveragePlanesInServiceSystem(int currentAirplanesInSystem)
        {

        }


        private void CalculateAverageServedPlanes()
        {

        }

        private void CalculateAveragePlanesWaitingForService()
        {

        }

        private void CalculateServiceDowntime()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
