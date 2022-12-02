using Day1.Models;

namespace Day1
{
    public class Application
    {
        public void Run()
        {
            var elvesWithFood = BuildModel();
            var elfWithMostFood = elvesWithFood.OrderByDescending(e => e.TotalCalories).First();
            Console.WriteLine($"Total of most calories held by an elf: {elfWithMostFood.TotalCalories}");

            var topThreeElves = elvesWithFood.OrderByDescending(e => e.TotalCalories).Take(3).ToList();
            Console.WriteLine($"Total calories held by the top 3 elves: {topThreeElves.Sum(e => e.TotalCalories)}");
        }

        private List<Elf> BuildModel()
        {
            var lines = File.ReadLines("input.txt");
            var elves = new List<Elf>();
            var elf = new Elf();
            foreach (var line in lines)
            {
                var isFood = int.TryParse(line, out int calories);
                if (isFood)
                {
                    elf.HeldFood.Add(new Food { Calories = calories });
                }
                else
                {
                    elves.Add(elf);
                    elf = new Elf();
                }
            }

            return elves;
        }

    }
}