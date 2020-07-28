using System.Linq;
using System.Windows.Forms;

namespace CustomAlertBoxDemo
{
    public static class WindowExt
    {
        // NB : Best to call this function from the windows Loaded event or after showing the window
        // (otherwise window is just positioned to fill the secondary monitor rather than being maximised).
        public static void HalfSizeOnSecondaryMonitor(this Form form)
        {
            form.StartPosition = FormStartPosition.Manual;

            var secondaryScreen = Screen.AllScreens.FirstOrDefault(s => !s.Primary);

            if (secondaryScreen != null)
            {
                SetFormSizeToHalfOfScreen(form, secondaryScreen);
            }
            else
            {
                SetFormSizeToHalfOfScreen(form, Screen.AllScreens.First());
            }
        }

        private static void SetFormSizeToHalfOfScreen(Form form, Screen screen)
        {
            //move to other monitor
            form.Location = screen.WorkingArea.Location;

            //set sizes
            var workingArea = screen.WorkingArea;
            form.Width = workingArea.Width / 2;

            form.Left = workingArea.Left + form.Width + 5;
            form.Top = workingArea.Top;
            form.Height = workingArea.Height;
        }
    }
}