using ashqTech;

namespace DutyCycle.Models.Machine
{
    public static class BoardExtensions
    {
        public static bool IsMaximumReached(this Board board, int axisIndex)
        {
            var machine = Singleton.GetInstance();
            return machine.Parameters.MaxCoordinate[axisIndex] != 0 ? machine.Parameters.MaxCoordinate[axisIndex]
    <= board.GetAxisCommandPosition(axisIndex) : false;
        }
    }
}
