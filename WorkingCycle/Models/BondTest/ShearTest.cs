namespace DutyCycle.Models.BondTest
{
    public class ShearTest : BondTest
    {
        public override string Name => "Сдвиг";
        public override double Result => Values.Max();

        private static int nextId = 1;

        public ShearTest() : base(nextId++) { }

        public override void TerminateTest()
        {
            if (Id > 0)
                nextId--;
            GC.SuppressFinalize(this);
        }
    }
}
