using ashqTech;
using DutyCycle.Csv;
using DutyCycle.Logic;
using DutyCycle.Models;
using DutyCycle.Models.BondTest;
using DutyCycle.Models.Machine;
using Emgu.CV;
using Emgu.CV.Structure;
using ScottPlot;
using ScottPlot.Plottables;
using System.Diagnostics;
using static DutyCycle.Scripts.KeyboardControls;


namespace DutyCycle.Forms.DutyCycle
{
    public partial class DutyCycleForm : Form
    {
        private readonly ComPort port;
        private const int DATA_INTERVAL = 250; //minimum arduino data recieve event interval
        private readonly double dataIntervalInMilliseconds = DATA_INTERVAL / 1000f;

        private List<double> testValues = [];
        private List<double> ticks = [];
        int stopTimerTickCounter = 0;
        private double maxTestValue = 0;
        private double testTime;
        private double testValue;

        private List<BondTest> breakTests = [];
        private List<BondTest> stretchTests = [];
        private List<BondTest> shearTests = [];

        private BondTest test;
        private List<BondTest> selectedTestList;
        private double[] selectedTestPoint;
        private int selectedTestAxis;

        private readonly string emptyCmbString = "Выберите тест";
        private readonly string labelForceCaption = "Макс. усилие, г:";
        private readonly string labelStretchCaption = "Растяжение, %";
        private readonly string breakTestTitle = "Тест на Разрыв";
        private readonly string stretchTestTitle = "Тест на Растяжение";
        private readonly string shearTestTitle = "Тест на Сдвиг";

        private Board board = Singleton.GetInstance().Board;
        private MachineParameters parameters = Singleton.GetInstance().Parameters;
        private TestConditions testConditions = Singleton.GetInstance().TestConditions;

        public DutyCycleForm()
        {
            InitializeComponent();

            KeyPreview = true;

            port = new ComPort();
            try
            {
                port.Open(ComPort.GetLastPortName());
                testTimer.Start();
            }
            catch
            {
                MessageBox.Show("Не удалось открыть COM-порт. Скорее всего он уже подключен, недоступен или вовсе отключен.", "Ошибка COM-порта", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            testTimer.Interval = DATA_INTERVAL;

            nudBreakDelay.Increment = (decimal)dataIntervalInMilliseconds;
            nudBreakDelay.Minimum = (decimal)dataIntervalInMilliseconds * 2;

            plot = testPlot.Plot;
            InitializePlot();

            rbBreakTest.Checked = true;
            UpdateMaximumVelocity();
            LoadTestVelocities();

            OpenCamera();

            SetupXYGroup();
        }

        #region Plot
        private readonly Plot plot;

        private void InitializePlot()
        {
            plot.YLabel("Усилие, г");
            plot.XLabel("Время, с");
            UpdatePlotTitle();
            testPlot.Refresh();
        }

        private void NewPlotScatter()
        {
            plot.Remove<Scatter>();

            var scat = plot.Add.Scatter(ticks, testValues);
            scat.Smooth = true;
            scat.LineWidth = 2;
            scat.MarkerSize = 10;
        }

        private void UpdatePlotTitle()
        {
            if (rbBreakTest.Checked)
                plot.Title(breakTestTitle);
            else if (rbStretchTest.Checked)
                plot.Title(stretchTestTitle);
            else if (rbShearTest.Checked)
                plot.Title(shearTestTitle);
        }
        #endregion

        bool testInProgress = false;

        private void testTimer_Tick(object sender, EventArgs e)
        {
            port.ReadTestValue();
            testValue = port.TestValue;
            tbForceData.Text = testValue.ToString();

            if (!testInProgress) return;

            var board = Singleton.GetInstance().Board;

            testValues.Add(testValue);
            ticks.Add(testTime);
            testTime += dataIntervalInMilliseconds;

            rtbTestValues.AppendText(testValue.ToString());
            rtbTestValues.AppendText("\n");

            plot.Axes.AutoScale();
            testPlot.Refresh();

            if (testValue >= maxTestValue)
                maxTestValue = testValue;
            else
                stopTimerTickCounter++;

            if (
                IsBondBroken()
                || board.IsMaximumReached(selectedTestAxis)
                || IsForceBoundReached()
                )
                StopTest();

            if (test is StretchTest)
                ((StretchTest)test).EndPosition = board.GetAxisActPosition(selectedTestAxis);
        }

        private void StopTest()
        {
            testInProgress = false;
            StopPull();
            //testTimer.Stop();
            EnableInterface();
            testTime = 0;
            stopTimerTickCounter = 0;
            maxTestValue = 0;
            KeyboardControl.blockControls = false;

            if (testValues.Count == 0 || test == null)
                return;

            test.Values.AddRange(testValues);

            selectedTestList.Add(test);

            cmbTests.Items.Add(test);
            tbTestResult.Text = test.Result.ToString();
        }

        #region Basing and Dialogues

        public void StartBasing(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
    "Вы уверены, что хотите совершить базирование?",
    "Подтверждение базирования",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
                StartBasing();
        }

        public void BasingOnStartUp()
        {
            DialogResult result = MessageBox.Show(
    "Совершить базирование при первом запуске?",
    "Подтверждение базирования",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
                StartBasing();
        }

        public void StartBasing() => Basing.Start([DisableInterface, () => board.BoardSetHighVelocity(parameters.BasingVelocities)], [Tare, EnableInterface], []);
        #endregion



        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (!Basing.BasingOnStartUpDone)
            {
                DialogResult result = MessageBox.Show(
"Перед началом испытаний необходимо выполнить базирование. Выполнить базирование приводов?",
"Подтверждение базирования",
MessageBoxButtons.YesNo,
MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    StartBasing();
                return;
            }

            if (!Basing.InTestPoint)
            {
                DialogResult result = MessageBox.Show(
    "Для начала теста необходимо находиться в точке теста, совершить перемещение?",
    "Приезд в точку теста не совершён",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    double[] pos = new double[3];
                    if (rbBreakTest.Checked)
                        pos = testConditions.TestPoints.Break;
                    else if (rbStretchTest.Checked)
                        pos = testConditions.TestPoints.Stretch;
                    else if (rbShearTest.Checked)
                        pos = testConditions.TestPoints.Shear;

                    Basing.Start([DisableInterface, () => board.BoardSetHighVelocity(parameters.BasingVelocities)], [Tare, EnableInterface], [], pos[0], pos[1], pos[2]);
                }
                return;
            }

            testInProgress = true;

            KeyboardControl.blockControls = false;
            rtbTestValues.Clear();
            tbTestResult.Clear();
            testValues.Clear();
            ticks.Clear();
            DisableInterface();
            cmbTests.Text = emptyCmbString;

            SetTestVelocity();
            test = CreateEmptyTest();

            if (test is StretchTest)
            {
                var board = Singleton.GetInstance().Board;
                ((StretchTest)test).StartPosition = board.GetAxisActPosition(selectedTestAxis);
                ((StretchTest)test).TestSpeed = (double)nudTestSpeed.Value;
                ((StretchTest)test).DelayTimeInSeconds = (double)nudBreakDelay.Value;
            }

            Thread.Sleep(500);

            NewPlotScatter();

            StartPull();
        }

        private void ChooseTestType()
        {
            Basing.InTestPoint = false;

            TestPoints testPoints = Singleton.GetInstance().TestConditions.TestPoints;

            selectedTestList = rbBreakTest.Checked ? breakTests :
                rbStretchTest.Checked ? stretchTests : shearTests;

            selectedTestAxis = rbBreakTest.Checked ? 2 :
                rbStretchTest.Checked ? 2 : 0;

            selectedTestPoint = rbBreakTest.Checked ? testPoints.Break :
                rbStretchTest.Checked ? testPoints.Stretch : testPoints.Shear;

            if (rbBreakTest.Checked)
                if (nudTestSpeed.Maximum > (decimal)testVelocities.Break)
                    nudTestSpeed.Value = (decimal)testVelocities.Break;
                else
                    nudTestSpeed.Value = nudTestSpeed.Maximum;

            else if (rbStretchTest.Checked)
                if (nudTestSpeed.Maximum > (decimal)testVelocities.Stretch)
                    nudTestSpeed.Value = (decimal)testVelocities.Stretch;
                else
                    nudTestSpeed.Value = nudTestSpeed.Maximum;
            else if (rbShearTest.Checked)
                if (nudTestSpeed.Maximum > (decimal)testVelocities.Shear)
                    nudTestSpeed.Value = (decimal)testVelocities.Shear;
                else
                    nudTestSpeed.Value = nudTestSpeed.Maximum;

            ChangeLabelCoords();
            ClearInterface();
            foreach (var test in selectedTestList)
            {
                cmbTests.Items.Add(test);
            }
            UpdatePlotTitle();
            plot.Remove<Scatter>();
            testPlot.Refresh();
        }

        private void DisableInterface()
        {
            operatorPanel.Enabled = false;
            KeyboardControl.blockControls = true;
        }

        private void EnableInterface()
        {
            operatorPanel.Enabled = true;
            KeyboardControl.blockControls = false;
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            port.Calibrate();
            Thread.Sleep(250);
            int calibrationWeight = Convert.ToInt32(cmbReferenceWeights.Items[cmbReferenceWeights.SelectedIndex]);
            port.SendReferenceWeight(calibrationWeight);
            //Thread.Sleep(300);
            //MessageBox.Show(port.ReadReferenceWeight());
            calibrationCountdownValue = CALIBRATION_COUNTDOWN_VALUE; // Сбрасываем значение обратного отсчета
            btnCalibrate.Text = calibrationCountdownValue.ToString(); // Устанавливаем начальное значение текста кнопки
            calibrationTimer.Start(); // Запускаем таймер
            DisableInterface();
        }

        private void btnLockUpperBond_Click(object sender, EventArgs e) => TurnOutput(pbOut6, 6, 3);

        private void btnLockLowerBond_Click(object sender, EventArgs e) => TurnOutput(pbOut7, 7, 3);

        private void TurnOutput(PictureBox pictureBoxDO, ushort channel, int axisIndex)
        {
            byte DoValue;
            if (pictureBoxDO.BackColor == System.Drawing.Color.Silver)
                DoValue = 1;
            else
                DoValue = 0;
            board.SetAxisOutputBit(axisIndex, channel, DoValue);
        }

        private void cmbTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbTestValues.Clear();

            var test = selectedTestList[cmbTests.SelectedIndex];

            foreach (var value in test.Values)
                rtbTestValues.AppendText(value.ToString() + '\n');

            tbTestResult.Text = test.Result.ToString();

            DrawSelectedTest(test);
        }

        private BondTest CreateEmptyTest() => rbBreakTest.Checked ? new BreakTest() :
rbStretchTest.Checked ? new StretchTest() : new ShearTest();

        private void DrawSelectedTest(BondTest test)
        {
            double tick = 0;
            ticks.Clear();
            foreach (var value in test.Values)
            {
                tick += dataIntervalInMilliseconds;
                ticks.Add(tick);
            }
            testValues = test.Values;
            NewPlotScatter();
            plot.Axes.AutoScale();
            testPlot.Refresh();
        }

        private void ClearInterface()
        {
            if (rbStretchTest.Checked)
                lblTestResult.Text = labelStretchCaption;
            else
                lblTestResult.Text = labelForceCaption;
            tbTestResult.Clear();
            rtbTestValues.Clear();
            cmbTests.Items.Clear();
            cmbTests.Text = emptyCmbString;
        }

        private void ChangeLabelCoords()
        {
            lblTestPoint0.Text = selectedTestPoint[0].ToString();
            lblTestPoint1.Text = selectedTestPoint[1].ToString();
            lblTestPoint2.Text = selectedTestPoint[2].ToString();
        }

        private bool IsBondBroken()
        {
            int timerTicksToStop = (int)((double)nudBreakDelay.Value / dataIntervalInMilliseconds);
            return stopTimerTickCounter == timerTicksToStop;
        }

        private void BondTestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseCam();
            StopTest();
            port.Close();

        }

        private void Tare()
        {
            testTimer.Stop();
            Thread.Sleep(DATA_INTERVAL);
            //очистка буфера: port.port.DiscardInBuffer(); + DiscardOutBuffer()
            port.TareScale();
            testTimer.Start();
        }

        private void btnTare_Click(object sender, EventArgs e)
        {
            Tare();
        }

        private bool IsForceBoundReached() => cbBoundSet.Checked &&
            (double)nudLoadBound.Value <= testValue;

        public void StopBySpaceKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (test != null)
                {
                    StopTest();
                    test.Terminated = "Да";
                    if (testValues.Count == 0)
                        test.TerminateTest();
                }
            }
        }

        private void rbBreakTest_CheckedChanged(object sender, EventArgs e) => ChooseTestType();

        private void rbStretchTest_CheckedChanged(object sender, EventArgs e) => ChooseTestType();

        private void rbShearTest_CheckedChanged(object sender, EventArgs e) => ChooseTestType();

        private void btnSetTestPoint_Click(object sender, EventArgs e)
        {
            var machine = Singleton.GetInstance();
            var board = Singleton.GetInstance().Board;
            double[] newTestPoint =
            {
                board.GetAxisCommandPosition(0),
                board.GetAxisCommandPosition(1),
                board.GetAxisCommandPosition(2)
            };

            if (rbBreakTest.Checked)
            {
                machine.TestConditions.TestPoints.Break = newTestPoint;
            }
            else if (rbStretchTest.Checked)
            {
                machine.TestConditions.TestPoints.Stretch = newTestPoint;
            }
            else if (rbShearTest.Checked)
            {
                machine.TestConditions.TestPoints.Shear = newTestPoint;
            }

            machine.TestConditions.TestPoints.Save();

            TestPoints testPoints = Singleton.GetInstance().TestConditions.TestPoints;

            selectedTestPoint = rbBreakTest.Checked ? testPoints.Break :
                rbStretchTest.Checked ? testPoints.Stretch : testPoints.Shear;

            ChangeLabelCoords();

        }

        private void btnLoadTestVelocity_Click(object sender, EventArgs e)
        {
            string testName = rbBreakTest.Checked ? "РАЗРЫВ" :
    rbStretchTest.Checked ? "РАСТЯЖЕНИЕ" : "СДВИГ";
            DialogResult result = MessageBox.Show(
$"Вы уверены, что хотите загрузить скорость для теста на {testName}?",
"Подтверждение загрузки",
MessageBoxButtons.YesNo,
MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                UpdateMaximumVelocity();
                LoadTestVelocities();
            }
        }

        private void btnSaveTestVelocity_Click(object sender, EventArgs e)
        {
            string testName = rbBreakTest.Checked ? "РАЗРЫВ" :
                rbStretchTest.Checked ? "РАСТЯЖЕНИЕ" : "СДВИГ";
            DialogResult result = MessageBox.Show(
$"Вы уверены, что хотите сохранить скорость для теста на {testName}?",
"Подтверждение сохранения",
MessageBoxButtons.YesNo,
MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                var machineTestVelocities = Singleton.GetInstance().TestConditions.Velocities;
                if (rbBreakTest.Checked)
                    machineTestVelocities.Break = testVelocities.Break;
                else if (rbStretchTest.Checked)
                    machineTestVelocities.Stretch = testVelocities.Stretch;
                else if (rbShearTest.Checked)
                    machineTestVelocities.Shear = testVelocities.Shear;
                Singleton.GetInstance().TestConditions.Velocities.Save();
            }
        }

        public void StartPull() => Singleton.GetInstance().
Board.StartAxisContinuousMovement(selectedTestAxis, 1);

        public void StopPull() => Singleton.GetInstance().
Board.StopAxisEmg(selectedTestAxis);

        private void btnSaveTests_Click(object sender, EventArgs e) => CsvConverter.SaveToCsv(selectedTestList);

        private void btnLoadTests_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "CSV files (*.csv)|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                List<BondTest> list = CsvConverter.LoadFromCsv(ofd.FileName);
                var firstTestName = list.First();
                if (firstTestName is BreakTest)
                {
                    breakTests = list;
                    rbBreakTest.Checked = true;
                }
                else if (firstTestName is StretchTest)
                {
                    stretchTests = list;
                    rbStretchTest.Checked = true;
                }
                else if (firstTestName is ShearTest)
                {
                    shearTests = list;
                    rbStretchTest.Checked = true;
                }
                if (cmbTests.Items.Count >= 0) cmbTests.SelectedIndex = 0;
            }
            else
                return;
        }

        #region Camera
        private VideoCapture? capture;
        bool camIsActive = false;

        private void CloseCam()
        {
            //capture.Pause();
            //cbEnCross.Checked = false;
            capture?.Dispose();
            capture = null;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = null;
            camIsActive = false;
            //btnOpenCam.Text = "Начать просмотр";
        }

        private void OpenCamera()
        {
            if (camIsActive)
            {
                try
                {
                    if (capture != null)
                        CloseCam();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    if (capture != null)
                    {
                        string secondProjectPath = @"Debug\net8.0\CameraSettings.exe";
                        Process.Start(secondProjectPath);
                        capture.Start();
                    }
                    else
                    {
                        string secondProjectPath = @"Debug\net8.0\CameraSettings.exe";
                        Process.Start(secondProjectPath);
                        Thread.Sleep(1000);
                        capture = new VideoCapture(0);
                        capture.ImageGrabbed += Capture_ImageGrabbed;
                        capture.Start();
                        camIsActive = true;
                        //btnOpenCam.Text = "Закончить просмотр";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Capture_ImageGrabbed(object? sender, EventArgs? e)
        {
            using (Image<Gray, Single> image = new Image<Gray, Single>(1000, 800))
            {
                //try
                //{
                Mat m = new Mat();
                capture?.Retrieve(m);
                pictureBox1.Image = m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                Invoke(new Action(Update));
            }
        }
        #endregion

        #region Servo
        bool driversBlocked = false;  // Controls servo state
        public void BlockDrivers(Button button)
        {
            string btnBlockDriversCaption = "Зафиксировать двигатели";
            string btnUnblockDriversCaption = "Расфиксировать двигатели";
            if (driversBlocked == false)
            {
                board.BoardSetOutputBit(4, 1);
                driversBlocked = true;
                button.Text = btnUnblockDriversCaption;
                btnHome.Enabled = true;
            }
            else
            {
                board.BoardSetOutputBit(4, 0);
                driversBlocked = false;
                button.Text = btnBlockDriversCaption;
                btnHome.Enabled = false;
            }
        }
        #endregion

        private void btnMoveToTestPoint_Click(object sender, EventArgs e)
        {
            double[] pos = new double[3];
            if (rbBreakTest.Checked)
                pos = testConditions.TestPoints.Break;
            else if (rbStretchTest.Checked)
                pos = testConditions.TestPoints.Stretch;
            else if (rbShearTest.Checked)
                pos = testConditions.TestPoints.Shear;

            if (!Basing.BasingOnStartUpDone)
            {
                DialogResult result = MessageBox.Show(
"Перед началом испытаний необходимо выполнить базирование. Выполнить базирование приводов?",
"Подтверждение базирования",
MessageBoxButtons.YesNo,
MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Basing.Start([DisableInterface, () => board.BoardSetHighVelocity(parameters.BasingVelocities)], [() => Basing.Start([DisableInterface], [Tare, EnableInterface], [], pos[0], pos[1], pos[2]), Tare, EnableInterface], []);
                }
            }
            else
                Basing.Start([DisableInterface, () => board.BoardSetHighVelocity(parameters.BasingVelocities)], [Tare, EnableInterface], [], pos[0], pos[1], pos[2]);
        }

        private void SetupXYGroup()
        {
            board.AddToGroup(0);
            board.AddToGroup(1);
            board.SetGroupDriveSpeed(Singleton.GetInstance().Parameters.BasingVelocities[0]);
            board.SetGroupAcc(1000000);
            board.SetGroupDec(1000000);
        }

        private void outsTimer_Tick(object sender, EventArgs e)
        {
            const int phiAxis = 3;
            byte bit6 = board.GetAxisOutputBit(phiAxis, 6);
            byte bit7 = board.GetAxisOutputBit(phiAxis, 7);
            if (bit6 == 1) pbOut6.BackColor = System.Drawing.Color.Green;
            else pbOut6.BackColor = System.Drawing.Color.Silver;

            if (bit7 == 1) pbOut7.BackColor = System.Drawing.Color.Green;
            else pbOut7.BackColor = System.Drawing.Color.Silver;
        }


        private int calibrationCountdownValue;
        private const int CALIBRATION_COUNTDOWN_VALUE = 11;
        private void calibrationTimer_Tick(object sender, EventArgs e)
        {
            if (calibrationCountdownValue > 0)
            {
                calibrationCountdownValue--;
                btnCalibrate.Text = "Установите вес. " + calibrationCountdownValue.ToString();
            }
            else
            {
                calibrationTimer.Stop();
                btnCalibrate.Text = "Калибровка";
                EnableInterface();
            }
        }

        private void DutyCycleForm_Load(object sender, EventArgs e) => BasingOnStartUp();
    }
}
