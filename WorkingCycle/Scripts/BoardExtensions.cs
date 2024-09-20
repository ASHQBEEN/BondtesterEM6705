using ashqTech;
using DutyCycle.Models.Machine;

namespace DutyCycle.Scripts
{
    public static class BoardExtensions
    {
        public static void StartAxisContinuousMovementChecked(this Board b, int axisIndex, ushort direction)
        {
            if (b.GetAxisState(axisIndex) == (ushort)AxisState.STA_AX_READY)
            {
                //если максимальная координата задана и нынешняя координата больше максимальной
                if (IfMaximumReached(axisIndex)
                    && direction == 0)
                {
                    MessageBox.Show("Достигнут максимум перемещения.");
                    return;
                }
                b.StartAxisContinuousMovement(axisIndex, direction);
            }
        }

        public static bool IfMaximumReached(int axisIndex)
        {
            var machine = Singleton.GetInstance();
            return machine.Parameters.MaxCoordinate[axisIndex] != 0 ? machine.Parameters.MaxCoordinate[axisIndex]
                <= machine.Board.GetAxisCommandPosition(axisIndex) : false;
        }

        public static void In1Setup(this Board board, int axisIndex)
        {
            if (!board.IsOpen) throw new Exception("Board needs to be opened before any operations");
            DriverControl.SetIn1StopLogicBit(board[axisIndex], 1);
            DriverControl.SetIn1StopReactBit(board[axisIndex], 0);
        }
    }
}
