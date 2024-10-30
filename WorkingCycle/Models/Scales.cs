using DutyCycle.Models.Machine;
using System.Globalization;
using System.IO.Ports;

namespace DutyCycle.Models
{
    public class Scales
    {
        #region Singleton
        private static Scales instance;

        private Scales() { }

        public static Scales GetInstance()
        {
            if(instance is null)
                try
                {
                    instance = new();
                    instance.port = new(GetLastPortName(), 115200, Parity.None, 8, StopBits.One);
                    instance.port.DataReceived += instance.DataReceivedEvent;
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось ИНИЦИАЛИЗИРОВАТЬ COM-порт.",
                        "Ошибка COM-порта", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            return instance;
        }
        #endregion

        public double WeightValue { get; private set; }

        private SerialPort? port;

        public void ReadTestValue()
        {
            try
            {
                port?.Write("r");
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
                port.Write("t");
            }
            catch (Exception)
            {
                //throw;
            }
        }

        public void Calibrate() => port.Write("c");
        public void SendReferenceWeight(int weight) => port.Write(weight.ToString());
        public void Close() => port?.Close();

        public void Open()
        {
            try
            {
                port?.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть COM-порт. Скорее всего он уже подключен, недоступен или вовсе отключен.", "Ошибка COM-порта", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void DataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
        {
            // If the com Scales has been closed, do nothing
            if (!port.IsOpen) return;

            // Read all the data waiting in the buffer
            string receivedValueString = port.ReadLine();
            WeightValue = double.Parse(receivedValueString, CultureInfo.InvariantCulture);
            WeightValue = Math.Round(WeightValue, 1);
        }

        public static string GetLastPortName() => SerialPort.GetPortNames()
    .OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out int num) ? num : 0)
    .ToArray()
    .Last();
    }
}
