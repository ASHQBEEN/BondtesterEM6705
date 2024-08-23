using ashqTech;

namespace DutyCycle.Logic
{
    public static partial class Basing
    {
        public static bool InTestPoint = false;

        private static void ToTestPoint(double positionX, double positionY, double positionZ)
        {
            switch (state)
            {
                //в точку по XY
                case 1:
                    startTime = Environment.TickCount;
                    board.MoveGroupAbsolute([positionX, positionY]);
                    state++;
                    break;
                case 2:
                    if (board.GetGroupState() == 4) break; //STA_Gp_Motion
                    state++;
                    break;
                //в точку по Z
                case 3:
                    board.MoveToPoint(2, positionZ);
                    state++;
                    break;
                case 4:
                    if (CheckReboundInProgress(2)) break; //STA_AX_PTP_MOT
                    state++; break;
                case 5:
                    EndBasing();
                    InTestPoint = true;
                    break;
            }
        }
    }
}
