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

            var secondaryScreen = GetSecondaryScreen();

            SetFormSizeToHalfOfScreen(form, secondaryScreen);
        }

        public static Screen GetSecondaryScreen()
        {
            return Screen.AllScreens.FirstOrDefault(s => !s.Primary)
                ?? Screen.AllScreens.First();
        }

        private static void SetFormSizeToHalfOfScreen(Form form, Screen screen)
        {
            //move to other monitor
            form.Location = screen.WorkingArea.Location;

            //set sizes
            var workingArea = screen.WorkingArea;
            form.Width = workingArea.Width / 2;

            form.Left = workingArea.Left + form.Width;
            form.Top = workingArea.Top;
            form.Height = workingArea.Height;
        }
    }
}