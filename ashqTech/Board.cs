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
        private string deviceName = string.Empty;
        public string BoardName { get => deviceName; }
        public bool IsOpen { get; private set; }
        public bool IsVirtual { get; private set; }

        private void InitializeBoard(uint boardNumber, uint? axesCount = null)
        {
            if (!IsOpen)
            {
                deviceHandler = DriverControl.GetDeviceHandler(boardNumber, out deviceName);
                AxesCount = axesCount ?? DriverControl.GetAxesCount(deviceHandler);
                axisHandlers = DriverControl.InitializeAxes(AxesCount, deviceHandler);
                IsVirtual = deviceName[0..2] == "V_";
                IsOpen = true;
            }
        }

        /// <summary>
        /// Конструктор для инициализации платы Advantech и её осей (первая плата в утилите с автоопределением осей)
        /// </summary>
        public Board() : this(0) { }

        /// <summary>
        /// Конструктор для инициализации платы Advantech и её осей
        /// </summary>
        /// <param name="boardNumber">Порядковый номер платы в утилите</param>
        public Board(uint boardNumber) => InitializeBoard(boardNumber);

        /// <summary>
        /// Конструктор для инициализации платы Advantech и её осей
        /// </summary>
        /// <param name="boardNumber">Порядковый номер платы в утилите</param>
        /// <param name="axesCount">Количество осей, инициализируемых платой, null - автоопределение</param>
        public Board(uint boardNumber, uint axesCount) => InitializeBoard(boardNumber, axesCount);

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
