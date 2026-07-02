using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace UV7_Program_Manager
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Config.ApplicationConfig.LoadConfigFile();

            if (Config.ApplicationConfig.Language)
            {
                var culture = new CultureInfo("de-DE");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                Application.CurrentCulture = culture;
            }
            else
            {
                var culture = CultureInfo.GetCultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
                Application.CurrentCulture = culture;
            }

            Application.Run(new Form_main());
        }
    }
}