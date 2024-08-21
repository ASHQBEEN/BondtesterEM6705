namespace DutyCycle.Models.BondTest
{
    public abstract class BondTest
    {
        public int Id { get; set; }
        public abstract string Name { get; }
        public List<double> Values { get; set; } = [];
        public DateTime Date { get; set; }
        public abstract double Result { get; }
        public string Terminated { get; set; } = string.Empty;

        public BondTest(int testID)
        {
            Id = testID;
            Date = DateTime.Now;
        }

        public abstract void TerminateTest();

        public override string ToString() => $"{Id}, {Name} {Date:HH:mm:ss} ";
    }
}
