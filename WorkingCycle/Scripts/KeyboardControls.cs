using ashqTech;
using DutyCycle.Logic;
using DutyCycle.Models.Machine;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DutyCycle.Scripts
{
    public class KeyboardControls
    {
        public class KeyboardControl
        {
            private const int WH_KEYBOARD_LL = 13;
            private const int WM_KEYDOWN = 0x0100;
            private const int WM_KEYUP = 0x0101;

            private static LowLevelKeyboardProc _proc = HookCallback;
            private static nint _hookID = nint.Zero;

            public static bool blockControls = false;

            public void PreInitialize()
            {
                _hookID = SetHook(_proc);
            }

            public void Initialize()
            {
                UnhookWindowsHookEx(_hookID);
            }

            private delegate nint LowLevelKeyboardProc(int nCode, nint wParam, nint lParam);

            private static nint HookCallback(int nCode, nint wParam, nint lParam)
            {
                Keys key = (Keys)Marshal.ReadInt32(lParam);
                if (key == Keys.Space) Basing.Stop();

                if (blockControls) return nint.Zero;

                //Нажата ли клавиша передвижения
                if (nCode >= 0)
                {
                    int axisIndex;

                    //Соответсвие оси определённой клавише
                    switch (key)
                    {
                        case Keys.Right:
                        case Keys.Left:
                            axisIndex = 0;
                            break;
                        case Keys.Up:
                        case Keys.Down:
                            axisIndex = 1;
                            break;
                        case Keys.PageUp:
                        case Keys.PageDown:
                            axisIndex = 2;
                            break;
                        case Keys.Insert:
                        case Keys.Delete:
                            axisIndex = 3;
                            break;
                        default:
                            return nint.Zero;
                    }

                    var board = Singleton.GetInstance().Board;

                    //событие KeyDown
                    if (wParam == WM_KEYDOWN)
                    {
                        CtrlSpeedSwitch(axisIndex);
                        ushort direction;
                        const ushort PositiveDirection = 0, NegativeDirection = 1;
                        if (key == Keys.Right || key == Keys.Up || key == Keys.PageUp || key == Keys.Insert)
                            direction = PositiveDirection;
                        else
                            direction = NegativeDirection;
                        board.StartAxisContinuousMovementChecked(axisIndex, direction);
                    }

                    //событие KeyUp
                    if (wParam == WM_KEYUP)
                    {
                        board.StopAxisEmg(axisIndex);
                    }
                }

                // Передаем управление следующему хуку в цепочке
                return CallNextHookEx(_hookID, nCode, wParam, lParam);
            }

            private static nint SetHook(LowLevelKeyboardProc proc)
            {
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }

            private static void CtrlSpeedSwitch(int axisIndex)
            {
                var machine = Singleton.GetInstance();
                double speed;
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                    speed = machine.Parameters.FastVelocity[axisIndex];
                else
                    speed = machine.Parameters.SlowVelocity[axisIndex];
                machine.Board.SetAxisHighVelocity(axisIndex, speed);
            }

            #region DLL Imports
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern nint SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, nint hMod, uint dwThreadId);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool UnhookWindowsHookEx(nint hhk);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern nint CallNextHookEx(nint hhk, int nCode, nint wParam, nint lParam);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern nint GetModuleHandle(string lpModuleName);
            #endregion
        }
    }
}