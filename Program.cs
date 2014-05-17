using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace kOS_IDE
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            OpenedForms.Init();
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            //try { AppOptions.Load("config.cfg"); }
            //catch { AppOptions.Default(); }

            Application.Run(new TextEditor());
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            //AppOptions.Save("config.cfg");
        }
    }
}
