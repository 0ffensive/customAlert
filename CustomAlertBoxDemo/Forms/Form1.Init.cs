using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomAlertBoxDemo.Selenium;
using CustomAlertBoxDemo.Selenium.Core;
using OpenQA.Selenium;

namespace CustomAlertBoxDemo.Forms
{
    partial class Form1
    {
        public DriverFactory driverFactory;
        public IWebDriver driver;
        private RegistrationPage reg;

        //We need this to hold the last checked CheckBox
        private CheckBox lastChecked;

        private int counter = 0;
        int seconds = 0;

        public static string Komunikat;
        public bool Rezerwacja { get; set; }

        public Form1()
        {
            InitializeComponent();

            grpTimer.Paint += PaintBorderlessGroupBox;

            timePickerFrom.Format = DateTimePickerFormat.Custom;
            timePickerFrom.CustomFormat = "HH:mm"; // Only use hours and minutes
            timePickerFrom.ShowUpDown = true;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 5;
            progressBar1.Value = 1;

            lastChecked = chbManchester;

            timer1.Enabled = false;
            seconds = GetFrequencySeconds();
            txbFrequency_Leave(this, null);

            WindowExt.HalfSizeOnSecondaryMonitor(this);

            if (IsTimeRight)
            {
                KickOffSeleniumInBackground();
                //StartTimer();
            }
        }
        ~Form1()
        {
            driver.Close();
            driver.Dispose();
            driverFactory.Dispose();
        }


        private void KickOffSeleniumInBackground()
        {
            Thread thread = new Thread(() =>
            {
                KickOffSelenium();
                //Start_TimeConsumingOperation();
                StartTimer();
                //if(!backgroundWorker1.IsBusy)
                //{
                //    backgroundWorker1.RunWorkerAsync();    
                //}
                
            });
            thread.Priority = ThreadPriority.BelowNormal;
            thread.Start();
        }

        private void InitSelenium()
        {
            driverFactory = new DriverFactory();
            driver = driverFactory.CreateDriver();
            reg = new RegistrationPage(driver);
            
            //sizing
            var screen = WindowExt.GetSecondaryScreen().WorkingArea;
            driver.Manage().Window.Size = new Size(screen.Width / 2, screen.Height - 200);

            //move window to the left if 2 screens
            if (Screen.AllScreens.Length > 1)
                driver.Manage().Window.Position = new Point(-1500, 0);
        }

        private void MaximalizeForm(Form form, Screen screen)
        {
            Rectangle bounds = screen.Bounds;
            form.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }
    }
}
