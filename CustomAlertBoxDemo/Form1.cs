using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CustomAlertBoxDemo;
using CustomAlertBoxDemo.Controls;
using CustomAlertBoxDemo.Selenium;
using CustomAlertBoxDemo.Selenium.Core;
using OpenQA.Selenium;

namespace CustomAlertBoxDemo
{
    public partial class Form1 : Form
    {
        private int counter = 0;
        int seconds = 0;
        public static string Komunikat;
        public bool Rezerwacja { get; set; }

        public DriverFactory driverFactory;
        public IWebDriver driver;
        private RegistrationPage reg;

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

            //timer1.Interval = GetFrequencySeconds() * 1000;
            //timer1.Start();

            WindowExt.HalfSizeOnSecondaryMonitor(this);

            if (IsTimeRight)
            {
                KickOffSeleniumInBackground();
                StartTimer();
            }
        }
        ~Form1()
        {
            driver.Close();
            driver.Dispose();
            driverFactory.Dispose();
        }

        private int GetFrequencySeconds()
        {
            if (Int32.TryParse(txbFrequency.Text, out int result))
                return result;
            else 
                return 20;
        }

        private void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            p.Graphics.Clear(SystemColors.Control);

            //box.ForeColor = timer1.Enabled ? Color.Green : Color.DarkRed;
            var brush = timer1.Enabled ? Brushes.Green : Brushes.DarkRed;
            p.Graphics.DrawString(box.Text, box.Font, brush, 0, 0);
        }

        private void KickOffSeleniumInBackground()
        {
            Thread thread = new Thread(() =>
            {
                KickOffSelenium();
                Start_TimeConsumingOperation();
                //backgroundWorker1.RunWorkerAsync();
            });
            thread.Priority = ThreadPriority.BelowNormal;
            thread.Start();
        }

        private void KickOffSelenium()
        {
            InitSelenium();
            StartBrowsingLocation();
        }

        private void InitSelenium()
        {
            driverFactory = new DriverFactory();
            driver = driverFactory.CreateDriver();
            //
            var screen = WindowExt.GetSecondaryScreen().WorkingArea;
            driver.Manage().Window.Size = new Size(screen.Width / 2, screen.Height);

            if (Screen.AllScreens.Length > 1)
                driver.Manage().Window.Position = new Point(-1500, 0);

            reg = new RegistrationPage(driver);
        }

        private void StartBrowsingLocation() => reg.Start(GetSelectedLocation);
        private string GetSelectedLocation => chbEdynburg.Checked ? chbEdynburg.Text : chbManchester.Text;
        private bool IsTimeRight => DateTime.Now.TimeOfDay >= timePickerFrom.Value.TimeOfDay 
                                    && DateTime.Now.TimeOfDay <= timePickerTo.Value.TimeOfDay;

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
                textBox1.AppendText($"{e.Result}\r\n");
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
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.Alert("Success Alert",Form_Alert.enmType.Success);
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            Screen[] screens = Screen.AllScreens;
            setFormLocation(this, screens[1]);
        }

        private void setFormLocation(Form form, Screen screen)
        {
            // first method
            Rectangle bounds = screen.Bounds;
            form.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);

            // second method
            //Point location = screen.Bounds.Location;
            //Size size = screen.Bounds.Size;

            //form.Left = location.X;
            //form.Top = location.Y;
            //form.Width = size.Width;
            //form.Height = size.Height;
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
            Start_TimeConsumingOperation();
        }

        private void Start_TimeConsumingOperation()
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
                
                bool exists = reg.DniDropDown.Exists();
                if (exists && reg.DniDropDown.IsEnabled)
                {
                    timer1.Enabled = false;
                    SystemSounds.Exclamation.Play();

                    txt = $"REZERWACJA";
                    Komunikat = $"{DateTime.Now:T}: {txt}";

                    reg.ScrollToBottom();
                    reg.Rezerwuj();
                }

                txt = reg.KomunikatSpan.Text;

                reg.ScrollToBottom();
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

        //We need this to hold the last checked CheckBox
        private CheckBox lastChecked;

        private void chk_Click(object sender, EventArgs e)
        {
            CheckBox activeCheckBox = sender as CheckBox;

            if(activeCheckBox != lastChecked && lastChecked != null) 
                lastChecked.Checked = false;

            lastChecked = activeCheckBox.Checked ? activeCheckBox : null;

            if(activeCheckBox.Checked)
            {
                CheckBoxClicked(activeCheckBox);
            }
        } 

        private void CheckBoxClicked(CheckBox activeCheckBox)
        {
            //set times
            if (activeCheckBox.Text == "Edynburg")
            {
                timePickerFrom.Value = new DateTime(2020, 1, 1,19, 45, 0);
                timePickerTo.Value = new DateTime(2020, 1, 1,20, 20, 0);
            }
            else
            {
                timePickerFrom.Value = new DateTime(2020, 1, 1,8, 45, 0);
                timePickerTo.Value = new DateTime(2020, 1, 1,9, 20, 0);
            }

            if (timer1.Enabled || IsTimeRight)
            {
                StopTimer();
                if (driver == null)
                {
                    KickOffSelenium();
                }
                StartBrowsingLocation();
                //btnTimer_Click(this, null);
                StartTimer();
            }
        }

        private void StartTimer()
        {
            timer1.Enabled = true;
            timer1.Start();

            if(!timer2.Enabled)
            {
                timer2.Enabled = true;
                timer2.Start();
            }
        }

        private void StopTimer()
        {
            timer1.Stop();
            timer1.Enabled = false;
        }

        private void ToggleTimer()
        {
            if (timer1.Enabled)
                StopTimer();
            else
                StartTimer();
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (driver == null)
            {
                InitSelenium();
            }

            if (!driver.Url.Contains("guid"))
            {
                StartBrowsingLocation();
                backgroundWorker1.RunWorkerAsync();
            }

            ToggleTimer();

            grpTimer.Refresh();
        }

        private void txbFrequency_Leave(object sender, EventArgs e)
        {
            //StopTimer();
            timer1.Interval = GetFrequencySeconds() * 1000;
            timer2.Stop();
            seconds = GetFrequencySeconds();
            lblCounter.Text = seconds.ToString();
            timer2.Start();
            //StartTimer();
        }

        
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
                return;

            seconds--;
            if(seconds < 0)
            {
                seconds = GetFrequencySeconds();
            }
            lblCounter.Text = seconds.ToString();
        }
    }
}
