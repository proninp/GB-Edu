using System;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        /// <summary>
        /// App main entry point
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form form = new Form
            {
                Width = Settings.FieldWidth,
                Height = Settings.FieldHeight,
                MaximizeBox = false,
                BackgroundImage = Properties.Resources.background
            };
            Game.Init(form);
            SplashScreen.ControlsInit(form);
            form.Show();
            Application.Run(form);
        }
    }
}
