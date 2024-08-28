using ashqTech;
using DutyCycle.Forms.DutyCycle;
using DutyCycle.Models.Machine;
using DutyCycle.Logic;

namespace DutyCycle.Forms
{
    public partial class MainMenu : Form
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
        }

        private void InitializeDriver()
        {
            Singleton.GetInstance().LoadAllModulesParameters();
            Singleton.GetInstance().InitializeBoard();
            Singleton.GetInstance().Board.OpenBoard();
            board = Singleton.GetInstance().Board;
        }

        private void btnAdjustments_Click(object sender, EventArgs e)
        {
            velocitySettings ??= new();
            velocitySettings.MdiParent = this;
            velocitySettings.Dock = DockStyle.Fill;
            velocitySettings.Show();
            velocitySettings?.Activate();
            Basing.AxisZBasingDone = false;
        }

        private void btnDutyCycle_Click(object sender, EventArgs e)
        {
            dutyCycle ??= new DutyCycleForm();
            dutyCycle.MdiParent = this;
            dutyCycle.Dock = DockStyle.Fill;
            dutyCycle.Show();
            dutyCycle?.Activate();
            KeyDown -= dutyCycle.StopBySpaceKey;
            KeyDown += dutyCycle.StopBySpaceKey;
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
            if (File.Exists(machine.advantechConfigurationPath))
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
                        machine.advantechConfigurationPath = openFileConfig.FileName;
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
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            board.CloseBoard();
        }

        private void pbMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

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
    }
}
