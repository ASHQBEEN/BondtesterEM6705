using System.Globalization;
using System.IO.Ports;

namespace DutyCycle.Models
{
    public class Scales
    {
        public double WeightValue { get; private set; }

        private readonly SerialPort port = new(
            GetLastPortName(),
            115200,
            Parity.None,
            8,
            StopBits.One);

        public Scales() => port.DataReceived += DataReceivedEvent;

        public void ReadTestValue()
        {
            try
            {
                port.Write("w");
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
        public void Close() => port.Close();
        //public string ReadReferenceWeight() => scales.ReadLine();

        public void Open()
        {
            try
            {
                port.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть COM-порт. Скорее всего он уже подключен, недоступен или вовсе отключен.", "Ошибка COM-порта", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void DataReceivedEvent(object sender, SerialDataReceivedEventArgs e)
        {
            // If the com scales has been closed, do nothing
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
