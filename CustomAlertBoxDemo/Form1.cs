using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomAlertBoxDemo;

namespace CustomAlertBoxDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            timer1.Interval = 3 * 1000;
            timer1.Enabled = true;
            timer1.Start();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 20;
            progressBar1.Value = 0;
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            button1_Click(sender, e);
        }

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg,type);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Alert("Success Alert",Form_Alert.enmType.Success);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //this.Alert("Warning Alert", Form_Alert.enmType.Warning);

            progressBar1.Value++;

            //ScrapeSite();
            Scraper scraper = new Scraper();
            string result = await scraper.ScrapeWebsite();
            textBox1.Text += result;
        }

        //private async Task ScrapeSite()
        //{
            
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            this.Alert("Error Alert", Form_Alert.enmType.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Alert("Info Alert", Form_Alert.enmType.Info);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(backgroundWorker1.IsBusy)
                return;

            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.ReportProgress(1);
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(5 * 1000);
            
        }
    }
}
