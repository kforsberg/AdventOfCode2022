class Application
{
    public int[,] TreeData;

    public Application()
    {
        var lines = File.ReadAllLines("input.txt");
        TreeData = new int[lines.Count(), lines[0].Length];
        InitializeData(lines);
    }

    public void Run()
    {
        Console.WriteLine(SumVisibleTrees());
        var score = GetScenicScores();
    }

    private void InitializeData(string[] lines)
    {
        var yIndex = 0;
        foreach (var line in lines)
        {
            var xIndex = 0;
            line.Select(s => int.Parse(s.ToString())).ToList().ForEach(i =>
            {
                TreeData[yIndex,xIndex] = i;
                xIndex++;
            });
            yIndex++;
        }
    }

    private int SumVisibleTrees()
    {
        int totalVisible = 0;

        for (int row = 0; row < TreeData.GetLength(0); row++)
        {
            for (int column = 0; column < TreeData.GetLength(1); column++)
            {
                if (IsVisibleDown(row, column) || IsVisibleUp(row, column) || IsVisibleLeft(row, column)|| IsVisibleRight(row, column) ) // todo: down
                {
                    totalVisible++;
                }
            }
        }

        return totalVisible;
    }
    public bool IsVisibleRight(int currentRow, int currentColumn)
    {
        var value = TreeData[currentRow, currentColumn];
        var rowLength = TreeData.GetLength(1);
        var row = Enumerable.Range(0, rowLength).Select(x => TreeData[currentRow, x]).ToArray().Skip(currentColumn + 1).Take(rowLength).ToList();
        
        if (row.Count() == 0)
        {
            return true;
        }
        return row.All(r => r < value);
    }

    public bool IsVisibleLeft(int currentRow, int currentColumn)
    {
        var value = TreeData[currentRow, currentColumn];
        var rowLength = TreeData.GetLength(1);
        var row = Enumerable.Range(0, rowLength).Select(x => TreeData[currentRow, x]).ToArray().Take(currentColumn).ToList();
        if (row.Count() == 0)
        {
            return true;
        }
        return row.All(r => r < value);
    }

    private bool IsVisibleDown(int currentRow, int currentColumn)
    {
        var value = TreeData[currentRow, currentColumn];
        var columnLength = TreeData.GetLength(0);
        var column = Enumerable.Range(0, columnLength).Select(x => TreeData[x, currentColumn]).ToArray().Skip(currentRow + 1).Take(columnLength).ToList();
        if (column.Count() == 0)
        {
            return true;
        }
        return column.All(c => c < value);
    }

    private bool IsVisibleUp(int currentRow, int currentColumn)
    {
        var value = TreeData[currentRow, currentColumn];
        var columnLength = TreeData.GetLength(0);
        var column = Enumerable.Range(0, columnLength).Select(x => TreeData[x, currentColumn]).ToArray().Take(currentRow).ToList();
        if (column.Count() == 0)
        {
            return true;
        }

        return column.All(c => c < value);
    }

    private List<int> GetScenicScores()
    {
        var scores = new List<int>();
        var columnLength = TreeData.GetLength(0);
        var rowLength = TreeData.GetLength(1);
        for (int rowIndex = 0; rowIndex < TreeData.GetLength(0); rowIndex++)
        {
            var row = Enumerable.Range(0, rowLength).Select(x => TreeData[rowIndex, x]).ToList();
            for (int columnIndex = 0; columnIndex < TreeData.GetLength(1); columnIndex++)
            {
                var column = Enumerable.Range(0, columnLength).Select(x => TreeData[x, columnIndex]);
                var rightDistance = CalculateViewingDistanceRight(columnIndex, row);
                var leftDistance = CalculateViewingDistanceLeft(columnIndex, row);
                Console.WriteLine($"Right Distance: {rightDistance}");
                Console.WriteLine($"Left Distance: {leftDistance}");
            }
        }
        return scores;
    }

    private int CalculateViewingDistanceRight(int columnIndex, List<int> row)
    {
        var remainingRow = row.Skip(columnIndex + 1).ToList();
        var index = 1;
        foreach(var item in remainingRow)
        {
            if (remainingRow[index-1] >= row[columnIndex])
            {
                return index;
            }
            index++;
        }
        return index;
    }

    private int CalculateViewingDistanceLeft(int columnIndex, List<int> row)
    {
        var remainingRow = row.Take(columnIndex).ToList();
        var value = row[columnIndex];
        var index = 1;
        for (int i = remainingRow.Count(); i > 0; i--)
        {
            if (remainingRow[i-1] >= value)
            {
                return index;
            }
            index++;
        }
        return index;
    }

}