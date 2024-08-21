using ashqTech;
using DutyCycle.Models.Machine;

namespace DutyCycle
{
    public partial class IOSetup : Form
    {
        private PictureBox[][] pbOuts;
        private Button[][] btnOuts;
        private Board board = Singleton.GetInstance().Board;

        public IOSetup()
        {
            InitializeComponent();

            btnOuts = [
                [btnOut40, btnOut50, btnOut60, btnOut70],
                [btnOut41, btnOut51, btnOut61, btnOut71],
                [btnOut42, btnOut52, btnOut62, btnOut72],
                [btnOut43, btnOut53, btnOut63, btnOut73]
            ];

            pbOuts = [
                [pbOut40, pbOut50, pbOut60, pbOut70],
                [pbOut41, pbOut51, pbOut61, pbOut71],
                [pbOut42, pbOut52, pbOut62, pbOut72],
                [pbOut43, pbOut53, pbOut63, pbOut73]
            ];

            for (int axisIndex = 0; axisIndex < board.AxesCount; axisIndex++)
                for (ushort column = 0; column < 4; column++)
                {
                    int index = axisIndex;
                    int col = column;
                    EventHandler eh = (o, ea) => TurnOutput(pbOuts[index][col], (ushort)(col + 4), index);
                    btnOuts[axisIndex][column].Click += eh;
                }
        }

        private void outsTimer_Tick(object sender, EventArgs e)
        {
            for (int axisIndex = 0; axisIndex < board.AxesCount; axisIndex++)
                for (ushort column = 0; column < 4; column++)
                {
                    byte bit = board.GetAxisOutputBit(axisIndex, (ushort)(column + 4));
                    if (bit == 1)
                        pbOuts[axisIndex][column].BackColor = Color.Green;
                    else
                        pbOuts[axisIndex][column].BackColor = Color.Silver;
                }
        }

        private void TurnOutput(PictureBox pictureBoxDO, ushort channel, int axisIndex)
        {
            byte DoValue;
            if (pictureBoxDO.BackColor == Color.Silver)
                DoValue = 1;
            else
                DoValue = 0;
            board.SetAxisOutputBit(axisIndex, channel, DoValue);
        }
    }
}
