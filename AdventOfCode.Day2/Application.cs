using Day2.Enums;

public class Application
{

    private Dictionary<string, HandShape> OpponentMappings = new Dictionary<string, HandShape>
    {
        { "A", HandShape.Rock  },
        { "B", HandShape.Paper },
        { "C", HandShape.Scissors }
    };

    private Dictionary<string, HandShape> YourResponseMappings = new Dictionary<string, HandShape>
    {
        { "X", HandShape.Rock  },
        { "Y", HandShape.Paper },
        { "Z", HandShape.Scissors }
    };

    private Dictionary<HandShape, int> ShapeScores = new Dictionary<HandShape, int>
    {
        { HandShape.Rock, 1 },
        { HandShape.Paper, 2 },
        { HandShape.Scissors, 3 }
    };

    private Dictionary<HandShape, HandShape> WinningPairs = new Dictionary<HandShape, HandShape>
    {
        { HandShape.Rock, HandShape.Scissors },
        { HandShape.Scissors, HandShape.Paper },
        { HandShape.Paper, HandShape.Rock }
    };

    private Dictionary<HandShape, HandShape> LosingPairs = new Dictionary<HandShape, HandShape>
    {
        { HandShape.Scissors, HandShape.Rock },
        { HandShape.Paper, HandShape.Scissors },
        { HandShape.Rock, HandShape.Paper }
    };

    private Dictionary<string, DesiredOutcome> Outcomes = new Dictionary<string, DesiredOutcome>
    {
        { "X", DesiredOutcome.Lose },
        { "Y", DesiredOutcome.Draw },
        { "Z", DesiredOutcome.Win }
    };

    public void Run()
    {
        PlayToWin();
        PlayWithStrategy();
    }

    private void PlayToWin()
    {
        var lines = File.ReadLines("input.txt");
        var totalScore = 0;
        foreach (var line in lines)
        {
            var playerInputs = line.Split(' ');
            var opponentInput = OpponentMappings[playerInputs[0]];
            var yourInput = YourResponseMappings[playerInputs[1]];
            totalScore += PlayRound(opponentInput, yourInput);
        }

        Console.WriteLine($"Total Score: {totalScore}");
    }

    private void PlayWithStrategy()
    {
        var lines = File.ReadLines("input.txt");
        var totalScore = 0;
        foreach(var line in lines)
        {
            var playerInputs = line.Split(' ');
            var opponentInput = OpponentMappings[playerInputs[0]];
            var outcome = Outcomes[playerInputs[1]];
            var yourInput = DetermineThrow(outcome, opponentInput);
            totalScore += PlayRound(opponentInput, yourInput);
        }
        Console.WriteLine($"Total Score with elf's strategy: {totalScore}");
    }

    private int PlayRound(HandShape opponentShape, HandShape yourShape)
    {
        var roundScore = ShapeScores[yourShape];
        if (opponentShape == yourShape)
        {
            roundScore += 3;
        }
        roundScore += IsWin(opponentShape, yourShape) ? 6 : 0;
        return roundScore;
    }

    private bool IsWin(HandShape opponentShape, HandShape yourShape)
    {
        var winningShape = WinningPairs[yourShape];
        return winningShape == opponentShape;
    }

    private HandShape DetermineThrow(DesiredOutcome outcome, HandShape opponentThrow)
    {
        if (outcome == DesiredOutcome.Win)
        {
            return LosingPairs[opponentThrow];
        }
        if (outcome == DesiredOutcome.Lose)
        {
            return WinningPairs[opponentThrow];
        }
        return opponentThrow;
    }

}