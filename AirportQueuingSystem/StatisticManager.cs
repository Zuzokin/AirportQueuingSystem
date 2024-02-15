using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AirportQueuingSystem
{
    internal class StatisticManager
    {
        // todo change getter to get string and multiply by 24
        public double TwoBrigadesWaitingProbability { get; set; } = 0;
        public double AveragePlanesInServiceSystem { get; set; } = 0;
        public double AverageServedPlanes { get; set; } = 0;
        public double AveragePlanesWaitingForService { get; set; } = 0;
        public double ServiceDayDowntime { get; set; } = 0;

        public double HoursCounter { get; set; } = 0;
        public double DaysCounter { get; set; } = 0;
        public double totalNumberOfServedPlanes { get; set; }

        private int TotalHoursTwoBrigadesWaiting = 0;
        private int brigadesDowntime = 0;
        private int totalNumberOfPlanesPassingThroughSystem = 0;
        private int totalNumberOfPlanesWaitingForService = 0;
        public List<Brigade> Brigades;

        public StatisticManager(List<Brigade> brigades)
        {
            Brigades = brigades;
        }

        public (string, string, string, string, string) GetStats()
        {
            return (
                Math.Round(TwoBrigadesWaitingProbability, 3).ToString(),
                Math.Round(AveragePlanesInServiceSystem, 3).ToString(),
                Math.Round(AverageServedPlanes, 3).ToString(),
                Math.Round(AveragePlanesWaitingForService, 3).ToString(),
                Math.Round(ServiceDayDowntime, 3).ToString()
                );
        }


        public void UpdateStatistic()
        {
            var PlanesWaitingForService = Brigades[0].Queue.NumberOfPlanes() + Brigades[1].Queue.NumberOfPlanes();
            var planesInServiceSystem = Brigades.Count(b => b.Status == BrigadeStatus.Working);
            var CurrentPlanesInSystem = PlanesWaitingForService + planesInServiceSystem;
            var isBrigadesWaiting = Brigades.Count(b => b.Status == BrigadeStatus.Waiting) == 0;
            if (isBrigadesWaiting)
            {
                TotalHoursTwoBrigadesWaiting++;
            }
            brigadesDowntime += Brigades.Count(b => b.Status == BrigadeStatus.Waiting);

            totalNumberOfPlanesPassingThroughSystem += CurrentPlanesInSystem;
            totalNumberOfPlanesWaitingForService += PlanesWaitingForService;

            HoursCounter++;
            DaysCounter = HoursCounter / 24;

            TwoBrigadesWaitingProbability = CalculateTwoBrigadesWaitingProbability();
            AveragePlanesInServiceSystem = CalculateAveragePlanesInServiceSystem();
            AverageServedPlanes = CalculateAverageServedPlanes();
            AveragePlanesWaitingForService = CalculateAveragePlanesWaitingForService();
            ServiceDayDowntime = CalculateServiceDowntime();
        }

        /// <summary>
        /// Определить вероятность того, что обе бригады будут свободны от обслуживания
        /// </summary>
        private double CalculateTwoBrigadesWaitingProbability()
        {
            try { return TotalHoursTwoBrigadesWaiting / HoursCounter; }
            catch { return TwoBrigadesWaitingProbability; }
        }

        /// <summary>
        /// среднее число самолетов, находящихся в системе обслуживания
        /// </summary>
        /// <returns></returns>
        private double CalculateAveragePlanesInServiceSystem()
        {
            try { return totalNumberOfPlanesPassingThroughSystem / HoursCounter; }
            catch { return AveragePlanesInServiceSystem; }
        }

        /// <summary>
        /// среднее число самолетов, обслуживаемых обеими бригадами в сутки
        /// </summary>
        /// <returns></returns>
        private double CalculateAverageServedPlanes()
        {
            try { return totalNumberOfServedPlanes / DaysCounter; }
            catch (Exception) { return AverageServedPlanes; }
        }

        /// <summary>
        /// среднее число самолетов, ожидающих обслуживания
        /// </summary>
        /// <returns></returns>
        private double CalculateAveragePlanesWaitingForService()
        {
            try { return totalNumberOfPlanesWaitingForService / HoursCounter; }
            catch (Exception) { return AveragePlanesWaitingForService; }
        }

        /// <summary>
        /// время простоя первой и второй бригад в течение суток
        /// </summary>
        private double CalculateServiceDowntime()
        {
            try { return brigadesDowntime / DaysCounter; }
            catch (Exception) { return ServiceDayDowntime; }
        }

    }
}
