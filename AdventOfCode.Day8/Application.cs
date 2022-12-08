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
        for (int i = 0; i < TreeData.GetLength(0); i++)
        {
            for (int j = 0; j < TreeData.GetLength(1); j++)
            {
                Console.Write(TreeData[i,j]);
            }
            Console.WriteLine();
        }
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

}