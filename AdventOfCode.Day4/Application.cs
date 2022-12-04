public class Application
{
    public void Run()
    {
        FindContainingPairs();
    }

    private void FindContainingPairs()
    {
        int totalMatching = 0;
        int totalOverlapping = 0;
        foreach (var line in File.ReadLines("input.txt"))
        {
            var rangePairs = BuildRangePairs(line);
            var firstRange = rangePairs.Item1;
            var secondRange = rangePairs.Item2;
            if (ArePairsContained(firstRange, secondRange))
            {
                totalMatching++;
            }
            if (ArePairsOverlapped(firstRange, secondRange))
            {
                totalOverlapping++;
            }
        }
        Console.WriteLine($"Total matching pairs: {totalMatching}");
        Console.WriteLine($"Total overlapping pairs: {totalOverlapping}");
    }

    private Tuple<IEnumerable<int>, IEnumerable<int>> BuildRangePairs(string line)
    {
        var pairs = line.Split(',');
        var firstPairSplit = pairs[0].Split('-');
        var secondPairSplit = pairs[1].Split('-');
        var firstPairRange = BuildRange(int.Parse(firstPairSplit[0]), int.Parse(firstPairSplit[1]));
        var secondPairRange = BuildRange(int.Parse(secondPairSplit[0]), int.Parse(secondPairSplit[1]));
        return new Tuple<IEnumerable<int>, IEnumerable<int>>(firstPairRange,secondPairRange);
    }

    private IEnumerable<int> BuildRange(int firstNumber, int secondNumber)
    {
        return Enumerable.Range(firstNumber, (secondNumber - firstNumber) + 1);
    }

    private bool ArePairsContained(IEnumerable<int> firstPair, IEnumerable<int> secondPair)
    {
        return firstPair.All(c => secondPair.Contains(c)) || secondPair.All(c => firstPair.Contains(c));
    }

    private bool ArePairsOverlapped(IEnumerable<int> firstPair, IEnumerable<int> secondPair)
    {
        return firstPair.Any(c => secondPair.Contains(c)) || secondPair.Any(c => firstPair.Contains(c));
    }

}