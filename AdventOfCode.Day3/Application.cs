public class Application
{
    public void Run()
    {
        FindAllRuckSackPriority();
        FindGroupPriority();
    }

    public void FindAllRuckSackPriority()
    {
        var totalPriority = 0;
        var lines = File.ReadLines("input.txt");
        foreach (var line in lines)
        {
            totalPriority += ProcessRucksack(line);
        }
        Console.WriteLine(totalPriority);
    }

    private void FindGroupPriority()
    {
        var totalPriority = 0;
        var group = new List<string>();
        var lines = File.ReadLines("input.txt");
        var index = 0;
        foreach (var line in lines)
        {
            group.Add(line);
            if (index == 2)
            {
                index = 0;
                var commonLetter = FindCommonLetter(group[0], group[1], group[2]);
                totalPriority += GetPriority(commonLetter);
                group = new List<string>();
                continue;
            }
            index++;
        }
        Console.WriteLine(totalPriority);
    }

    private char FindCommonLetter(string elfOne, string elfTwo, string elfThree)
    {
        var firstPairCommon = elfOne.Where(e1 => elfTwo.Contains(e1));
        char? commonLetter = null;
        foreach (var c in firstPairCommon)
        {
            commonLetter = elfThree.FirstOrDefault(e3 => e3 == c);
            if (commonLetter.HasValue && commonLetter != char.MinValue)
            {
                break;
            }
        }

        if (!commonLetter.HasValue)
        {
            throw new Exception("Character not found");
        }

        return commonLetter.Value;
    }

    private int ProcessRucksack(string rucksack)
    {
        var len = rucksack.Length;
        var firstHalf = rucksack.Take(len / 2);
        var secondHalf = rucksack.Skip(len / 2).Take(len / 2);
        var commonLetter = FindCommonLetter(firstHalf, secondHalf);
        return GetPriority(commonLetter);
    }

    private char FindCommonLetter(IEnumerable<char> firstHalf, IEnumerable<char> secondHalf)
    {
        return firstHalf.First(f => secondHalf.Contains(f));
    }

    private int GetPriority(char c)
    {
        return Char.IsLower(c) ? c - 96 : c - 38; 
    }
}