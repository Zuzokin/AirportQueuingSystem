using System;
using System.Windows.Forms;

namespace AirportQueuingSystem
{
    public partial class Form1 : Form
    {
        
        private AirportSMO AirportSMO { get; set; }
        public Form1()
        {
            InitializeComponent();
            Timer timer1 = new Timer();
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            AirportSMO = new AirportSMO(initialNumberOfPlanes: 120, averageInterval: 12);
        }

        public void StartSimulation()
        {

        }

        /// <summary>
        /// На каждый тик 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            AirportSMO.SimulateTick();
        }

        /// <summary>
        /// Начать симуляцию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();


        }
    }
}
