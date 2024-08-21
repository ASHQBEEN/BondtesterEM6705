namespace DutyCycle.Models.BondTest
{
    public class BreakTest : BondTest
    {
        public override string Name => "Разрыв";
        public override double Result => Values.Max();

        private static int nextId = 1;

        public BreakTest() : base(nextId++) { }

        public override void TerminateTest()
        {
            if(Id > 0)
                nextId--;
            GC.SuppressFinalize(this);
        }
    }
}
