namespace Day1.Models
{
    public class Elf
    {
        public List<Food> HeldFood {get; set;} = new();
        public int TotalCalories
        {
            get
            {
                return HeldFood.Sum(f => f.Calories);
            }
        }
    }
}