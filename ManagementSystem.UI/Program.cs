using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ManagementSystem.UI
{
    internal static class Program
    {
        /// <summary>
        ///  Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var culturaCr = new CultureInfo("es-CR");
            culturaCr.NumberFormat.CurrencySymbol = "₡";
            culturaCr.NumberFormat.CurrencyPositivePattern = 0; // ₡n
            culturaCr.NumberFormat.CurrencyNegativePattern = 1; // -₡n

            CultureInfo.DefaultThreadCurrentCulture = culturaCr;
            CultureInfo.DefaultThreadCurrentUICulture = culturaCr;
            Thread.CurrentThread.CurrentCulture = culturaCr;
            Thread.CurrentThread.CurrentUICulture = culturaCr;

            ApplicationConfiguration.Initialize();
            Application.Run(new Forms.LoginForms());
        }
    }
}

