namespace ashqTech
{


    public class Board
    {
        /// <summary>
        /// Количество осей, используемых платой.
        /// </summary>
        public uint AxesCount;
        public IntPtr GroupHandler = IntPtr.Zero;
        public IntPtr deviceHandler = IntPtr.Zero;
        private IntPtr[] axisHandlers = [];
        public string DeviceName = string.Empty;
        public bool IsOpen { get; private set; }
        public bool IsVirtual { get; private set; }

        /// <summary>
        /// Конструктор для инициализации платы Advantech и её осей
        /// </summary>
        /// <param name="axesCount">Количество осей, инициализируемых платой, null - автоопределение</param>
        /// <param name="boardNumber">Порядковый номер платы в утилите</param>
        public Board(uint? axesCount = null, uint boardNumber = 0)
        {
            if (!IsOpen)
            {
                deviceHandler = DriverControl.GetDeviceHandler(boardNumber, out DeviceName);
                if (axesCount is null)
                    AxesCount = DriverControl.GetAxesCount(deviceHandler);
                else
                    AxesCount = axesCount.Value;
                axisHandlers = DriverControl.InitializeAxes(AxesCount, deviceHandler);
                CheckBoardVirtuality();
                IsOpen = true;
            }
        }

        private void CheckBoardVirtuality() => IsVirtual = DeviceName[0] == 'V';

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
        public IntPtr this[int axisIndex]
        {
            get
            {
                if (IsOpen)
                    return axisHandlers[axisIndex];
                return IntPtr.Zero;
            }
        }
    }
}
