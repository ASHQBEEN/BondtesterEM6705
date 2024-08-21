namespace ashqTech
{
    public class Board
    {
        /// <summary>
        /// Номер устройства в Advantech.Motion.DeviceTypeID
        /// </summary>
        private readonly uint deviceType;
        /// <summary>
        /// ID платы в утилите Common Motion Utility</param>
        /// </summary>
        private readonly uint boardID;
        /// <summary>
        /// Количество осей, используемых платой.
        /// </summary>
        public readonly int AxesCount;
        public IntPtr GroupHandler = IntPtr.Zero;
        public IntPtr deviceHandler = IntPtr.Zero;
        private IntPtr[] axisHandlers = [];
        public bool IsOpen { get; private set; } = false;

        /// <summary>
        /// Конструктор для инициализации платы Advantech и её осей
        /// </summary>
        /// <param name="DeviceType">Номер устройства в Advantech.Motion.DeviceTypeID</param>
        /// <param name="BoardID">ID платы в утилите Common Motion Utility</param>
        /// <param name="AxesCount">Количество осей, инициализируемых платой</param>
        ///
        public Board(uint DeviceType, uint BoardID, int AxesCount) 
        {
            deviceType = DeviceType;
            boardID = BoardID;
            this.AxesCount = AxesCount;
        }

        public void OpenBoard()
        {
            if (!IsOpen)
            {
                deviceHandler = DriverControl.InitializeDeviceHandler(deviceType, boardID);
                axisHandlers = DriverControl.InitializeAxes(AxesCount, deviceHandler);
                IsOpen = true;
            }
        }

        public void CloseBoard()
        {
            if (IsOpen)
            {
                DriverControl.CloseDevice(ref deviceHandler);
                IsOpen = false;
            }
        }

        /// <summary>
        /// Индексатор для обращения к определённой оси.
        /// </summary>
        /// <param name="axisIndex">Индекс оси (начиная с 0).</param>
        /// <returns>Обработчик (Handler) выбранной оси.</returns>
        public IntPtr this[int axisIndex] { 
            get 
            { 
                if(IsOpen) 
                    return axisHandlers[axisIndex];
                return IntPtr.Zero;
            }
        }
    }
}
