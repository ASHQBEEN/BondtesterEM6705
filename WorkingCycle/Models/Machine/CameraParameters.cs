using System.Text.Json;

namespace DutyCycle.Models.Machine
{
    public class CameraParameters
    {
        public double Gain { get; set; }
        public double ExposureTime { get; set; }
        public string BlackLevelAuto { get; set; } = string.Empty;
        public string BlackLevelSelector { get; set; } = string.Empty;
        public double BlackLevel { get; set; }
        public long ADCLevel { get; set; }

        public void Save()
        {
            var machine = Singleton.GetInstance();
            string path = machine.ConfigurationJsonPath;
            string oldMachineData;

            if (File.Exists(path))
            {
                try
                {
                    oldMachineData = File.ReadAllText(path);
                    var newMachineData = JsonSerializer.Deserialize<MachineDto>(oldMachineData);
                    newMachineData.CameraParameters = machine.CameraParameters;
                    string jsonData = JsonSerializer.Serialize(newMachineData, Singleton.serializerOptions);
                    File.WriteAllText(path, jsonData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
                MessageBox.Show("Не удалось загрузить параметры камеры, файл конфгурации не обнаружен.");
        }

        public void Load()
        {
            var machine = Singleton.GetInstance();
            string path = machine.ConfigurationJsonPath;
            string loadedMachineData;

            if (File.Exists(path))
            {
                try
                {
                    loadedMachineData = File.ReadAllText(path);
                    var dto = JsonSerializer.Deserialize<MachineDto>(loadedMachineData);
                    if (dto == null)
                        throw new ArgumentNullException(nameof(dto));
                    machine.CameraParameters = dto.CameraParameters;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Не удалось загрузить параметры камеры, файл конфгурации не обнаружен.");
        }
    }
}
