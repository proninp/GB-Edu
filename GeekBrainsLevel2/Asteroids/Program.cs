using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                MaximizeBox = false
            };
            Game.Init(form);
            form.Show();
            Application.Run(form);
        }
    }
}
