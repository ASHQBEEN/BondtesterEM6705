namespace DutyCycle.Models.BondTest
{
    public class StretchTest : BondTest
    {
        public override string Name => "Растяжение";
        public override double Result => (StartPosition - EndPosition - TestSpeed * DelayTimeInSeconds) / 1000;
        public double DelayTimeInSeconds { get; set; }
        public double StartPosition { get; set; }
        public double EndPosition { get; set; }
        public double TestSpeed { get; set; }


        private static int nextId = 1;

        public StretchTest() : base(nextId++){ }

        public override void TerminateTest()
        {
            if (Id > 0)
                nextId--;
            GC.SuppressFinalize(this);
        }
    }
}
