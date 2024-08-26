using System.Globalization;
using System.IO.Ports;

namespace DutyCycle.Models
{
    public class ComPort
    {
        public bool IsOpen { get { return port.IsOpen; } }
        public double TestValue { get; private set; }
        private readonly SerialPort port = new();

        public ComPort()
        {
            port.BaudRate = 115200;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.DataReceived += DataReceivedEvent;
        }

        public void ReadTestValue()
        {
            try
            {
                port.Write("w;");
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void TareScale()
        {
            try
            {
                port.Write("t;");
            }
            catch (Exception)
            {
                //throw;
            }
        }
        public void Calibrate() => port.Write("c;");
        public void SendReferenceWeight(int weight) => port.Write(weight.ToString());
        public void Close() => port.Close();
        //public string ReadReferenceWeight() => port.ReadLine();

        public void Open(string portName)
        {
            try
            {
                port.PortName = portName;
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось ИНИЦИАЛИЗИРОВАТЬ COM-порт.", "Ошибка COM-порта", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            try
            {
                if (port.IsOpen)
                {
                    port.Close();
                }
                else
                    port.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть COM-порт. Скорее всего он уже подключен, недоступен или вовсе отключен.", "Ошибка COM-порта", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void DataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
        {
            // If the com port has been closed, do nothing
            if (!IsOpen) return;

            // Read all the data waiting in the buffer
            string receivedValueString = port.ReadLine();
            TestValue = double.Parse(receivedValueString, CultureInfo.InvariantCulture);
            TestValue = Math.Round(TestValue/10, 1);
        }

        public static string GetLastPortName() => SerialPort.GetPortNames()
    .OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out int num) ? num : 0)
    .ToArray()
    .Last();

        //    public static string GetLastPortName() 
        //    {
        //        string portName = string.Empty;
        //        try
        //        {
        //            portName = SerialPort.GetPortNames()
        //.OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out int num) ? num : 0)
        //.ToArray()
        //.Last();
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show("Не удалось ОБНАРУЖИТЬ COM-порт.", "Ошибка COM-порта", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        }
        //        return portName;
        //    } 
    }
}
