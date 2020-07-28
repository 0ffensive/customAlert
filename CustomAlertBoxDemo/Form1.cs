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
            this.Location = Screen.AllScreens[0].WorkingArea.Location;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            this.backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 5;
            progressBar1.Value = 0;

            timer1.Interval = 20 * 1000;
            timer1.Enabled = true;
            timer1.Start();

            driverFactory = new DriverFactory();
            driver = driverFactory.CreateDriver();

            reg = new RegistrationPage(driver);
            if (!driver.Url.Contains("guid"))
            {
                reg.Start("Manchester");
                //reg.Start("Edynburg");
            }

            this.backgroundWorker1.RunWorkerAsync();
        }
        ~Form1()
        {
            driver.Close();
            driver.Dispose();
            driverFactory.Dispose();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do not access the form's BackgroundWorker reference directly.
            // Instead, use the reference provided by the sender parameter.
            BackgroundWorker bw = sender as BackgroundWorker;

            // Extract the argument.
            //int arg = (int)e.Argument;

            // Start the time-consuming operation.
            e.Result = TimeConsumingOperation(bw);

            // If the operation was canceled by the user,
            // set the DoWorkEventArgs.Cancel property to true.
            if (bw.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        // This event handler demonstrates how to interpret
        // the outcome of the asynchronous operation implemented
        // in the DoWork event handler.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                // The user canceled the operation.
                MessageBox.Show("Operation was canceled");
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
                string msg = String.Format("An error occurred: {0}", e.Error.Message);
                MessageBox.Show(msg);
            }
            else
            {
                textBox1.Text += $"{e.Result}\r\n";
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (progressBar1.Maximum == progressBar1.Value)
            {
                progressBar1.Value = progressBar1.Minimum;
            }
            progressBar1.Value++;
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

            //textBox1.Text += $"{Komunikat}";

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

        private string TimeConsumingOperation(BackgroundWorker bw)
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
                Komunikat = $"{DateTime.Now:T}: {txt}";
            }

            return Komunikat;
        }
    }
}
