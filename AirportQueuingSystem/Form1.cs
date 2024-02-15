using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AirportQueuingSystem
{
    public partial class Form1 : Form
    {
        private int IntervalSimulationTime = 500;
        private int stopTime = 1000;
        private int initialNumberOfPlanes = 120;
        private int averageFirstBrigadeTime = 8;
        private int averageSecondBrigadeTime = 6;
        private int averageIntervalPlanesArrive = 12;
        private StringBuilder logger = new StringBuilder();
        private AirportSMO AirportSMO { get; set; }
        public Form1()
        {
            InitializeComponent();
            Timer timer1 = new Timer();
            timer1.Interval = IntervalSimulationTime;
            timer1.Tick += timer1_Tick;

            AirportSMO = new AirportSMO(initialNumberOfPlanes: initialNumberOfPlanes, averageInterval: averageIntervalPlanesArrive);
        }

        /// <summary>
        /// На каждый тик 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (AirportSMO.statisticManager.HoursCounter == stopTime)
            {
                timer1.Stop();
                return;
            }

            AirportSMO.SimulateTick();
            UpdateOneTickSimulationInfo();
            UpdateStats();
        }
        /// <summary>
        /// Обновить статистику на форме
        /// </summary>
        private void UpdateStats()
        {
            var stats = AirportSMO.statisticManager.GetStats();
            textBox1.Text = stats.Item1;
            textBox2.Text = stats.Item2;
            textBox3.Text = stats.Item3;
            textBox4.Text = stats.Item4;
            textBox5.Text = stats.Item5;
        }

        /// <summary>
        /// Обновляет текстбокс на один клик симуляции
        /// </summary>
        private void UpdateOneTickSimulationInfo()
        {
            richTextBox1.Text = Convert.ToString(logger.AppendLine(
                $"Время:{AirportSMO.statisticManager.HoursCounter} ч -" +
                $"Самолетов в эксплуатации: {AirportSMO.PlanesInUse.Count}" +
                $" Бригада 1: очередь - {AirportSMO.statisticManager.Brigades[0].Queue.NumberOfPlanes()}," +
                $" статус - {AirportSMO.statisticManager.Brigades[0].Status.ToString()}" +
                $" Бригада 2: очередь - {AirportSMO.statisticManager.Brigades[1].Queue.NumberOfPlanes()}," +
                $" статус - {AirportSMO.statisticManager.Brigades[1].Status.ToString()} " +
                $"**************************************************************")
                );
        }

        /// <summary>
        /// Начать симуляцию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            try
            {
                stopTime = Convert.ToInt32(numericUpDown1.Value);
                IntervalSimulationTime = Convert.ToInt32(numericUpDown2.Value);
                timer1.Interval = IntervalSimulationTime;            }
            catch { MessageBox.Show("вы ввели неверное значение времени"); }
        }

        /// <summary>
        /// Остановить симуляцию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopSimulationButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
