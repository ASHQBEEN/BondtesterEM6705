using GxIAPINET;
using System.Text.Json;

namespace CameraSettings
{
    public static class Control
    {
        private static IGXFactory? factory;
        private static IGXDevice? device;
        private static IGXFeatureControl? featureControl;

        private static CameraParameters parameters;

        public const string configurationJsonPath = "CameraConfiguration.json";
        public static readonly JsonSerializerOptions serializerOptions = new() { WriteIndented = true };

        static Control()
        {
            parameters = new();
            LoadParameters();
        }

        public static void LoadParameters()
        {
            string path = configurationJsonPath;
            string loadedCameraData;

            if (File.Exists(path))
            {
                try
                {
                    loadedCameraData = File.ReadAllText(path);
                    var dto = JsonSerializer.Deserialize<CameraParameters>(loadedCameraData);
                    if (dto == null)
                        throw new ArgumentNullException(nameof(dto));
                    parameters = dto;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Не удалось загрузить параметры камеры, файл конфгурации не обнаружен. Будут применены параметры по умолчанию");
                string jsonData = JsonSerializer.Serialize(parameters, serializerOptions);
                File.WriteAllText(path, jsonData);
            }
        }

        public static void OpenDevice()
        {
            try
            {
                factory = IGXFactory.GetInstance();
                factory.Init();

                List<IGXDeviceInfo> listGXDeviceInfo = [];
                factory.UpdateDeviceList(200, listGXDeviceInfo);

                // Check it has found device or not.
                if (listGXDeviceInfo.Count <= 0)
                {
                    Console.WriteLine("No device has been found!");
                    return;
                }

                //Open the first device in the device list has been found
                device = factory.OpenDeviceBySN(listGXDeviceInfo[0].GetSN(), GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
                featureControl = device.GetRemoteFeatureControl();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CloseDevice()
        {
            try
            {
                device?.Close();
                device = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ApplyParameters()
        {
            try
            {
                featureControl?.GetFloatFeature("Gain").SetValue(parameters.Gain);
                featureControl?.GetFloatFeature("ExposureTime").SetValue(parameters.ExposureTime);
                featureControl?.GetEnumFeature("BlackLevelAuto").SetValue(parameters.BlackLevelAuto);
                featureControl?.GetEnumFeature("BlackLevelSelector").SetValue(parameters.BlackLevelSelector);
                featureControl?.GetFloatFeature("BlackLevel").SetValue(parameters.BlackLevel);
                featureControl?.GetIntFeature("ADCLevel").SetValue(parameters.ADCLevel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); ;
            }
        }
    }
}
