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

    // validated
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

    // validated
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

    // validated
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

    // in progress
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

}