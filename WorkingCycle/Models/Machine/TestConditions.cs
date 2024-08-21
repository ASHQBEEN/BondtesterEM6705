using System.Text.Json;

namespace DutyCycle.Models.Machine
{
    public class TestConditions
    {
        public TestVelocities Velocities { get; set; } 
        public TestPoints TestPoints { get; set; }
    }

    public class TestVelocities
    {
        public double Break { get; set; } = 10;
        public double Stretch { get; set; } = 10;
        public double Shear { get; set; } = 10;

        public void Save()
        {
            var machine = Singleton.GetInstance();
            string path = machine.configurationJsonPath;
            string oldMachineData;

            if (File.Exists(path))
            {
                try
                {
                    oldMachineData = File.ReadAllText(path);
                    var newMachineData = JsonSerializer.Deserialize<MachineDto>(oldMachineData);
                    newMachineData.TestConditions.Velocities = machine.TestConditions.Velocities;
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
    }

    public class TestPoints
    {
        public double[] Break { get; set; }
        public double[] Stretch { get; set; }
        public double[] Shear { get; set; }

        public void Save()
        {
            var machine = Singleton.GetInstance();
            string path = machine.configurationJsonPath;
            string oldMachineData;

            if (File.Exists(path))
            {
                try
                {
                    oldMachineData = File.ReadAllText(path);
                    var newMachineData = JsonSerializer.Deserialize<MachineDto>(oldMachineData);
                    newMachineData.TestConditions.TestPoints = machine.TestConditions.TestPoints;
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
    }


}
