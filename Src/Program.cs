using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MarkerDatabase markerDatabase = new MarkerDatabase(logger: new FileLogger());
            MainViewModel mainViewModel = new MainViewModel(markerDatabase);
            MainForm mainForm = new MainForm(mainViewModel);

            Application.Run(mainForm);
        }
    }
}
