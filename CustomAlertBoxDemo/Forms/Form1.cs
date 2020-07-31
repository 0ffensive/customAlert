using System;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using CustomAlertBoxDemo.Selenium;
using CustomAlertBoxDemo.Selenium.Core;
using OpenQA.Selenium;

namespace CustomAlertBoxDemo.Forms
{
    public partial class Form1 : Form
    {
        private void StartBrowsingLocation()
        {
            try
            {
                reg.Start(GetSelectedLocation);
            }
            catch (Exception e)
            {
                WriteMessage(e.ToString(), 222);
            }
            
        }
        private string GetSelectedLocation => chbEdynburg.Checked ? chbEdynburg.Text : chbManchester.Text;
        private bool IsTimeRight => DateTime.Now.TimeOfDay >= timePickerFrom.Value.TimeOfDay 
                                    && DateTime.Now.TimeOfDay <= timePickerTo.Value.TimeOfDay;

        private int GetFrequencySeconds()
        {
            if (Int32.TryParse(txbFrequency.Text, out int result))
                return result;
            else 
                return 20;
        }

        private void KickOffSelenium()
        {
            InitSelenium();
            StartBrowsingLocation();
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
                txt = exception.ToString().Substring(0, 222);
            }

            //return Komunikat;
            return txt;
        }

        private void WriteMessage(string msg, int trimTo = 0)
        {
            if (trimTo > 0)
            {
                msg = msg.Substring(0, trimTo);
            }

            txbOutput.AppendText($"{DateTime.Now:T}: {msg}\r\n");
        }

        private void CheckBoxClicked(CheckBox activeCheckBox)
        {
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
            timer1.Start();
            btnTimer.ForeColor = Color.DarkGreen;

            if(!timer2.Enabled)
            {
                timer2.Start();
            }
        }

        private void StopTimer()
        {
            timer1.Stop();
            btnTimer.ForeColor = Color.DarkGoldenrod;

            if(timer2.Enabled)
            {
                timer2.Stop();
            }
        }

        private void ToggleTimer()
        {
            if (timer1.Enabled)
                StopTimer();
            else
                StartTimer();
        }

    
    }
}
