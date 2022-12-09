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
        Console.WriteLine(score.OrderByDescending(x => x).First());
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
                var column = Enumerable.Range(0, columnLength).Select(x => TreeData[x, columnIndex]).ToList();
                var rightDistance = CalculateViewingDistanceRight(columnIndex, row);
                var leftDistance = CalculateViewingDistanceLeft(columnIndex, row);
                var upDistance = CalculateViewingDistanceUp(rowIndex, column);
                var downDistance = CalculateViewingDistanceDown(rowIndex, column);
                var score = rightDistance * leftDistance * upDistance * downDistance; 
                scores.Add(score);
            }
            // break;
        }
        return scores;
    }

    private int CalculateViewingDistanceRight(int columnIndex, List<int> row)
    {
        var remainingRow = row.Skip(columnIndex + 1).ToList();
        if (!remainingRow.Any())
        {
            return 0;
        }
        var currentTreeHeight = row[columnIndex];
        return ProcessTreeHeights(currentTreeHeight, remainingRow);
    }

    private int ProcessTreeHeights(int currentTreeHeight, List<int> otherTreeHeights)
    {
        var index = 1;
        foreach(var treeHeight in otherTreeHeights)
        {
            if (treeHeight >= currentTreeHeight)
            {
                return index;
            }
            index++;
        }
        return index;
    }

    private int CalculateViewingDistanceLeft(int columnIndex, List<int> row)
    {
        var remainingRow = row.Take(columnIndex).Reverse().ToList();
        if (!remainingRow.Any())
        {
            return 0;
        }
        var currentTreeHeight = row[columnIndex];
        return ProcessTreeHeights(currentTreeHeight, remainingRow);
    }

    private int CalculateViewingDistanceUp(int rowIndex, List<int> column)
    {
        var remainingColumn = column.Take(rowIndex).Reverse().ToList();
        if (!remainingColumn.Any())
        {
            return 0;
        }
        var currentTreeHeight = column[rowIndex];
        return ProcessTreeHeights(currentTreeHeight, remainingColumn);
    }


    private int CalculateViewingDistanceDown(int rowIndex, List<int> column)
    {
        var remainingColumn = column.Skip(rowIndex + 1).ToList();
        if (!remainingColumn.Any())
        {
            return 0;
        }
        var currentTreeHeight = column[rowIndex];
        return ProcessTreeHeights(currentTreeHeight, remainingColumn);
    }

}