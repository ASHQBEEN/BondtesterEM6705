using ashqTech;
using DutyCycle.Models.Machine;

namespace DutyCycle.Forms.DutyCycle
{
    public partial class DutyCycleForm
    {
        private readonly double basingDistance = 1000;
        private readonly int phiBasingDistance = 200;
        private readonly int ticksBeforeStop = 30000;

        private const ushort STATE_HOMING = (ushort)AxisState.STA_AX_HOMING;
        private const ushort STATE_MOVING = (ushort)AxisState.STA_AX_PTP_MOT;

        private int startTime;
        private int basingTickerState = 0;
        private bool basingOnStartUpDone = false;

        public void StartBasing(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
    "Вы уверены, что хотите совершить базирование?",
    "Подтверждение базирования",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
                Basing();
        }

        public void BasingOnStartUp()
        {
            DialogResult result = MessageBox.Show(
    "Совершить базирование при первом запуске?",
    "Подтверждение базирования",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
                Basing();
        }

        public void BasingBeforeFirstTestPoint()
        {
            if (!basingOnStartUpDone)
            {
                DialogResult result = MessageBox.Show(
"Перед началом испытаний необходимо выполнить базирование. Выполнить базирование приводов?",
"Подтверждение базирования",
MessageBoxButtons.YesNo,
MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                    Basing();
            }
            else
                ToTestPoint();
        }

        public void Basing()
        {
            DisableInterface();
            startTime = Environment.TickCount;
            basingTickerState = 1;
            board.BoardSetHighVelocity(parameters.BasingVelocities);
            timerBasing.Start();
        }

        private void BasingTimerTick(object sender, EventArgs e)
        {
            if (basingTickerState == 0)
            {
                BasingStop();
                timerBasing.Stop();
            }
            BasingTick();
        }

        public void BasingTick()
        {
            switch (basingTickerState)
            {
                //Базирование Z
                case 1:
                    board.AxisMoveHome(2, 1, 1);
                    basingTickerState++;
                    break;
                case 2:
                    if (Environment.TickCount - startTime > ticksBeforeStop)
                    {
                        SensorNotFoundStop(2);
                        break;
                    }
                    if (board.GetAxisState(2) == STATE_HOMING) break;
                    basingTickerState++;
                    break;
                case 3:
                    board.AxisMoveRelative(2, basingDistance);
                    basingTickerState++;
                    break;
                case 4:
                    if (board.GetAxisState(2) == STATE_MOVING) break;
                    board.ResetCommandPosition(2);
                    board.ResetActPosition(2);
                    basingTickerState++;
                    Singleton.GetInstance().AxisZBasingDone = true;
                    break;
                //Базирование X
                case 5:
                    board.AxisMoveHome(0, 1, 1);
                    basingTickerState++;
                    break;
                case 6:
                    if (Environment.TickCount - startTime > ticksBeforeStop)
                    {
                        SensorNotFoundStop(0);
                        break;
                    }
                    if (board.GetAxisState(0) == STATE_HOMING) break;
                    basingTickerState++;
                    break;
                case 7:
                    board.AxisMoveRelative(0, basingDistance);
                    basingTickerState++;
                    break;
                case 8:
                    if (board.GetAxisState(0) == STATE_MOVING) break;
                    basingTickerState++;
                    board.ResetCommandPosition(0);
                    board.ResetActPosition(0);
                    break;
                //Базирование Y
                case 9:
                    board.AxisMoveHome(1, 1, 1);
                    basingTickerState++;
                    break;
                case 10:
                    if (Environment.TickCount - startTime > ticksBeforeStop)
                    {
                        SensorNotFoundStop(1);
                        break;
                    }
                    if (board.GetAxisState(1) == STATE_HOMING) break;
                    basingTickerState++; break;
                case 11:
                    board.AxisMoveRelative(1, basingDistance);
                    basingTickerState++;
                    break;
                case 12:
                    if (board.GetAxisState(1) == STATE_MOVING) break;
                    board.ResetCommandPosition(1);
                    board.ResetActPosition(1);
                    basingTickerState++;
                    break;
                //Базирование φ по IN1
                case 13:
                    board.StartAxisContinuousMovement(3, 1);
                    basingTickerState++;
                    break;
                case 14:
                    if (Environment.TickCount - startTime > ticksBeforeStop)
                    {
                        SensorNotFoundStop(3);
                        break;
                    }
                    if (board.GetDiBit(3, 1) == 1) break;
                    basingTickerState++;
                    board.StopAxisEmg(3);
                    break;
                case 15:
                    board.AxisMoveRelative(3, phiBasingDistance);
                    basingTickerState++;
                    break;
                case 16:
                    if (board.GetAxisState(3) == STATE_MOVING) break;
                    board.ResetCommandPosition(3);
                    board.ResetActPosition(3);
                    //Если не выполнено базирование при выезде на точку теста в первый раз
                    //if (!basingOnStartUpDone)
                    //    basingTickerState++;
                    //else
                        //Если выполнено - остановка
                        basingTickerState = 0;
                    basingOnStartUpDone = true;
                    break;
                //Выезд на точку теста
                //case 17:
                //    ToTestPoint();
                //    break;
            }
        }

        private void SensorNotFoundStop(int axisIndex)
        {
            BasingStop();
            MessageBox.Show($"Не удалось обнаружить датчик ИП для оси {axisIndex}");
        }

        private void BasingStop()
        {
            basingTickerState = 0;
            EnableInterface();
            board.BoardEmgStop();
        }
    }
}
