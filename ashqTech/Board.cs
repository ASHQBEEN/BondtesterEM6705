namespace ashqTech
{

    /// <summary>
    /// Конструктор для инициализации платы Advantech и её осей
    /// </summary>
    /// <param name="AxesCount">Количество осей, инициализируемых платой</param>
    ///
    public class Board(int AxesCount)
    {
        /// <summary>
        /// Количество осей, используемых платой.
        /// </summary>
        public readonly int AxesCount = AxesCount;
        public IntPtr GroupHandler = IntPtr.Zero;
        public IntPtr deviceHandler = IntPtr.Zero;
        private IntPtr[] axisHandlers = [];
        public string DeviceName = string.Empty;
        private readonly uint boardNumber = 0;
        public bool IsOpen { get; private set; }
        public bool IsVirtual { get; private set; }

        /// <summary>
        /// Конструктор для инициализации платы Advantech и её осей c учётом 
        /// порядкового номера платы (для машин с несколькими платами)
        /// </summary>
        /// <param name="boardNumber">Порядковый номер платы</param>
        /// <param name="AxesCount">Количество осей, инициализируемых платой</param>
        ///
        public Board(int AxesCount, uint boardNumber) : this(AxesCount) => this.boardNumber = boardNumber;

        public void OpenBoard()
        {
            if (!IsOpen)
            {
                deviceHandler = DriverControl.OpenDevice(boardNumber, out DeviceName);
                CheckBoardVirtuality();
                axisHandlers = DriverControl.InitializeAxes(AxesCount, deviceHandler);
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
