using Day11.Models;

internal class Program
{

    static List<Monkey> monkies = new List<Monkey>();

    private static void Main(string[] args)
    {
        BuildMonkeys();
        foreach(var round in Enumerable.Range(0, 20).ToList())
        {
            Console.WriteLine($"Performing round {round}");
            PerformRound();
        }
        var mostActiveMonkies = monkies.OrderByDescending(x => x.ItemsInspected).ToList();
        Console.WriteLine(mostActiveMonkies[0].ItemsInspected * mostActiveMonkies[1].ItemsInspected);
    }

    private static void PerformRound()
    {
        foreach (var monkey in monkies)
        {
            while (monkey.ItemsHeld.Any())
            {
                monkey.ItemsInspected++;
                monkey.CurrentlyHeldItem = monkey.ItemsHeld[0];
                monkey.ItemsHeld = monkey.ItemsHeld.Skip(1).ToList();
                monkey.PerformOperation();
                if (monkey.CurrentlyHeldItem % monkey.TestNumber > 0)
                {
                    monkies[monkey.ThrowToIfFalse].ItemsHeld.Add(monkey.CurrentlyHeldItem);
                } else
                {
                    monkies[monkey.ThrowToIfTrue].ItemsHeld.Add(monkey.CurrentlyHeldItem);
                }
            }
        }
    }

    private static void BuildMonkeysSmall()
    {
        // 0
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 79, 98 },
            Operation = (item) => item * 19,
            TestNumber = 23,
            ThrowToIfTrue = 2,
            ThrowToIfFalse = 3
        });

        // 1
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 54, 65, 75, 74 },
            Operation = (item) => item + 6,
            TestNumber = 19,
            ThrowToIfTrue = 2,
            ThrowToIfFalse = 0
        });

        // 2
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 79, 60, 97 },
            Operation = (item) => item * item,
            TestNumber = 13,
            ThrowToIfTrue = 1,
            ThrowToIfFalse = 3
        });

        // 3
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 74 },
            Operation = (item) => item + 3,
            TestNumber = 17,
            ThrowToIfTrue = 0,
            ThrowToIfFalse = 1
        });
    }

    private static void BuildMonkeys()
    {
        // 0
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 99, 67, 92, 61, 83, 64, 98 },
            Operation = (old) => old * 17,
            TestNumber = 3,
            ThrowToIfTrue = 4,
            ThrowToIfFalse = 2
        });

        // 1
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 78, 74, 88, 89, 50 },
            Operation = (old) => old * 11,
            TestNumber = 5,
            ThrowToIfTrue = 3,
            ThrowToIfFalse = 5
        });

        // 2
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 98, 91 },
            Operation = (old) => old + 4,
            TestNumber = 2,
            ThrowToIfTrue = 6,
            ThrowToIfFalse = 4
        });

        // 3
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 59, 72, 94, 91, 79, 88, 94, 51 },
            Operation = (old) => old * old,
            TestNumber = 13,
            ThrowToIfTrue = 0,
            ThrowToIfFalse = 5
        });

        // 4
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 95, 72, 78 },
            Operation = (old) => old + 7,
            TestNumber = 11,
            ThrowToIfTrue = 7,
            ThrowToIfFalse = 6
        });

        // 5
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 76 },
            Operation = (old) => old + 8,
            TestNumber = 17,
            ThrowToIfTrue = 0,
            ThrowToIfFalse = 2
        });
        
        // 6
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 69, 60, 53, 89, 71, 88 },
            Operation = (old) => old + 5,
            TestNumber = 19,
            ThrowToIfTrue = 7,
            ThrowToIfFalse = 1
        });

        // 7
        monkies.Add(new Monkey
        {
            ItemsHeld = new List<long> { 72, 54, 63, 80 },
            Operation = (old) => old + 3,
            TestNumber = 7,
            ThrowToIfTrue = 1,
            ThrowToIfFalse = 3
        });
    }

}