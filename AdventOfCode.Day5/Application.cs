using System.Text.RegularExpressions;

class Application
{

    private List<Stack<string>> stacks = new List<Stack<string>>();

    public void Run()
    {
        BuildStack();
        CrateMover9000();

        BuildStack();
        CrateMover9001();
    }

    private void BuildStack()
    {
        stacks.Clear();
        var tempStacks = new List<Stack<string>>();
        Enumerable.Range(0, 9).ToList().ForEach(_ => stacks.Add(new Stack<string>()));
        Enumerable.Range(0, 9).ToList().ForEach(_ => tempStacks.Add(new Stack<string>()));
        foreach (var line in File.ReadLines("stack.txt"))
        {
            var index = 0;
            foreach (var match in Regex.Matches(line, ".{1,4}"))
            {
                if (match != null)
                {
                    var str = match.ToString();
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        tempStacks[index].Push(str.ToString().Trim());
                    }
                }
                index++;
            }
        }

        var stackIndex = 0;
        foreach (var stack in tempStacks)
        {
            while (stack.Count > 0)
            {
                stacks[stackIndex].Push(tempStacks[stackIndex].Pop());
            }
            stackIndex++;
        }
    }

    private void CrateMover9000()
    {
        foreach (var line in File.ReadLines("procedure.txt"))
        {
            var instruction = BuildInstructions(line);
            Enumerable.Range(0, instruction.Item1).ToList().ForEach(_ => 
            {
                if (stacks[instruction.Item2].TryPop(out string? val) && val != null)
                {
                    stacks[instruction.Item3].Push(val);
                }
            });
        }
        PrintStack();
    }

    private void PrintStack()
    {
        var finalString = "";
        stacks.ForEach(s => {
            s.TryPeek(out string? result);
            finalString += (result ?? string.Empty);
        });
        Console.WriteLine(finalString.Replace("[", "").Replace("]", ""));
    }

    private void CrateMover9001()
    {
        foreach (var line in File.ReadLines("procedure.txt"))
        {
            var instruction = BuildInstructions(line);
            var list = new List<string>();
            var tempStack = new Stack<string>();
            Enumerable.Range(0, instruction.Item1).ToList().ForEach(_ => 
            {
                if (stacks[instruction.Item2].TryPop(out string? val) && val != null)
                {
                    list.Add(val);
                }
            });
            list.Reverse();
            list.ForEach(s => stacks[instruction.Item3].Push(s));
        }
        PrintStack();
    }


    // Item1 = Quantity
    // Item2 = From Index
    // Item3 = To Index
    private Tuple<int, int, int> BuildInstructions(string instruction)
    {
        var matches = Regex.Matches(instruction, @"\d+", RegexOptions.Singleline).Cast<Match>().Select(x => x.Groups[0].Value).ToList();
        return new Tuple<int, int, int>(int.Parse(matches[0]), int.Parse(matches[1]) - 1, int.Parse(matches[2]) - 1);
    }

}