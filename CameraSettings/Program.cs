// See https://aka.ms/new-console-template for more information
using CameraSettings;

const int delay = 2000;

Control.OpenDevice();
Thread.Sleep(delay);
Control.ApplyParameters();

Control.CloseDevice();