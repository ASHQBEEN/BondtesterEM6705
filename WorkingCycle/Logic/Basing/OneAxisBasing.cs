using ashqTech;

namespace DutyCycle.Logic
{
    public static partial class Basing
    {
        private static void OneAxisBasing(int axisIndex)
        {
            switch (state)
            {
                case 1:
                    startTime = Environment.TickCount;
                    if (axisIndex == 3)
                        StartContinuousMovement(axisIndex);
                    else
                        StartHoming(axisIndex);
                    break;
                case 2:
                    if (!IsSensorFound(axisIndex)) break;
                    if (axisIndex != 3)
                        if (CheckHomingInProgress(axisIndex)) break;
                        else { }
                    else
                        if (board.GetDiBit(3, 1) == 1) break;
                    board.StopAxisEmg(axisIndex);
                    state++;
                    break;
                case 3:
                    StartRebound(axisIndex, axisIndex == 3 ? phiBasingDistance : basingDistance);
                    break;
                case 4:
                    if (CheckReboundInProgress(axisIndex)) break;
                    ResetPosition(axisIndex); break;
                case 5:
                    FinilizeBasing();
                    if (axisIndex == 2)
                        AxisZBasingDone = true;
                    break;

            }
        }
    }
}
