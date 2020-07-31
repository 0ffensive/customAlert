using System;
using System.Windows.Forms;

namespace CustomAlertBoxDemo.Controls
{
    public static class ControlsExtensions
    {
        public static void Invoke<T>(this T c, Action<T> action) where T : Control
        {
            if (c.InvokeRequired)
                c.TopLevelControl.Invoke(action);
            else
                action(c);
        }
    }
}