using HidSharp;
using ashqTech;
using DutyCycle.Models.Machine;
using ScottPlot.Colormaps;
using System.Reflection.PortableExecutable;

namespace DutyCycle.Scripts
{
    public class Joystick
    {
        public string DeviceName { get => joystick.GetProductName(); }
        public int VID { get => joystick.VendorID; }
        public int PID { get => joystick.ProductID; }
        private HidDevice joystick;
        private HidStream stream;
        private Thread joystickPoll;
        private bool isPolled = false;

        private Board board = Singleton.GetInstance().Board;
        private MachineParameters parameters = Singleton.GetInstance().Parameters;

        public Joystick()
        {
            Initialize();
        }

        private void PollJoystick()
        {
            while (isPolled)
            {
                byte[] inputReport = new byte[joystick.GetMaxInputReportLength()];

                int[] prevLevels = new int[3];
                int[] prevValues = new int[3];

                // Чтение данных с джойстика

                int bytesRead = stream.Read(inputReport, 0, inputReport.Length);
                if (bytesRead > 0)
                {
                    int[] levels = [inputReport[2], inputReport[4], inputReport[6]];
                    int[] values = [inputReport[1], inputReport[3], inputReport[5]];

                    //int i = 0;
                    for (int i = 0; i < board.AxesCount - 1; i++)
                        if (prevValues[i] != values[i] || prevLevels[i] != levels[i])
                        {
                            PollToMoveAxis(i, values[i], levels[i]);
                            prevLevels[i] = levels[i];
                            prevValues[i] = values[i];
                            Console.WriteLine($"level: {levels[i]}, value: {values[i]}");
                        }
                }
            }
        }

        private void PollToMoveAxis(int axisIndex, int value, int level)
        {
            if ((level == 2 && value == 0) || (level == 3 && value == 0) || (level == 1 && value == 255) || (level == 0 && value == 255))
            {
                board.StopAxisEmg(axisIndex);
                return;
            }

            //DIRECTION
            const ushort PositiveDirection = 0, NegativeDirection = 1;
            ushort direction;
            if (value > 2)
                direction = NegativeDirection;
            else
                direction = PositiveDirection;

            double speed;
            if (value == 3 || value == 0)
                speed = parameters.FastVelocity[axisIndex];
            else
                speed = parameters.SlowVelocity[axisIndex];
            board.SetAxisHighVelocity(axisIndex, speed);
            board.StartAxisContinuousMovementChecked(axisIndex, direction);
        }

        private void Initialize()
        {
            DeviceList devices = DeviceList.Local;
            foreach (var device in devices.GetHidDevices())
            {
                // Открываем устройство для чтения
                if (device.GetProductName().Contains("Joystick")) // Можно добавить точное имя или фильтр по VID/PID
                {
                    joystick = device;
                    stream = joystick.Open();
                    isPolled = true;
                    joystickPoll = new Thread(PollJoystick);
                    joystickPoll.Start();
                    break;
                }
            }
            if (joystick == null)
                throw new Exception("No joystick found!");
        }

        public void StopPolling()
        {
            isPolled = false;
            joystickPoll.Join();
            stream.Close();
        }
    }
}
