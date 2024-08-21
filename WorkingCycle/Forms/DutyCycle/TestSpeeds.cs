using DutyCycle.Models.Machine;
using ashqTech;

namespace DutyCycle.Forms.DutyCycle
{
    public partial class DutyCycleForm
    {
        #region Скорости теста

        private TestVelocities testVelocities = new();

        private void LoadTestVelocities()
        {
            //nudTestSpeed.Maximum = fastVelocity after load

            var machineTestVelocities = Singleton.GetInstance().TestConditions.Velocities;

            var fastVelocities = Singleton.GetInstance().Parameters.FastVelocity;

            //стрижка максимумов скоростей 
            if (fastVelocities[2] > machineTestVelocities.Break)
                testVelocities.Break = machineTestVelocities.Break;
            else
                testVelocities.Break = fastVelocities[2];

            if (fastVelocities[2] > machineTestVelocities.Stretch)
                testVelocities.Stretch = machineTestVelocities.Stretch;
            else
                testVelocities.Stretch = fastVelocities[2];

            if (fastVelocities[1] > machineTestVelocities.Shear)
                testVelocities.Shear = machineTestVelocities.Shear;
            else
                testVelocities.Shear = fastVelocities[1];

            if (rbBreakTest.Checked)
                nudTestSpeed.Value = (decimal)testVelocities.Break;
            else if (rbStretchTest.Checked)
                nudTestSpeed.Value = (decimal)testVelocities.Stretch;
            else if (rbShearTest.Checked)
                nudTestSpeed.Value = (decimal)testVelocities.Shear;

        }

        //Нужен для обновления максимума в случае если мы обновили FastVelocity и начали изменять значение в nud
        private void nudTestSpeed_KeyDown(object sender, KeyEventArgs e) => UpdateMaximumVelocity();

        private void nudTestSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (rbBreakTest.Checked)
                testVelocities.Break = (double)nudTestSpeed.Value;
            else if (rbStretchTest.Checked)
                testVelocities.Stretch = (double)nudTestSpeed.Value;
            else if (rbShearTest.Checked)
                testVelocities.Shear = (double)nudTestSpeed.Value;
        }

        private void SetTestVelocity() => Singleton.GetInstance().
    Board.SetAxisHighVelocity(selectedTestAxis, (double)nudTestSpeed.Value);

        private void UpdateMaximumVelocity()
        {
            var machineParameters = Singleton.GetInstance().Parameters;
            nudTestSpeed.Maximum = (decimal)machineParameters.FastVelocity[selectedTestAxis];
        }

        private void nudTestSpeed_Enter(object sender, EventArgs e)
        {
            UpdateMaximumVelocity();
        }

        #endregion
    }
}
