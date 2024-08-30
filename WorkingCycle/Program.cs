using DutyCycle.Forms;

using static DutyCycle.Scripts.KeyboardControls;

namespace DutyCycle
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var keyboardControl = new KeyboardControl();
            keyboardControl.PreInitialize();

            Application.Run(new MainMenu());
        }
    }
}