using ashqTech;

namespace DutyCycle.Forms.DutyCycle
{
    public partial class DutyCycleForm
    {
        double[] pos = new double[3];

        private void ToTestPoint()
        {

            if (rbBreakTest.Checked)
                pos = testConditions.TestPoints.Break;
            else if (rbStretchTest.Checked)
                pos = testConditions.TestPoints.Stretch;
            else if (rbShearTest.Checked)
                pos = testConditions.TestPoints.Shear;

            board.BoardSetHighVelocity(parameters.BasingVelocities);

            DisableInterface();
            toTestPointState = 1;
            toTestPointTimer.Start();
        }

        private System.Windows.Forms.Timer toTestPointTimer = new();
        private int toTestPointState = 0;

        public void ToTestPointTimerTick(object sender, EventArgs e)
        {
            if (toTestPointState == 0)
            {
                ToTestPointStop();
                toTestPointTimer.Stop();
            }
            ToTestPointTick();
        }

        public void ToTestPointStop()
        {
            toTestPointState = 0;
            EnableInterface();
            board.BoardEmgStop();
        }

        public void ToTestPointTick()
        {
            switch (toTestPointState)
            {
                //в точку по XY
                case 1:
                    board.MoveGroupAbsolute([pos[0], pos[1]]);
                    toTestPointState++;
                    break;
                case 2:
                    if (board.GetGroupState() == 4) break; //STA_Gp_Motion
                    toTestPointState++;
                    break;
                //в точку по Z
                case 3:
                    board.MoveToPoint(2, pos[2]);
                    toTestPointState++;
                    break;
                case 4:
                    if (board.GetAxisState(2) == 5) break; //STA_AX_PTP_MOT
                    toTestPointState = 0;
                    break;
            }
        }

    }
}
