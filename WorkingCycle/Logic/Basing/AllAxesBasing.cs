using ashqTech;

namespace DutyCycle.Logic
{
    public static partial class Basing
    {
        private static void AllAxisBasing()
        {
            switch (state)
            {
                //Базирование Z - ось 2
                case 1: StartHoming(2); break;
                case 2:
                    if (!IsSensorFound(2)) break;
                    if (CheckHomingInProgress(2)) break;
                    state++; break;
                case 3: StartRebound(2, basingDistance); break;
                case 4:
                    if (CheckReboundInProgress(2)) break;
                    ResetPosition(2); break;
                //Базирование X - ось 0
                case 5: StartHoming(0); break;
                case 6:
                    if (!IsSensorFound(0)) break;
                    if (CheckHomingInProgress(0)) break;
                    state++; break;
                case 7: StartRebound(0, basingDistance); break;
                case 8:
                    if (CheckReboundInProgress(0)) break;
                    ResetPosition(0); break;
                //Базирование Y - ось 1
                case 9: StartHoming(1); break;
                case 10:
                    if (!IsSensorFound(1)) break;
                    if (CheckHomingInProgress(1)) break;
                    state++; break;
                case 11: StartRebound(1, basingDistance); break;
                case 12:
                    if (CheckReboundInProgress(1)) break;
                    ResetPosition(1); break;
                //Базирование φ по IN1
                case 13:
                    StartContinuousMovement(3); break;
                case 14:
                    if (!IsSensorFound(3)) break;
                    if (board.GetDiBit(3, 1) == 1) break;
                    state++;
                    board.StopAxisEmg(3);
                    break;
                case 15:
                    StartRebound(3, phiBasingDistance); break;
                case 16:
                    if (CheckReboundInProgress(3)) break;
                    ResetPosition(3); break;
                case 17:
                    FinilizeBasing();
                    BasingOnStartUpDone = true;
                    break;
            }
        }
    }
}
