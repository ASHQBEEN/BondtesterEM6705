using ashqTech;
using DutyCycle.Models.Machine;
using static DutyCycle.Scripts.KeyboardControls;

namespace DutyCycle
{
    public partial class VelocitySettings : Form
    {

        private readonly NumericUpDown[] ndSlowVel;
        private readonly NumericUpDown[] ndFastVel;
        private readonly NumericUpDown[] ndDrVel;
        private readonly NumericUpDown[] ndAcc;
        private readonly RadioButton[] rbT;
        private readonly RadioButton[] rbS;
        private readonly NumericUpDown[] ndMaxCoord;
        private readonly TextBox[] tbActPos;
        private readonly TextBox[] tbState;
        private readonly PictureBox[] pbOuts;
        private readonly Button[] btnsFixate;
        private readonly Button[] btnsBasing;
        private readonly Button[] btnsBasingWithoutZ;
        private readonly Button[] btnsResetError;

        private Board board = Singleton.GetInstance().Board;
        private int axesCount = Singleton.GetInstance().Board.AxesCount;

        public VelocitySettings()
        {
            InitializeComponent();

            MachineParameters parameters = Singleton.GetInstance().Parameters;

            ndSlowVel = [ndSlowVel0, ndSlowVel1, ndSlowVel2, ndSlowVel3];
            ndFastVel = [ndFastVel0, ndFastVel1, ndFastVel2, ndFastVel3];
            ndDrVel = [ndDrVel0, ndDrVel1, ndDrVel2, ndDrVel3];
            ndAcc = [ndAcc0, ndAcc1, ndAcc2, ndAcc3];
            rbT = [rbT0, rbT1, rbT2, rbT3];
            rbS = [rbS0, rbS1, rbS2, rbS3];
            ndMaxCoord = [ndMaxCoord0, ndMaxCoord1, ndMaxCoord2, ndMaxCoord3];
            tbActPos = [tbActPos0, tbActPos1, tbActPos2, tbActPos3];
            tbState = [tbState0, tbState1, tbState2, tbState3];
            pbOuts = [pbOut40, pbOut41, pbOut42, pbOut43];
            btnsFixate = [btnFixate0, btnFixate1, btnFixate2, btnFixate3];
            btnsBasing = [btnBasing0, btnBasing1, btnBasing2, btnBasing3];
            btnsBasingWithoutZ = [btnBasing0, btnBasing1, btnBasing3];
            btnsResetError = [btnResetErrors0, btnResetErrors1, btnResetErrors2, btnResetErrors3];

            for (int i = 0; i < 4; i++)
            {
                int index = i;
                EventHandler ehFastToSlow = (o, ea) => ndSlowVel[index].Maximum = ndFastVel[index].Value;
                ndFastVel[i].ValueChanged += ehFastToSlow;
                EventHandler ehDrToFast = (o, ea) => ndFastVel[index].Maximum = ndDrVel[index].Value;
                ndDrVel[i].ValueChanged += ehDrToFast;
                EventHandler ehFixateDrivers = (o, ea) => FixateDriver(index);
                btnsFixate[i].Click += ehFixateDrivers;
                EventHandler ehResetError = (o, ea) => board.ResetAxisError(index);
                btnsResetError[i].Click += ehResetError;

                EventHandler ehBasing = (o, ea) => AxisBasing(index);
                btnsBasing[i].Click += ehBasing;
            }
        }



        private void ParseParams(bool loadVels)
        {
            MachineParameters parameters = Singleton.GetInstance().Parameters;
            //getting or loading virtual values
            if (loadVels)
            {
                parameters.Load();
            }

            for (int i = 0; i < axesCount; i++)
            {
                ndSlowVel[i].Text = Convert.ToString(parameters.SlowVelocity[i]);
                ndFastVel[i].Text = Convert.ToString(parameters.FastVelocity[i]);
                ndAcc[i].Text = Convert.ToString(parameters.Acceleration[i]);
                ndDrVel[i].Text = Convert.ToString(parameters.DriverVelocity[i]);
                ndMaxCoord[i].Text = Convert.ToString(parameters.MaxCoordinate[i]);
                if (parameters.Jerk[i] == 0) rbT[i].Checked = true;
                else rbS[i].Checked = true;
            }
        }

        private void VelocitySettings_Load(object sender, EventArgs e)
        {
            MachineParameters parameters = Singleton.GetInstance().Parameters;
            if (parameters.ParametersBeenSet)
            {
                ParseParams(false);
            }
            else
            {
                if (File.Exists(Singleton.GetInstance().configurationJsonPath))
                    ParseParams(true);
                else
                    ParseParams(false);
            }
        }

        private void btnSetParams_Click(object sender, EventArgs e)
        {
            MachineParameters parameters = Singleton.GetInstance().Parameters;
            SetParameters();
            board.BoardSetJerk(parameters.Jerk);
            board.BoardSetAcc(parameters.Acceleration);
            board.BoardSetDec(parameters.Acceleration);
        }

        private void btnSaveParameters_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
"Вы уверены, что хотите сохранить параметры приводов?",
"Подтверждение сохранения",
MessageBoxButtons.YesNo,
MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                MachineParameters parameters = Singleton.GetInstance().Parameters;
                SetParameters();
                parameters.Save();
            }
        }

        private void SetParameters()
        {
            MachineParameters parameters = Singleton.GetInstance().Parameters;
            //buffer values
            double[] jerk = new double[axesCount];
            double[] velDr = new double[axesCount];
            double[] acc = new double[axesCount];
            double[] velManFast = new double[axesCount];
            double[] velManSlow = new double[axesCount];
            double[] maxCoord = new double[axesCount];

            for (int i = 0; i < axesCount; i++)
            {
                if (rbT[i].Checked == true) jerk[i] = 0;
                else jerk[i] = 1;
                acc[i] = Convert.ToDouble(ndAcc[i].Value);
                velDr[i] = Convert.ToDouble(ndDrVel[i].Value);
                velManSlow[i] = Convert.ToDouble(ndSlowVel[i].Value);
                velManFast[i] = Convert.ToDouble(ndFastVel[i].Value);
                maxCoord[i] = Convert.ToDouble(ndMaxCoord[i].Value);
            }

            parameters.Jerk = jerk;
            parameters.Acceleration = acc;
            parameters.SlowVelocity = velManSlow;
            parameters.FastVelocity = velManFast;
            parameters.DriverVelocity = velDr;
            parameters.MaxCoordinate = maxCoord;

            parameters.ParametersBeenSet = true;
        }

        private void btnLoadParameters_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
"Вы уверены, что хотите загрузить параметры приводов?",
"Подтверждение загрузки",
MessageBoxButtons.YesNo,
MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                if (File.Exists(Singleton.GetInstance().configurationJsonPath))
                {
                    ParseParams(true);
                }
                else MessageBox.Show("Вы ещё не сохраняли скорости.");
            }
        }

        private void adjustmentsTimer_Tick(object sender, EventArgs e)
        {
            double[] actPos = Singleton.GetInstance().Board.BoardGetActPos();

            for (int i = 0; i < board.AxesCount; i++)
            {
                tbActPos[i].Text = actPos[i].ToString();
                //tbState[i].Text = ((AxisState)Singleton.GetInstance().Board.GetAxisState(i)).();
                tbState[i].Text = ((AxisState)Singleton.GetInstance().Board.GetAxisState(i)).ToString();
            }

            foreach (var btn in btnsBasingWithoutZ)
            {
                if (Singleton.GetInstance().AxisZBasingDone)
                    btn.Enabled = true;
                else
                    btn.Enabled = false;
            }
        }

        #region FIXATE DRIVERS

        private const int FIXATE_DRIVERS_OUT_CHANELL = 4;

        private void FixateDriver(int axisIndex)
        {
            byte DoValue;
            if (board.GetAxisOutputBit(axisIndex, FIXATE_DRIVERS_OUT_CHANELL) == 1)
                DoValue = 0;
            else
                DoValue = 1;
            board.SetAxisOutputBit(axisIndex, FIXATE_DRIVERS_OUT_CHANELL, DoValue);
        }

        private void driverFixateTimer_Tick(object sender, EventArgs e)
        {
            double[] outBits = new double[4];

            for (int i = 0; i < board.AxesCount; i++)
            {
                string actualCaption;
                string btnUnblockDriversCaption = "Расфиксировать двигатель";
                string btnBlockDriversCaption = "Зафиксировать двигатель";
                outBits[i] = board.GetAxisOutputBit(i, FIXATE_DRIVERS_OUT_CHANELL);
                if (outBits[i] == 1)
                {
                    pbOuts[i].BackColor = Color.Silver;
                    actualCaption = btnBlockDriversCaption;
                }
                else
                {
                    pbOuts[i].BackColor = Color.Green;
                    actualCaption = btnUnblockDriversCaption;
                }
                btnsFixate[i].Text = actualCaption;
            }
        }
        #endregion


        #region one axis basing

        private int oneAxisBasing = 0;
        private readonly double basingDistance = 1000;
        private readonly int ticksBeforeStop = 10000;
        private readonly int phiBasingDistance = 200;

        private const ushort STATE_HOMING = (ushort)AxisState.STA_AX_HOMING;
        private const ushort STATE_MOVING = (ushort)AxisState.STA_AX_PTP_MOT;

        private int startTime;

        private void EnableInterface()
        {
            tableLayoutPanel1.Enabled = true;
            KeyboardControl.blockControls = false;
        }

        private void DisableInterface()
        {
            tableLayoutPanel1.Enabled = false;
            KeyboardControl.blockControls = true;
        }

        private Action<int> action;

        private void AxisBasing(int axisIndex)
        {
            var parameters = Singleton.GetInstance().Parameters;
            board.BoardSetHighVelocity(parameters.BasingVelocities);
            startTime = Environment.TickCount;
            basingAxisIndex = axisIndex;
            if (axisIndex == 3)
                action = new Action<int>(BasingPhiAxisTick);
            else
                action = new Action<int>(BasingAnAxisTick);
            oneAxisTimer.Start();
            DisableInterface();
            oneAxisBasing++;
        }

        private void BasingPhiAxisTick(int axisIndex)
        {
            switch (oneAxisBasing)
            {
                case 1:
                    board.StartAxisContinuousMovement(axisIndex, 1);
                    oneAxisBasing++;
                    break;
                case 2:
                    if (Environment.TickCount - startTime > ticksBeforeStop)
                    {
                        oneAxisBasing = 0;
                        EnableInterface();
                        board.BoardEmgStop();
                        MessageBox.Show($"Не удалось обнаружить датчик ИП для оси {axisIndex}");
                        break;
                    }
                    if (board.GetDiBit(3, 1) == 1) break;
                    oneAxisBasing++;
                    board.StopAxisEmg(3);
                    break;
                case 3:
                    board.AxisMoveRelative(axisIndex, phiBasingDistance);
                    oneAxisBasing++;
                    break;
                case 4:
                    if (board.GetAxisState(axisIndex) == STATE_MOVING) break;
                    board.ResetCommandPosition(axisIndex);
                    board.ResetActPosition(axisIndex);
                    oneAxisBasing = 0;
                    break;
            }
        }

        private void BasingAnAxisTick(int axisIndex)
        {
            switch (oneAxisBasing)
            {
                case 1:
                    board.AxisMoveHome(axisIndex, 1, 1);
                    oneAxisBasing++;
                    break;
                case 2:
                    if (Environment.TickCount - startTime > ticksBeforeStop)
                    {
                        oneAxisBasing = 0;
                        EnableInterface();
                        board.BoardEmgStop();
                        MessageBox.Show($"Не удалось обнаружить датчик ИП для оси {axisIndex}");
                        break;
                    }
                    if (board.GetAxisState(axisIndex) == STATE_HOMING) break;
                    oneAxisBasing++;
                    break;
                case 3:
                    board.AxisMoveRelative(axisIndex, basingDistance);
                    oneAxisBasing++;
                    break;
                case 4:
                    if (board.GetAxisState(axisIndex) == STATE_MOVING) break;
                    board.ResetCommandPosition(axisIndex);
                    board.ResetActPosition(axisIndex);
                    oneAxisBasing = 0;
                    if (axisIndex == 2)
                        Singleton.GetInstance().AxisZBasingDone = true;
                    break;
            }
        }

        private int basingAxisIndex;

        private void oneAxisTimer_Tick(object sender, EventArgs e)
        {
            if (oneAxisBasing == 0)
            {
                EnableInterface();
                board.BoardEmgStop();
                oneAxisTimer.Stop();
            }
            action(basingAxisIndex);
        }


        public void StopSoloAxisBasingBySpaceKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (oneAxisBasing != 0)
                    oneAxisBasing = 0;
            }
        }
        #endregion

        private void btnLoadCfg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileConfig = new OpenFileDialog();
            if (openFileConfig.ShowDialog() == DialogResult.OK)
            {
                if (cbMemCfg.Checked)
                {
                    Singleton.GetInstance().advantechConfigurationPath = openFileConfig.FileName;
                    Singleton.GetInstance().SaveAdvantechConfiguration();
                    Singleton.GetInstance().LoadOverridedConfig();
                }
                else
                {
                    Singleton.GetInstance().LoadOverridedConfig();
                }
                cbMemCfg.Checked = false;
            }
        }
    }
}
