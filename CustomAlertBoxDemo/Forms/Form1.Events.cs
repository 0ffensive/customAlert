using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomAlertBoxDemo.Forms
{
    partial class Form1
    {
        protected void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            p.Graphics.Clear(SystemColors.Control);

            //box.ForeColor = timer1.Enabled ? Color.Green : Color.DarkRed;
            var brush = timer1.Enabled ? Brushes.Green : Brushes.DarkRed;
            p.Graphics.DrawString(box.Text, box.Font, brush, 0, 0);
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

        private async void button2_Click(object sender, EventArgs e)
        {
            //this.Alert("Warning Alert", Form_Alert.enmType.Warning);

            //textBox1.Text += $"{Komunikat}";

            //Scraper scraper = new Scraper();
            //string result = await scraper.ScrapeWebsite();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Screen[] screens = Screen.AllScreens;
            MaximalizeForm(this, screens[1]);
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

        private void chk_Click(object sender, EventArgs e)
        {
            CheckBox activeCheckBox = sender as CheckBox;

            if(activeCheckBox != lastChecked && lastChecked != null) 
                lastChecked.Checked = false;

            lastChecked = activeCheckBox.Checked ? activeCheckBox : null;

            if(activeCheckBox.Checked && reg.Lokalizacja != activeCheckBox.Text)
            {
                //set times
                if (activeCheckBox.Text == "Edynburg")
                {
                    timePickerFrom.Value = new DateTime(2020, 1, 1,19, 45, 0);
                    timePickerTo.Value = new DateTime(2020, 1, 1,20, 45, 0);
                }
                else
                {
                    timePickerFrom.Value = new DateTime(2020, 1, 1,8, 45, 0);
                    timePickerTo.Value = new DateTime(2020, 1, 1,9, 45, 0);
                }

                CheckBoxClicked(activeCheckBox);
            }
        }
        
        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (driver == null)
            {
                InitSelenium();
            }

            //jesli nie jestesmy na stronie docelowej
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
