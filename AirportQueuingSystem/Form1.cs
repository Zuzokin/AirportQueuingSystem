using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirportQueuingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Timer timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            AirportSMO airportSMO = new AirportSMO(initialNumberOfPlanes: 120, averageInterval: 0.5);
            Brigade firstBrigade = new Brigade();
            Brigade secondBrigade = new Brigade();
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
