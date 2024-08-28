using GxIAPINET;
using DutyCycle.Models.GxBitmap;
using DutyCycle.Models.Machine;

namespace DutyCycle.Forms.DutyCycle
{
    public partial class DutyCycleForm
    {
        private IGXStream stream;
        private GxBitmap bitmap;
        private IGXFactory factory;
        private IGXDevice device;
        private IGXFeatureControl featureControl;
        private bool m_bIsSnap = false;
        private bool m_bIsOpen = false;
        private IGXFeatureControl streamFeatureControl;

        public void ApplyParameters()
        {
            try
            {
                var cameraParameters = Singleton.GetInstance().CameraParameters;
                featureControl?.GetFloatFeature("Gain").SetValue(cameraParameters.Gain);
                featureControl?.GetFloatFeature("ExposureTime").SetValue(cameraParameters.ExposureTime);
                featureControl?.GetEnumFeature("BlackLevelAuto").SetValue(cameraParameters.BlackLevelAuto);
                featureControl?.GetEnumFeature("BlackLevelSelector").SetValue(cameraParameters.BlackLevelSelector);
                featureControl?.GetFloatFeature("BlackLevel").SetValue(cameraParameters.BlackLevel);
                featureControl?.GetIntFeature("ADCLevel").SetValue(cameraParameters.ADCLevel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); ;
            }
        }

        private void OpenGxDevice()
        {
            try
            {
                factory = IGXFactory.GetInstance();
                factory.Init();

                List<IGXDeviceInfo> listGXDeviceInfo = [];

                //close stream
                CloseStream();

                // If the device has been opened, it will be closed in order to the device can be opend again after initializing failed.
                CloseDevice();

                factory.UpdateDeviceList(200, listGXDeviceInfo);

                // Check it has found device or not.
                if (listGXDeviceInfo.Count <= 0)
                {
                    MessageBox.Show("No device has been found!");
                    return;
                }

                // If the device has been opened then close it to ensure the camera can be opened again.
                if (null != device)
                {
                    device.Close();
                    device = null;
                }

                //Open the first device in the device list has been found
                device = factory.OpenDeviceBySN(listGXDeviceInfo[0].GetSN(), GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
                featureControl = device.GetRemoteFeatureControl();


                //Open the stream.
                if (null != device)
                {
                    stream = device.OpenStream(0);
                    streamFeatureControl = stream.GetFeatureControl();
                }

                // It is recommended that the user set the camera's stream channel packet length value
                // according to the current network environment after turning on 
                // the network camera to improve the collection performance of the network camera. 
                // For the setting method, refer to the following code.
                GX_DEVICE_CLASS_LIST objDeviceClass = device.GetDeviceInfo().GetDeviceClass();
                if (GX_DEVICE_CLASS_LIST.GX_DEVICE_CLASS_GEV == objDeviceClass)
                {
                    // Determine whether the device supports the stream channel packet function.
                    if (true == featureControl.IsImplemented("GevSCPSPacketSize"))
                    {
                        // Get the optimal packet length value of the current network environment
                        uint nPacketSize = stream.GetOptimalPacketSize();
                        // Set the optimal packet length value to the stream channel packet length of the current device.
                        featureControl.GetIntFeature("GevSCPSPacketSize").SetValue(nPacketSize);
                    }
                }

                //Initialize the device parameters.
                InitDevice();

                // Initialize UI widget
                //__InitUI();

                bitmap = new GxBitmap(device, pbCamera);
                // Update the flag that indicates whether the device has been opened.
                //m_bIsOpen = true;

                // Update the enable status of UI Widget
                //__UpdateUI();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InitDevice()
        {
            //Setting the acquisition mode is continuous.
            featureControl?.GetEnumFeature("AcquisitionMode").SetValue("Continuous");
        }

        private void CloseStream()
        {
            try
            {
                //Close stream
                if (null != stream)
                {
                    stream.Close();
                    stream = null;
                    streamFeatureControl = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void CloseDevice()
        {
            try
            {
                //Close device
                if (null != device)
                {
                    device.Close();
                    device = null;
                }
            }
            catch (Exception)
            {
            }
        }

        private void CloseGxDevice()
        {
            try
            {
                try
                {
                    // If the device doesn't stop to capture, it will be stopped to capture at first.
                    if (m_bIsSnap)
                    {
                        if (null != featureControl)
                        {
                            featureControl.GetCommandFeature("AcquisitionStop").Execute();
                            featureControl = null;
                        }
                    }
                }
                catch (Exception)
                {

                }

                m_bIsSnap = false;

                try
                {
                    //Close the acquisition of stream ,unregister the  capture callback function and close stream.
                    if (null != stream)
                    {
                        stream.StopGrab();

                        //Unregister the  capture callback function
                        stream.UnregisterCaptureCallback();
                        stream.Close();
                        stream = null;
                        streamFeatureControl = null;
                    }
                }
                catch (Exception)
                {

                }

                CloseDevice();

                m_bIsOpen = false;

                //Update the enable status of UI Widget
                //__UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartGxDevice()
        {
            try
            {
                if (null != streamFeatureControl)
                {
                    try
                    {
                        //Set StreamBufferHandlingMode
                        streamFeatureControl.GetEnumFeature("StreamBufferHandlingMode").SetValue("OldestFirst");
                    }
                    catch (Exception)
                    {
                    }
                }

                //Open the acquisition of stream.
                if (null != stream)
                {
                    //The first parameter of the function RegisterCaptureCallback is belong to custom defined parameter(it's type must be quotative type).
                    //If want to use the parameter, the user can used it in the delegate function.
                    stream.RegisterCaptureCallback(this, __CaptureCallbackPro);
                    stream.StartGrab();
                }

                // Send the command of starting acquisition.
                featureControl?.GetCommandFeature("AcquisitionStart").Execute();
                m_bIsSnap = true;

                // Update the enable status of UI Widget
                //__UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void __CaptureCallbackPro(object objUserParam, IFrameData objIFrameData)
        {
            try
            {
                ImageShowAndSave(objIFrameData);
            }
            catch (Exception)
            {
            }
        }

        void ImageShowAndSave(IFrameData objIFrameData)
        {
            try
            {
                bitmap.Show(objIFrameData);
            }
            catch (Exception)
            {
            }

            // Check whether is need to save image or not.
            //if (m_bSaveBmpImg.Checked)
            //{
            //    DateTime dtNow = System.DateTime.Now;  // Get the current system time
            //    string strDateTime = dtNow.Year.ToString() + "_"
            //                       + dtNow.Month.ToString() + "_"
            //                       + dtNow.Day.ToString() + "_"
            //                       + dtNow.Hour.ToString() + "_"
            //                       + dtNow.Minute.ToString() + "_"
            //                       + dtNow.Second.ToString() + "_"
            //                       + dtNow.Millisecond.ToString();

            //    string stfFileName = m_strFilePath + "\\" + strDateTime + ".bmp";  // The default name of image will be saved
            //    m_objGxBitmap.SaveBmp(objIFrameData, stfFileName);
            //}
        }
    }
}
