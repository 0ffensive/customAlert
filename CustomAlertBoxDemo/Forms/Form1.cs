using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;
using CustomAlertBoxDemo.Selenium;
using CustomAlertBoxDemo.Selenium.Core;
using OpenQA.Selenium;
using Color = System.Drawing.Color;

namespace CustomAlertBoxDemo.Forms
{
    public partial class Form1 : Form
    {
        private void StartBrowsingLocation() => reg.Start(GetSelectedLocation);
        private string GetSelectedLocation => chbEdynburg.Checked ? chbEdynburg.Text : chbManchester.Text;
        private bool IsTimeRight => DateTime.Now.TimeOfDay >= timePickerFrom.Value.TimeOfDay 
                                    && DateTime.Now.TimeOfDay <= timePickerTo.Value.TimeOfDay;
        
        private MediaPlayer mediaPlayer = new MediaPlayer();

        private void PlayAlarm()
        {
            mediaPlayer.Open(new Uri($"{Directory.GetCurrentDirectory()}\\Resources\\alarm1.mp3"));
            mediaPlayer.Play();
        }
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
                    PlayAlarm();

                    txt = $"REZERWACJA";
                    //Komunikat = $"{DateTime.Now:T}: {txt}";
                    //reg.ScrollToBottom();
                    reg.Rezerwuj();
                }

                reg.ScrollToBottom();
                txt = reg.KomunikatSpan.Text;
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

        private void CheckBoxClicked(CheckBox activeCheckBox)
        {
            if (timer1.Enabled || IsTimeRight)
            {
                StopTimer();
                if (driver == null)
                {
                    KickOffSelenium();
                    //KickOffSeleniumInBackground();
                }
                //StartBrowsingLocation();
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
