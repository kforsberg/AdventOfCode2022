namespace Day11.Models
{
    public class Monkey
    {
        public List<long> ItemsHeld = new List<long>();
        public long CurrentlyHeldItem { get; set; }
        public Func<long, long> Operation { get; set; }
        public int TestNumber { get; set; }

        public int ThrowToIfTrue { get; set; }
        public int ThrowToIfFalse { get; set; }

        public int ItemsInspected { get; set; } = 0;

        public void PerformOperation()
        {
            var val = Operation.Invoke(CurrentlyHeldItem);
            CurrentlyHeldItem = (int)Math.Floor(val / 3.0);
        }

    }
}