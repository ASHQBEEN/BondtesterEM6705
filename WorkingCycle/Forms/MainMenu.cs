using ashqTech;
using DutyCycle.Forms.DutyCycle;
using DutyCycle.Models.Machine;
using DutyCycle.Logic;
using DutyCycle.Scripts;

namespace DutyCycle.Forms
{
    public partial class MainMenu : Form, IGlobalKeyMessageFilter
    {
        private Board board;
        private DutyCycleForm dutyCycle;
        private DriverSettings velocitySettings;

        PictureBox[] pbNeg;
        PictureBox[] pbPos;
        TextBox[] tbCmdPos;

        public MainMenu()
        {
            KeyPreview = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            InitializeDriver();

            InitializeComponent();

            pbNeg = [pbNeg0, pbNeg1, pbNeg2, pbNeg3];
            pbPos = [pbPos0, pbPos1, pbPos2, pbPos3];
            tbCmdPos = [tbCmdPos0, tbCmdPos1, tbCmdPos2, tbCmdPos3];

            if (Singleton.GetInstance().Board.IsVirtual)
            {
                Basing.InTestPoint = true;
                Basing.BasingOnStartUpDone = true;
                Basing.AxisZBasingDone = true;
            }
        }

        private void InitializeDriver()
        {
            Singleton.GetInstance().LoadAllModulesParameters();
            Singleton.GetInstance().InitializeBoard();
            board = Singleton.GetInstance().Board;
        }

        private void btnAdjustments_Click(object sender, EventArgs e)
        {
            velocitySettings.MdiParent = this;
            velocitySettings.Dock = DockStyle.Fill;
            velocitySettings.Show();
            velocitySettings.Activate();
            if (!Singleton.GetInstance().Board.IsVirtual)
                Basing.AxisZBasingDone = false;
        }

        private void btnDutyCycle_Click(object sender, EventArgs e)
        {
            dutyCycle.MdiParent = this;
            dutyCycle.Dock = DockStyle.Fill;
            dutyCycle.Show();
            dutyCycle.Activate();
        }

        private void posTimer_Tick(object sender, EventArgs e)
        {
            double[] cmdPos = Singleton.GetInstance().Board.BoardGetCommandPositions();

            for (int i = 0; i < board.AxesCount; i++)
            {
                tbCmdPos[i].Text = cmdPos[i].ToString();

                //max coord checker
                if (board.IsMaximumReached(i))
                    board.StopAxisEmg(i);

                if (i == 3) continue;

                //LMT POS XYZ
                uint ioStatus = DriverControl.GetIOStatus(board[i]);
                if ((ioStatus & (uint)AxisIO.AX_MOTION_IO_LMTP) > 0)
                {
                    pbPos[i].BackColor = Color.Red;
                    board.ResetAxisError(i);
                }
                else 
                    pbPos[i].BackColor = Color.Gray;

                //LMT NEG XYZ
                if ((ioStatus & (uint)AxisIO.AX_MOTION_IO_LMTN) > 0)
                {
                    pbNeg[i].BackColor = Color.Red;
                    board.ResetAxisError(i);
                }
                else 
                    pbNeg[i].BackColor = Color.Gray;
            }

            //LMT PHI
            if(board.GetDiBit(3, 1) == 0)
            {
                pbNeg[3].BackColor = Color.Red;
                board.ResetAxisError(3);
            }
            else
                pbNeg[3].BackColor = Color.Gray;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            var machine = Singleton.GetInstance();
            if (File.Exists(machine.AdvantechConfigurationPath))
                machine.LoadOverridedConfig();
            else
            {
                DialogResult cfgDlg = MessageBox.Show("Конфигурационный файл не был обнаружен, укажите к нему путь." +
                    "\nИначе будут использованы параметры по умолчанию.", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (cfgDlg == DialogResult.OK)
                {
                    OpenFileDialog openFileConfig = new OpenFileDialog();
                    if (openFileConfig.ShowDialog() == DialogResult.OK)
                    {
                        machine.AdvantechConfigurationPath = openFileConfig.FileName;
                        machine.SaveAdvantechConfiguration();
                        machine.LoadOverridedConfig();
                        Activate();
                    }
                    else
                    {
                        Activate();
                        machine.OverrideConfig();
                    }
                }
                else
                    machine.OverrideConfig();
            }

            posTimer.Start();

            velocitySettings = new();
            dutyCycle = new();

            Application.AddMessageFilter(new GlobalKeyMessageFilter(dutyCycle));

            dutyCycle.MdiParent = this;
            dutyCycle.Dock = DockStyle.Fill;
            dutyCycle.Show();
            dutyCycle.Activate();

        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e) => board.CloseBoard();

        private void pbMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void pbClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Вы уверены, что хотите завершить работу?",
                "Подтверждение закрытия",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
                Close();
        }

        public bool OnGlobalKeyDown(Keys key)
        {
            if (key == Keys.Space)
            {
                Invoke(Basing.Stop);
                return false;
            }

            if (BindControl.BlockControls) return false;

            int axisIndex = GetAxisIndexForKeyboardControls(key);
            if (axisIndex == -1) return false;

            CtrlSpeedSwitch(axisIndex);
            ushort direction;
            const ushort PositiveDirection = 0, NegativeDirection = 1;
            if (key == Keys.Right || key == Keys.Up || key == Keys.PageUp || key == Keys.Insert)
                direction = PositiveDirection;
            else
                direction = NegativeDirection;
            board.StartAxisContinuousMovementChecked(axisIndex, direction);
            return true;
        }

        public bool OnGlobalKeyUp(Keys key)
        {
            int axisIndex = GetAxisIndexForKeyboardControls(key);
            if (axisIndex == -1) return false;
            board.StopAxisEmg(axisIndex);
            return true;
        }

        private int GetAxisIndexForKeyboardControls(Keys key)
        {
            return key switch
            {
                Keys.Right or Keys.Left => 0,
                Keys.Up or Keys.Down => 1,
                Keys.PageUp or Keys.PageDown => 2,
                Keys.Insert or Keys.Delete => 3,
                _ => -1,
            };
        }

        private static void CtrlSpeedSwitch(int axisIndex)
        {
            var machine = Singleton.GetInstance();
            double speed;
            if ((ModifierKeys & Keys.Control) == Keys.Control)
                speed = machine.Parameters.FastVelocity[axisIndex];
            else
                speed = machine.Parameters.SlowVelocity[axisIndex];
            machine.Board.SetAxisHighVelocity(axisIndex, speed);
        }
    }
}
