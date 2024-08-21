using ashqTech;
using DutyCycle.Models.Machine;

namespace DutyCycle.Logic
{
    public static partial class Basing
    {
        private static readonly Board board = Singleton.GetInstance().Board;
        private static readonly System.Windows.Forms.Timer timer = new();

        private static readonly double basingDistance = 1000;
        private static readonly double phiBasingDistance = 200;
        private static readonly int ticksBeforeStop = 30000;

        private const ushort STATE_HOMING = (ushort)AxisState.STA_AX_HOMING;
        private const ushort STATE_MOVING = (ushort)AxisState.STA_AX_PTP_MOT;

        public static bool AxisZBasingDone { get; set; } = false;
        public static bool BasingOnStartUpDone { get; set; } = false;

        private static bool isInProgress = false;
        private static int state = 0;
        private static int startTime;


        private static List<Action> preconditionMethods = [];
        private static List<Action> postconditionMethods = [];
        private static List<Action> finalMethods = [];

        private static Action basingAction = () => { };

        static Basing() => timer.Tick += TimerTick;

        public static void Start(List<Action> pre, List<Action> post, List<Action> final, double? x = null, double? y = null, double z = 0)
        {
            if (isInProgress)
                return;

            isInProgress = true;

            preconditionMethods = pre;
            postconditionMethods = post;
            finalMethods = final;

            ChooseStrategy(x, y, z);

            ExecutePrecontitions();

            state = 1;
            timer.Start();
        }

        public static void Stop()
        {
            timer.Stop();
            state = 0;
            board.StopGroupMovement();
            board.BoardEmgStop();

            ExecutePostcontitions();

            isInProgress = false;
        }

        private static void ChooseStrategy(double? x = null, double? y = null, double z = 0)
        {
            //Strategy for choosing corresponding basing type
            if (x != null)
                if (y != null)
                    basingAction = () => ToTestPoint(x.Value, y.Value, z);
                else
                    basingAction = () => OneAxisBasing((int)x);
            else
                basingAction = () => AllAxisBasing();
        }

        public static void TimerTick(object? sender, EventArgs? e) =>basingAction.Invoke();

        private static void ExecutePrecontitions() { foreach (var method in preconditionMethods) method.Invoke(); }
        private static void ExecutePostcontitions() { foreach (var method in postconditionMethods) method.Invoke(); }

        private static bool IsSensorFound(int axisIndex)
        {
            if (Environment.TickCount - startTime > ticksBeforeStop)
            {
                Stop();
                MessageBox.Show($"Не удалось обнаружить датчик ИП для оси {axisIndex}");
                return false;
            }
            return true;
        }

        private static void StartHoming(int axisIndex)
        {
            startTime = Environment.TickCount;
            board.AxisMoveHome(axisIndex, 1, 1);
            state++;
        }

        private static bool CheckHomingInProgress(int axisIndex) => board.GetAxisState(axisIndex) == STATE_HOMING;

        private static void StartRebound(int axisIndex, double basingDistance)
        {
            board.AxisMoveRelative(axisIndex, basingDistance);
            state++;
        }

        private static bool CheckReboundInProgress(int axisIndex) => board.GetAxisState(axisIndex) == STATE_MOVING;

        private static void ResetPosition(int axisIndex)
        {
            board.ResetCommandPosition(axisIndex);
            board.ResetActPosition(axisIndex);
            state++;
        }

        private static void StartContinuousMovement(int axisIndex)
        {
            startTime = Environment.TickCount;
            board.StartAxisContinuousMovement(axisIndex, 1);
            state++;
        }

        private static void FinilizeBasing()
        {
            foreach (var method in finalMethods) method.Invoke();
            state = 0;
            Stop();
        }
    }
}
