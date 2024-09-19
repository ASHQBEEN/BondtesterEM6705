using System.Text.Json;
using System.Text.Json.Serialization;

namespace DutyCycle.Models.Machine
{
    public class MachineParameters
    {
        [JsonIgnore] public bool ParametersBeenSet { get; set; } = false;
        public required double[] LowVelocity { get; set; }
        public required double[] SlowVelocity { get; set; }
        public required double[] FastVelocity { get; set; }
        public required double[] Acceleration { get; set; }
        public required double[] Jerk { get; set; }
        public required double[] MaxCoordinate { get; set; }
        public required double[] BasingVelocities { get; set; }

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
                    newMachineData.Parameters = machine.Parameters;
                    string jsonData = JsonSerializer.Serialize(newMachineData, Singleton.serializerOptions);
                    File.WriteAllText(path, jsonData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
                MessageBox.Show("Не удалось загрузить скорости теста, файл конфгурации не обнаружен.");
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
                    machine.Parameters = dto.Parameters;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка десериализации файла конфигурации. Скорее всего он повреждён\n" + ex.Message);
                }
            }
            else
                MessageBox.Show("Не удалось загрузить скорости теста, файл конфгурации не обнаружен.");
        }
    }
}
