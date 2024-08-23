namespace CameraSettings
{
    public class CameraParameters
    {
        public double Gain { get; set; } = 63;
        public double ExposureTime { get; set; } = 39000;
        public string BlackLevelAuto { get; set; } = "Off";
        public string BlackLevelSelector { get; set; } = "All";
        public double BlackLevel { get; set; } = 20;
        public long ADCLevel { get; set; } = 1;       
    }
}
