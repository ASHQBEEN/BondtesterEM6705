using DutyCycle.Forms;
using DutyCycle.Logic;
using DutyCycle.Scripts;

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

            var menu = new MainMenu();
            Joystick joystick = new Joystick();
            Application.AddMessageFilter(new GlobalKeyMessageFilter(menu));

            Application.Run(menu);
        }
    }
}