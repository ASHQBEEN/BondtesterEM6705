using ashqTech;
using System.Text.Json;

namespace DutyCycle.Models.Machine
{
    public sealed class Singleton
    {
        #region Singleton
        private static Singleton instance;

        private Singleton() { }

        public static Singleton GetInstance()
        {
            instance ??= new Singleton();
            return instance;
        }
        #endregion

        public Board Board { get; private set; }
        private uint deviceType = 167;
        private uint boardID = 1;
        private int axesCount = 4;

        public bool AxisZBasingDone { get; set; } = false;
        public bool CameraIsFullscreen { get; set; } = false;

        public MachineParameters Parameters { get; set; } = new MachineParameters
        {
            LowVelocity = [20, 20, 20, 20],
            SlowVelocity = [500, 500, 500, 500],
            FastVelocity = [20000, 20000, 5000, 2000],
            Acceleration = [1000000, 1000000, 1000000, 1000000],
            Jerk = [0, 0, 0, 0],
            MaxCoordinate = [0, 0, 64000, 0],
            BasingVelocities = [20000, 20000, 4000, 1000],
        };

        public TestConditions TestConditions { get; set; } = new TestConditions
        {
            Velocities = new TestVelocities
            {
                Break = 500,
                Stretch = 500,
                Shear = 500,
            },
            TestPoints = new TestPoints
            {
                Break = [50000, 50000, 5000],
                Stretch = [40000, 40000, 4000],
                Shear = [30000, 30000, 3000]
            }
        };

        public CameraParameters CameraParameters { get; set; } = new CameraParameters
        {
            Gain = 63,
            ExposureTime = 39000,
            BlackLevelAuto = "Off",
            BlackLevelSelector = "All",
            BlackLevel = 20,
            ADCLevel = 1
        };

        public string advantechConfigurationPath;
        public readonly string configurationJsonPath = "Configuration.json";
        public static JsonSerializerOptions serializerOptions = new() { WriteIndented = true };

        public void InitializeBoard()
        {
            instance.Board = new(deviceType, boardID, axesCount);
        }

        public void SaveParameters()
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(instance.ToDto(), serializerOptions);
                File.WriteAllText(configurationJsonPath, jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения файла конфигурации: {ex.Message}");
            }
        }

        private MachineDto ToDto()
        {
            return new MachineDto
            {
                AxesCount = instance.axesCount,
                BoardID = instance.boardID,
                DeviceType = instance.deviceType,
                Parameters = instance.Parameters,
                TestConditions = instance.TestConditions,
                advantechConfigurationPath = instance.advantechConfigurationPath,
                CameraParameters = instance.CameraParameters
            };
        }

        public void LoadAllModulesParameters()
        {
            try
            {
                if (File.Exists(configurationJsonPath))
                {
                    string loadedJsonData;
                    loadedJsonData = File.ReadAllText(configurationJsonPath);
                    var dto = JsonSerializer.Deserialize<MachineDto>(loadedJsonData);
                    if (dto == null)
                        throw new ArgumentNullException(nameof(dto));
                    instance.deviceType = dto.DeviceType;
                    instance.boardID = dto.BoardID;
                    instance.axesCount = dto.AxesCount;
                    instance.Parameters = dto.Parameters;
                    instance.TestConditions = dto.TestConditions;
                    instance.advantechConfigurationPath = dto.advantechConfigurationPath;
                    instance.CameraParameters = dto.CameraParameters;
                }
                else
                {
                    MessageBox.Show("Вы ещё не сохраняли параметры установки. Будут применены параметры по умолчанию.");
                    SaveParameters();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки файла конфигурации:\n {ex.Message} \n\nБудут применены параметры по умолчанию.");
            }
        }

        public void LoadOverridedConfig()
        {
            LoadConfig();
            //Since Acceleration = Deceleration (requirement)
            OverrideConfig();
        }

        public void LoadConfig()
        {
            if (File.Exists(advantechConfigurationPath))
            {
                DriverControl.LoadConfig(Board.deviceHandler, advantechConfigurationPath);
            }
            else MessageBox.Show($"Конфигурационный файл для платы драйвера не был обнаружен.");
        }

        public void SaveAdvantechConfiguration()
        {
            string oldMachineData;

            if (File.Exists(configurationJsonPath))
            {
                try
                {
                    oldMachineData = File.ReadAllText(configurationJsonPath);
                    var newMachineData = JsonSerializer.Deserialize<MachineDto>(oldMachineData);
                    newMachineData.advantechConfigurationPath = instance.advantechConfigurationPath;
                    string jsonData = JsonSerializer.Serialize(newMachineData, serializerOptions);
                    File.WriteAllText(configurationJsonPath, jsonData);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения файла конфигурации платы: {ex.Message}");
                }

            }
            else
                MessageBox.Show("Не удалось загрузить скорости теста, файл конфгурации не обнаружен.");
        }


        public void OverrideConfig()
        {
            LoadAllModulesParameters();
            //Since Acceleration = Deceleration (requirement)
            Board.BoardSetDec(Parameters.Acceleration);
            Board.BoardSetLowVelocity(Parameters.LowVelocity);
            Board.BoardSetAcc(Parameters.Acceleration);
            Board.BoardSetJerk(Parameters.Jerk);
        }
    }
}
