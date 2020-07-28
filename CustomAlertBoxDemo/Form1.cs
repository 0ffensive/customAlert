using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomAlertBoxDemo;
using CustomAlertBoxDemo.Selenium;
using CustomAlertBoxDemo.Selenium.Core;
using OpenQA.Selenium;

namespace CustomAlertBoxDemo
{
    public partial class Form1 : Form
    {
        private int counter = 0;
        public static string Komunikat;
        public bool Rezerwacja { get; set; }

        public DriverFactory driverFactory;
        public IWebDriver driver;
        private RegistrationPage reg;

        public Form1()
        {
            InitializeComponent();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 5;
            progressBar1.Value = 0;

            driverFactory = new DriverFactory();
            driver = driverFactory.CreateDriver();

            reg = new RegistrationPage(driver);
            if (!driver.Url.Contains("guid"))
            {
                reg.Start("Manchester");
                //reg.Start("Edynburg");
            }

            BackgroundWorker1_DoWork(this, null);
            button2_Click(this, null);
            //timer1_Tick(this, null);
            //
            timer1.Interval = 20 * 1000;
            timer1.Enabled = true;
            timer1.Start();
        }
        ~Form1()
        {
            driver.Close();
            driver.Dispose();
            driverFactory.Dispose();
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //button1_Click(sender, e);
            button2_Click(sender, e);
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

            if (progressBar1.Maximum == progressBar1.Value)
            {
                progressBar1.Value = progressBar1.Minimum;
            }
            progressBar1.Value++;

            textBox1.Text += $"{Komunikat}";

            //Scraper scraper = new Scraper();
            //string result = await scraper.ScrapeWebsite();
        }

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
            backgroundWorker1.ReportProgress(counter++);
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string txt = "Blad";
            try
            {
                reg.ReloadCurrentPage();
                txt = reg.KomunikatSpan.Text;

                bool exists = reg.DniDropDown.Exists();
                if (exists)
                {
                    timer1.Enabled = false;
                    SystemSounds.Exclamation.Play();
                    txt = $"REZERWACJA";
                    reg.Rezerwuj();
                }
            }
            catch (Exception exception)
            {
                txt = exception.ToString().Substring(55);
            }
            finally
            {
                string time = DateTime.Now.ToShortTimeString();
                Komunikat = $"{time}: {txt}\r\n";
            }
            
        }
    }
}
