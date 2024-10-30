using Advantech.Motion;
using System.Text;

namespace ashqTech
{
    public class DriverControl
    {
        #region DeviceControls
        static DriverControl() => FetchAvailableDevices();

        /// <summary>
        /// Инициализирует оси платы
        /// </summary>
        /// <param name="axesCount">Количество осей</param>
        /// <param name="deviceHandler">Обработчик платы</param>
        public static IntPtr[] InitializeAxes(uint axesCount, IntPtr deviceHandler)
        {
            IntPtr[] axesHandler = new IntPtr[axesCount];
            for (int i = 0; i < axesCount; i++)
            {
                uint actionResult = Motion.mAcm_AxOpen(deviceHandler, (UInt16)i, ref axesHandler[i]);
                string errorPrefix = "Open Axis";
                CheckAPIError(actionResult, errorPrefix);
                //Set command and actual position of drivers to start (0-point)        
                SetCommandPosition(axesHandler[i], 0);
                SetActualPosition(axesHandler[i], 0);
            }
            return axesHandler;
        }

        /// <summary>
        /// Закрывает обработчик платы
        /// </summary>
        /// <param name="deviceHandler">Обработчик платы</param>
        public static void CloseDevice(ref IntPtr deviceHandler)
        {
            uint actionResult = Motion.mAcm_DevClose(ref deviceHandler);
            string errorPrefix = "Close Board";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Загружает конфигурацию платы
        /// </summary>
        /// <param name="deviceHandler">Обработчик платы</param>
        /// <param name="advantechConfigPath">Путь к файлу конфигурации</param>
        public static void LoadConfig(IntPtr deviceHandler, string advantechConfigPath)
        {
            uint actionResult = Motion.mAcm_DevLoadConfig(deviceHandler, advantechConfigPath);
            string errorPrefix = "Load Config";
            CheckAPIError(actionResult, errorPrefix);
        }

        private static readonly DEV_LIST[] curAvailableDevs = new DEV_LIST[Motion.MAX_DEVICES];
        private static uint deviceCount = 0;
        public static uint DeviceCount { get => deviceCount; }

        public static void FetchAvailableDevices()
        {
            uint actionResult = (uint)Motion.mAcm_GetAvailableDevs(curAvailableDevs, Motion.MAX_DEVICES, ref deviceCount);
            string errorPrefix = "Get Available Devices";
            CheckAPIError(actionResult, errorPrefix);
        }

        public static nint GetDeviceHandler(uint deviceNumber, out string deviceName)
        {
            nint deviceHandle = nint.Zero;
            if (deviceCount == 0) throw new Exception("No Advantech devices found to be opened.");
            uint actionResult = Motion.mAcm_DevOpen(curAvailableDevs[deviceNumber].DeviceNum, ref deviceHandle);
            deviceName = curAvailableDevs[deviceNumber].DeviceName;
            string errorPrefix = "Open Board";
            CheckAPIError(actionResult, errorPrefix);
            return deviceHandle;
        }

        public static uint GetAxesCount(nint deviceHandler)
        {
            uint AxesPerDev = 0;
            uint actionResult = Motion.mAcm_GetU32Property(deviceHandler, (uint)PropertyID.FT_DevAxesCount, ref AxesPerDev);
            string errorPrefix = "Get axes count per device";
            CheckAPIError(actionResult, errorPrefix);
            return AxesPerDev;
        }

        #endregion

        #region SingleAxisControls
        /// <summary>
        /// Движение в точку по одной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик (Handler) оси платы</param>
        /// <param name="position">Значение координаты на оси</param>
        public static void MoveToPoint(IntPtr axisHandler, double position)
        {
            uint actionResult = Motion.mAcm_AxMoveAbs(axisHandler, position);
            string errorPrefix = "PTP Move";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Получить командную координату по выбранной оси в данный момент времени. 
        /// </summary>
        /// <param name="axisHandler">Обработчик (Handler) оси платы</param>
        /// <returns>Командная координата положения привода в данны ймомент времени</returns>
        public static double GetAxisCommandPosition(IntPtr axisHandler)
        {
            double CurrentComandPosition = new();
            uint actionResult = Motion.mAcm_AxGetCmdPosition(axisHandler, ref CurrentComandPosition);
            string errorPrefix = "Get Comand Position";
            CheckAPIError(actionResult, errorPrefix);
            return CurrentComandPosition;
        }

        /// <summary>
        /// Устанавливает значение высокой скорости на выбранной оси
        /// </summary>
        /// <param name="velHigh">Значение скорости</param>
        /// <param name="axisHandler">Обработчик оси</param>
        public static void SetAxisHighVelocity(IntPtr axisHandler, double velHigh)
        {
            uint actionResult = Motion.mAcm_SetF64Property(axisHandler, (uint)PropertyID.PAR_AxVelHigh, velHigh);
            string errorPrefix = "Set high velocity";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Устанавливает значение ускорение на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        /// <param name="acc">Значение ускорения</param>
        public static void SetAxisAcceleration(IntPtr axisHandler, double acc)
        {
            uint actionResult = Motion.mAcm_SetF64Property(axisHandler, (uint)PropertyID.PAR_AxAcc, acc);
            string errorPrefix = "Set high velocity";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Моментальная остановка движения на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        public static void StopContinuousMovementEmg(IntPtr axisHandler)
        {
            uint actionResult = Motion.mAcm_AxStopEmg(axisHandler);
            string errorPrefix = "Axis Emg Stop";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Устанавливает значение замедления на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        /// <param name="value">Значение замедления</param>
        public static void SetAxisDeceleration(IntPtr axisHandler, double value)
        {
            uint actionResult = Motion.mAcm_SetF64Property(axisHandler, (uint)PropertyID.PAR_AxDec, value);
            string errorPrefix = "Set deceleration";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Устанавливает значение плавности на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        /// <param name="value">Значение плавности</param>
        public static void SetAxisJerk(IntPtr axisHandler, double value)
        {
            uint actionResult = Motion.mAcm_SetF64Property(axisHandler, (uint)PropertyID.PAR_AxJerk, value);
            string errorPrefix = "Set the type of velocity profile";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Устанавливает значение низкой (начальной) скорости на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        /// <param name="value">Значение низкой (начальной) скорости</param>
        public static void SetAxisLowVelocity(IntPtr axisHandler, double value)
        {
            uint actionResult = Motion.mAcm_SetF64Property(axisHandler, (uint)PropertyID.PAR_AxVelLow, value);
            string errorPrefix = "Set low velocity";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Включает серводвигатель на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        public static void AxisServoOn(IntPtr axisHandler)
        {
            uint actionResult = Motion.mAcm_AxSetSvOn(axisHandler, 1);
            string errorPrefix = "Servo On";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Выключает серводвигатель на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        public static void AxisServoOff(IntPtr axisHandler)
        {
            uint actionResult = Motion.mAcm_AxSetSvOn(axisHandler, 0);
            string errorPrefix = "Servo Off";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Начинает передвижение в направлении по выбранной оси
        /// </summary>
        /// <param name="axisHandler"></param>
        /// <param name="direction">Направление, 0 - пложительное, 1 - отрицательное</param>
        public static void StartContinuousMovement(IntPtr axisHandler, ushort direction)
        {
            uint actionResult = Motion.mAcm_AxMoveVel(axisHandler, direction);
            string errorPrefix = "Continuous Movement";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Получает код состояния выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        public static ushort GetAxisState(IntPtr axisHandler)
        {
            ushort state = new();
            uint actionResult = Motion.mAcm_AxGetState(axisHandler, ref state);
            string errorPrefix = "Get Axis State";
            CheckAPIError(actionResult, errorPrefix);
            return state;
        }

        /// <summary>
        /// Сбрасывает ошибку на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        public static void ResetAxisError(IntPtr axisHandler)
        {
            uint actionResult = Motion.mAcm_AxResetError(axisHandler);
            string errorPrefix = "Reset axis's error";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Дополнительно проезжает distance пунктов по выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        /// <param name="distance">Количество пунктов</param>
        public static void AxisMoveRelative(IntPtr axisHandler, double distance)
        {
            uint actionResult = Motion.mAcm_AxMoveRel(axisHandler, distance);
            string errorPrefix = "Ax Move Rel";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Получает значение актуальной позиции (координату) на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        public static double GetAxisActPosition(IntPtr axisHandler)
        {
            double currentActPosition = new double();
            uint actionResult = Motion.mAcm_AxGetActualPosition(axisHandler, ref currentActPosition);
            string errorPrefix = "Get Comand Position";
            CheckAPIError(actionResult, errorPrefix);
            return currentActPosition;
        }

        /// <summary>
        /// Получает значение высокой (конечной) скорости на оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        /// <returns>Значение высокой (конечной) скорости</returns>
        public static double GetAxisHighVelocity(IntPtr axisHandler)
        {
            double vel = 0;
            uint actionResult = Motion.mAcm_GetF64Property(axisHandler, (uint)PropertyID.PAR_AxVelHigh, ref vel);
            string errorPrefix = "get vel high";
            CheckAPIError(actionResult, errorPrefix);
            return vel;
        }

        /// <summary>
        /// Устанавливает значение актуальной координаты на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        /// <param name="position">Устанавливаемое значение актуальной координаты на оси</param>
        public static void SetActualPosition(IntPtr axisHandler, double position)
        {
            uint actionResult = Motion.mAcm_AxSetActualPosition(axisHandler, position);
            string errorPrefix = "Set actual position";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Устанавливает значение командной координаты на выбранной оси
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        /// <param name="position">Устанавливаемое значение командной координаты на оси</param>
        public static void SetCommandPosition(IntPtr axisHandler, double position)
        {
            uint actionResult = Motion.mAcm_AxSetCmdPosition(axisHandler, position);
            string errorPrefix = "Set command position";
            CheckAPIError(actionResult, errorPrefix);
        }

        public static void AxisMoveHome(IntPtr axisHandler, uint homeMode, uint dirMode)
        {
            uint actionResult = Motion.mAcm_AxHome(axisHandler, homeMode, dirMode);
            string errorPrefix = "AxHome";
            CheckAPIError(actionResult, errorPrefix);
        }

        public static uint GetIOStatus(IntPtr axisHandler)
        {
            UInt32 IOStatus = new UInt32();
            uint actionResult = Motion.mAcm_AxGetMotionIO(axisHandler, ref IOStatus);
            string errorPrefix = "Get IO status";
            CheckAPIError(actionResult, errorPrefix);
            return IOStatus;
        }

        public static byte GetAxisOutputBit(IntPtr axisHandler, ushort chanell)
        {
            byte bitDo = 0;
            uint actionResult = Motion.mAcm_AxDoGetBit(axisHandler, chanell, ref bitDo);
            string errorPrefix = "Axis Get Output Bit";
            CheckAPIError(actionResult, errorPrefix);
            return bitDo;
        }

        public static void SetAxisOutputBit(IntPtr axisHandler, ushort channel, byte bitData)
        {
            uint actionResult = Motion.mAcm_AxDoSetBit(axisHandler, channel, bitData);
            string errorPrefix = "Axis Set Output Bit";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <param name="bit">IMMED_STOP = 0, DEC_TO_STOP = 1</param>
        public static void SetIn1StopReactBit(IntPtr axisHandler, int bit)
        {
            uint actionResult = Motion.mAcm_SetI32Property(axisHandler, (uint)PropertyID.CFG_AxIN1StopReact, bit);
            string errorPrefix = "Axis Set Input Bit: StopReact";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <param name="bit">STOP_ACT_LOW = 0, STOP_ACT_HIGH = 1</param>
        public static void SetIn1StopLogicBit(IntPtr axisHandler, int bit)
        {

            uint actionResult = Motion.mAcm_SetI32Property(axisHandler, (uint)PropertyID.CFG_AxIN1StopLogic, bit);
            string errorPrefix = "Axis Set Input Bit: StopLogic";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <param name="bit">STOP_DISABLE = 0, STOP_ENABLE = 1</param>
        public static void SetIn1StopEnableBit(IntPtr axisHandler, int bit)
        {
            uint actionResult = Motion.mAcm_SetI32Property(axisHandler, (uint)PropertyID.CFG_AxIN1StopEnable, bit);
            string errorPrefix = "Axis Set Input Bit: StopEnable";
            CheckAPIError(actionResult, errorPrefix);
        }

        public static byte GetDiBit(IntPtr axisHandler, ushort chanell)
        {
            byte bit = new();
            uint actionResult = Motion.mAcm_AxDiGetBit(axisHandler, chanell, ref bit);
            string errorPrefix = "Get DI bit";
            CheckAPIError(actionResult, errorPrefix);
            return bit;
        }

        public static double GetAxisLowVelocity(IntPtr axisHandler)
        {
            double vel = new();
            uint actionResult = Motion.mAcm_GetF64Property(axisHandler, (uint)PropertyID.PAR_AxVelLow, ref vel);
            string errorPrefix = "get vel low";
            CheckAPIError(actionResult, errorPrefix);
            return vel;
        }

        public static double GetAxisAcc(IntPtr axisHandler)
        {
            double acc = new();
            uint actionResult = Motion.mAcm_GetF64Property(axisHandler, (uint)PropertyID.PAR_AxAcc, ref acc);
            string errorPrefix = "get acc";
            CheckAPIError(actionResult, errorPrefix);
            return acc;
        }

        public static double GetAxisJerk(IntPtr axisHandler)
        {
            double jerk = new();
            uint actionResult = Motion.mAcm_GetF64Property(axisHandler, (uint)PropertyID.PAR_AxJerk, ref jerk);
            string errorPrefix = "get jerk";
            CheckAPIError(actionResult, errorPrefix);
            return jerk;
        }

        #endregion

        #region GroupControls
        /// <summary>
        /// Создаёт (обновляет) группу с выбранной осью
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        /// <param name="interpolationHandler">Обработчик группы</param>
        public static void AddToGroup(IntPtr axisHandler, ref IntPtr interpolationHandler)
        {
            uint actionResult = Motion.mAcm_GpAddAxis(ref interpolationHandler, axisHandler);
            string errorPrefix = "Add Axis To Group";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Убирает выбранную ось из группы
        /// </summary>
        /// <param name="axisHandler">Индекс оси</param>
        /// <param name="interpolationHandler">Обработчик группы</param>
        public static void RemoveAxisFromGroup(IntPtr axisHandler, IntPtr interpolationHandler)
        {
            Motion.mAcm_GpRemAxis(interpolationHandler, axisHandler);
        }

        /// <summary>
        /// Начинает движение группы в заданную позицию
        /// </summary>
        /// <param name="positions">Массив конечных позиций для каждой из осей</param>
        /// <param name="interpolationHandler">Обработчик группы</param>
        public static void MoveGroupAbsolute(double[] positions, IntPtr interpolationHandler)
        {
            uint actionResult = Motion.mAcm_GpMoveLinearAbs(interpolationHandler, positions);
            string errorPrefix = "Interpolation Group Move";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Останавливает движение группы
        /// </summary>
        /// <param name="interpolationHandler">Обработчик группы</param>
        public static void StopGroupMovement(IntPtr interpolationHandler)
        {
            uint actionResult = Motion.mAcm_GpStopEmg(interpolationHandler);
            //string errorPrefix = "Interpolation Group stop";
            //CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Получает код состояния группы (enum Advantech.Motion.GroupState)
        /// </summary>
        /// <param name="interpolationHandler"></param>
        /// <returns>Код состояния группы</returns>
        public static ushort GetGroupState(IntPtr interpolationHandler)
        {
            ushort GpState = new();
            uint actionResult = Motion.mAcm_GpGetState(interpolationHandler, ref GpState);
            string errorPrefix = "get interpolation state";
            CheckAPIError(actionResult, errorPrefix);
            return GpState;
        }

        /// <summary>
        /// Устанавливает значение конечной скорости группы
        /// </summary>
        /// <param name="interpolationHandler">Обработчик группы</param>
        /// <param name="value">Значение скорости</param>
        public static void SetGroupVelHigh(IntPtr interpolationHandler, double value)
        {
            uint actionResult = Motion.mAcm_SetF64Property(interpolationHandler, (uint)PropertyID.PAR_GpVelHigh, value);
            string errorPrefix = "Group Set High Velocity";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Устанавливает значение начальной скорости группы
        /// </summary>
        /// <param name="interpolationHandler">Обработчик группы</param>
        /// <param name="value">Значение скорости</param>
        public static void SetGroupVelLow(IntPtr interpolationHandler, double value)
        {
            uint actionResult = Motion.mAcm_SetF64Property(interpolationHandler, (uint)PropertyID.PAR_GpVelLow, value);
            string errorPrefix = "Group Set Low Velocity";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Устанавливает значение ускорения группы
        /// </summary>
        /// <param name="interpolationHandler">Обработчик группы</param>
        /// <param name="value">Значение ускорения</param>
        public static void SetGroupAcc(IntPtr interpolationHandler, double value)
        {
            uint actionResult = Motion.mAcm_SetF64Property(interpolationHandler, (uint)PropertyID.PAR_GpAcc, value);
            string errorPrefix = "Group Set Acceleration";
            CheckAPIError(actionResult, errorPrefix);
        }

        /// <summary>
        /// Устанавливает значение замедления группы
        /// </summary>
        /// <param name="interpolationHandler">Обработчик группы</param>
        /// <param name="value">Значение замедления</param>
        public static void SetGroupDec(IntPtr interpolationHandler, double value)
        {
            uint actionResult = Motion.mAcm_SetF64Property(interpolationHandler, (uint)PropertyID.PAR_GpDec, value);
            string errorPrefix = "Group Set Acceleration";
            CheckAPIError(actionResult, errorPrefix);
        }
        #endregion

        #region ErrorChecker
        private static void CheckAPIError(uint actionResult, string errorPrefix)
        {
            if (actionResult != (uint)ErrorCode.SUCCESS)
            {
                string errorMessage = $"{errorPrefix} Failed With Error Code: {actionResult}";
                StringBuilder ErrorMsg = new(string.Empty, 100);
                //Get the error message according to error code returned from API
                Motion.mAcm_GetErrorMessage(actionResult, ErrorMsg, 100);
                string errorDescription = string.Empty;
                errorDescription = ErrorMsg.ToString();
                throw new Exception(errorMessage + "\r\nError Message:" + errorDescription);
            }
        }
        #endregion
    }
}
