namespace ashqTech
{
    public static class BoardExtensions
    {
        #region SingleAxisControls
        /// <summary>
        /// Устанавливает значение актуальной координаты на выбранной оси на 0
        /// </summary>
        public static void ResetActPosition(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetActualPosition(board[axisIndex], 0);
        }

        /// <summary>
        /// Устанавливает значение командной координаты на выбранной оси на 0
        /// </summary>
        /// <param name="axisHandler">Обработчик оси</param>
        public static void ResetCommandPosition(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetCommandPosition(board[axisIndex], 0);
        }

        /// <summary>
        /// Движение в точку по одной оси
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        /// <param name="position">Значение координаты на оси</param>
        public static void MoveToPoint(this Board board, int axisIndex, double position)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.MoveToPoint(board[axisIndex], position);
        }

        /// <summary>
        /// Получить командную координату по выбранной оси в данный момент времени. 
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        /// <returns>Командная координата положения привода в данны ймомент времени</returns>
        public static double GetAxisCommandPosition(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            return DriverControl.GetAxisCommandPosition(board[axisIndex]);
        }

        /// <summary>
        /// Устанавливает значение высокой скорости на выбранной оси
        /// </summary>
        /// <param name="velocity">Значение скорости</param>
        /// <param name="axisIndex">Номер оси платы</param>
        public static void SetAxisHighVelocity(this Board board, int axisIndex, double velocity)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetAxisHighVelocity(board[axisIndex], velocity);
        }

        /// <summary>
        /// Устанавливает значение ускорение на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        /// <param name="acc">Значение ускорения</param>
        public static void SetAxisAcceleration(this Board board, int axisIndex, double acc)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetAxisAcceleration(board[axisIndex], acc);
        }

        /// <summary>
        /// Моментальная остановка движения на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        public static void StopAxisEmg(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.StopContinuousMovementEmg(board[axisIndex]);
        }

        /// <summary>
        /// Устанавливает значение замедления на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        /// <param name="dec">Значение замедления</param>
        public static void SetAxisDeceleration(this Board board, int axisIndex, double dec)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetAxisDeceleration(board[axisIndex], dec);
        }

        /// <summary>
        /// Устанавливает значение плавности на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        /// <param name="jerk">Значение плавности</param>
        public static void SetAxisJerk(this Board board, int axisIndex, double jerk)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetAxisJerk(board[axisIndex], jerk);
        }

        /// <summary>
        /// Устанавливает значение низкой (начальной) скорости на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        /// <param name="vel">Значение низкой (начальной) скорости</param>
        public static void SetAxisLowVelocity(this Board board, int axisIndex, double vel)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetAxisLowVelocity(board[axisIndex], vel);
        }

        /// <summary>
        /// Включает серводвигатель на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        public static void AxisServoOn(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.AxisServoOn(board[axisIndex]);
        }

        /// <summary>
        /// Выключает серводвигатель на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        public static void AxisServoOff(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.AxisServoOff(board[axisIndex]);
        }

        /// <summary>
        /// Начинает передвижение в направлении по выбранной оси
        /// </summary>
        /// <param name="axisHandler"></param>
        /// <param name="direction">Направление, 0 - пложительное, 1 - отрицательное</param>
        public static void StartAxisContinuousMovement(this Board board, int axisIndex, ushort direction)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.StartContinuousMovement(board[axisIndex], direction);
        }

        /// <summary>
        /// Получает код состояния выбранной оси
        /// </summary>
        /// <param name="axisIndex">Обработчик оси</param>
        public static ushort GetAxisState(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            return DriverControl.GetAxisState(board[axisIndex]);
        }

        /// <summary>
        /// Сбрасывает ошибку на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Обработчик оси</param>
        public static void ResetAxisError(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.ResetAxisError(board[axisIndex]);
        }

        /// <summary>
        /// Дополнительно проезжает distance пунктов по выбранной оси
        /// </summary>
        /// <param name="axisIndex">Обработчик оси</param>
        /// <param name="distance">Количество пунктов</param>
        public static void AxisMoveRelative(this Board board, int axisIndex, double distance)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.AxisMoveRelative(board[axisIndex], distance);
        }

        /// <summary>
        /// Получает значение актуальной позиции (координату) на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Обработчик оси</param>
        public static double GetAxisActPosition(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            return DriverControl.GetAxisActPosition(board[axisIndex]);
        }

        /// <summary>
        /// Получает значение высокой (конечной) скорости на оси
        /// </summary>
        /// <param name="axisIndex">Обработчик оси</param>
        /// <returns>Значение высокой (конечной) скорости</returns>
        public static double GetAxisHighVelocity(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            return DriverControl.GetAxisHighVelocity(board[axisIndex]);
        }

        /// <summary>
        /// Устанавливает значение актуальной координаты на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Обработчик оси</param>
        /// <param name="position">Устанавливаемое значение актуальной координаты на оси</param>
        public static void SetActualPosition(this Board board, int axisIndex, double position)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetActualPosition(board[axisIndex], position);
        }

        /// <summary>
        /// Устанавливает значение командной координаты на выбранной оси
        /// </summary>
        /// <param name="axisIndex">Обработчик оси</param>
        /// <param name="position">Устанавливаемое значение командной координаты на оси</param>
        public static void SetCommandPosition(this Board board, int axisIndex, double position)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetCommandPosition(board[axisIndex], position);
        }

        public static void AxisMoveHome(this Board board, int axisIndex, uint homeMode, uint dirMode)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.AxisMoveHome(board[axisIndex], homeMode, dirMode);
        }

        /// <summary>
        /// Получить командные координаты всех осей, инициализированных платой
        /// </summary>
        public static double[] BoardGetCommandPositions(this Board board)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            int axesCount = board.AxesCount;
            double[] result = new double[axesCount];
            for (int i = 0; i < axesCount; i++)
            {
                result[i] = DriverControl.GetAxisCommandPosition(board[i]);
            }
            return result;
        }

        /// <summary>
        /// Устанавливает значение скорости для каждой оси
        /// </summary>
        /// <param name="velHigh">Массив значений скоростей</param>
        public static void BoardSetHighVelocity(this Board board, double[] velHigh)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
            {
                DriverControl.SetAxisHighVelocity(board[i], velHigh[i]);
            }
        }

        /// <summary>
        /// Устанавливает значение ускорения для каждой оси
        /// </summary>
        /// <param name="acc">Массив значений ускорений</param>
        public static void BoardSetAcc(this Board board, double[] acc)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
            {
                DriverControl.SetAxisAcceleration(board[i], acc[i]);
            }
        }

        /// <summary>
        /// Устанавливает значение замедления для каждой оси
        /// </summary>
        /// <param name="decs">Массив значений замедлений</param>
        public static void BoardSetDec(this Board board, double[] decs)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
            {
                DriverControl.SetAxisDeceleration(board[i], decs[i]);
            }
        }

        /// <summary>
        /// Отключает серводвигатели на всех осях платы
        /// </summary>
        /// <param name="board"></param>
        public static void BoardServoOff(this Board board)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
            {
                DriverControl.AxisServoOff(board[i]);
            }
        }

        /// <summary>
        /// Включает серводвигатели на всех осях платы
        /// </summary>
        /// <param name="board"></param>
        public static void BoardServoOn(this Board board)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
            {
                DriverControl.AxisServoOn(board[i]);
            }
        }

        /// <summary>
        /// Останавливает движение на всех осях платы
        /// </summary>
        public static void BoardEmgStop(this Board board)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
            {
                DriverControl.StopContinuousMovementEmg(board[i]);
            }
        }

        /// <summary>
        /// Сбрасывает ошибки на всех осях
        /// </summary>
        /// <param name="board"></param>
        public static void BoardResetErrors(this Board board)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
            {
                DriverControl.ResetAxisError(board[i]);
            }
        }

        /// <summary>
        /// Устанавливает низкие скорости для каждой оси
        /// </summary>
        /// <param name="velLow">Массив скоростей</param>
        public static void BoardSetLowVelocity(this Board board, double[] velLow)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
            {
                DriverControl.SetAxisLowVelocity(board[i], velLow[i]);
            }
        }

        /// <summary>
        /// Устанавливает плавность для каждой оси
        /// </summary>
        /// <param name="jerks">Массив плавностей</param>
        public static void BoardSetJerk(this Board board, double[] jerks)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
            {
                DriverControl.SetAxisJerk(board[i], jerks[i]);
            }
        }

        /// <summary>
        /// Получает актуальные позиции на каждой из осей платы в виде массива
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static double[] BoardGetActPos(this Board board)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            int axesCount = board.AxesCount;
            double[] result = new double[axesCount];
            for (int i = 0; i < axesCount; i++)
            {
                result[i] = DriverControl.GetAxisActPosition(board[i]);
            }
            return result;
        }

        public static byte GetAxisOutputBit(this Board board, int axisIndex, ushort chanell)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            return DriverControl.GetAxisOutputBit(board[axisIndex], chanell);
        }

        public static void SetAxisOutputBit(this Board board, int axisIndex, ushort chanell, byte bitData)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetAxisOutputBit(board[axisIndex], chanell, bitData);
        }

        public static void BoardSetOutputBit(this Board board, ushort chanell, byte bitData)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++)
                DriverControl.SetAxisOutputBit(board[i], chanell, bitData);
        }

        public static byte GetDiBit(this Board board, int axisIndex, ushort chanell)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            return DriverControl.GetDiBit(board[axisIndex], chanell);
        }
        #endregion

        #region GroupControls
        /// <summary>
        /// Устанавливает ведущую скорость группы
        /// </summary>
        /// <param name="board"></param>
        /// <param name="driverVelocity"></param>
        public static void SetGroupDriveSpeed(this Board board, double driverVelocity)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetGroupVelHigh(board.GroupHandler, driverVelocity);
            DriverControl.SetGroupVelLow(board.GroupHandler, driverVelocity - 1); // -1 since VelLow cannot be higher than VelHigh
        }

        /// <summary>
        /// Убирает все оси из группы
        /// </summary>
        public static void ClearGroup(this Board board)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            for (int i = 0; i < board.AxesCount; i++) //3 = максимальное число осей для интерполяции
            {
                DriverControl.RemoveAxisFromGroup(board[i], board.GroupHandler);
            }
        }

        /// <summary>
        /// Создаёт (обновляет) группу с выбранной осью
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        public static void AddToGroup(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.AddToGroup(board[axisIndex], ref board.GroupHandler);
        }

        /// <summary>
        /// Убирает выбранную ось из группы
        /// </summary>
        /// <param name="axisIndex">Номер оси платы</param>
        public static void RemoveAxisFromGroup(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.RemoveAxisFromGroup(board[axisIndex], board.GroupHandler);
        }

        /// <summary>
        /// Начинает движение группы в заданную позицию
        /// </summary>
        /// <param name="positions">Массив конечных позиций для каждой из осей</param>
        public static void MoveGroupAbsolute(this Board board, double[] positions)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.MoveGroupAbsolute(positions, board.GroupHandler);
        }

        /// <summary>
        /// Останавливает движение группы
        /// </summary>
        public static void StopGroupMovement(this Board board)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.StopGroupMovement(board.GroupHandler);
        }

        /// <summary>
        /// Получает код состояния группы (enum Advantech.Motion.GroupState)
        /// </summary>
        /// <returns>Код состояния группы</returns>
        public static ushort GetGroupState(this Board board)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            return DriverControl.GetGroupState(board.GroupHandler);
        }

        /// <summary>
        /// Устанавливает значение конечной скорости группы
        /// </summary>
        /// <param name="value">Значение скорости</param>
        public static void SetGroupVelHigh(this Board board, double value)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetGroupVelHigh(board.GroupHandler, value);
        }

        /// <summary>
        /// Устанавливает значение начальной скорости группы
        /// </summary>
        /// <param name="value">Значение скорости</param>
        public static void SetGroupVelLow(this Board board, double value)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetGroupVelLow(board.GroupHandler, value);
        }

        /// <summary>
        /// Устанавливает значение ускорения группы
        /// </summary>
        /// <param name="value">Значение ускорения</param>
        public static void SetGroupAcc(this Board board, double value)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetGroupAcc(board.GroupHandler, value);
        }

        /// <summary>
        /// Устанавливает значение замедления группы
        /// </summary>
        /// <param name="value">Значение замедления</param>
        public static void SetGroupDec(this Board board, double value)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetGroupDec(board.GroupHandler, value);
        }
        #endregion
    }
}
