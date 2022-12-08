using Day7.Models;

class Application
{
    private int currentIndex = 0;

    public void Run()
    {
        ScenarioOne();
    }


    public void ScenarioOne()
    {
        var fileSystemItem = new FileSystemItem { Name = "/", IsDirectory = true };
        var lines = File.ReadLines("input.txt");
        fileSystemItem = WalkCommands(fileSystemItem, lines.ToList());
        fileSystemItem.DirectorySize = GetTotalBytes(fileSystemItem);
        Console.WriteLine(GetTotalByteFromDirectoriesUnderThreshold(fileSystemItem, 100000));
        var threshold = 30000000 - (70000000 - fileSystemItem.DirectorySize);
        Console.WriteLine(GetDirectoriesOverThreshold(fileSystemItem, threshold).OrderBy(i => i).FirstOrDefault());
    }

    public FileSystemItem WalkCommands(FileSystemItem fileSystemItem, List<string> terminalOutputs)
    {
        if (currentIndex + 1 == terminalOutputs.Count())
        {
            return fileSystemItem;
        }
        if (terminalOutputs[currentIndex] == "$ ls")
        {
            currentIndex++;
            do
            {
                var output = terminalOutputs[currentIndex];
                if (output.StartsWith("dir"))
                {
                    fileSystemItem.ChildItems.Add(new FileSystemItem { Name = output.Split(" ")[1], IsDirectory = true });
                }
                else // output is a file
                {
                    var itemName = output.Split(" ")[1];
                    var itemSize = int.Parse(output.Split(" ")[0]);
                    fileSystemItem.ChildItems.Add(new FileSystemItem { Name = itemName, ItemSize = itemSize, IsDirectory = false });
                }
                if (currentIndex + 1 == terminalOutputs.Count()) // the last line was reached in an 'ls' command
                {
                    return fileSystemItem;
                }
            } while (!terminalOutputs[++currentIndex].StartsWith("$"));
        }

        while (terminalOutputs[currentIndex].StartsWith("$ cd"))
        {
            if (terminalOutputs[currentIndex] == "$ cd ..")
            {
                currentIndex++;
                return fileSystemItem;
            }

            var dirName = terminalOutputs[currentIndex].Split(" ")[2];
            var item = fileSystemItem.ChildItems.FirstOrDefault(c => c.Name == dirName);
            currentIndex++;
            item = WalkCommands(item, terminalOutputs);
        }

        return fileSystemItem;
    }

    private int GetTotalBytes(FileSystemItem item)
    {
        var total = 0;
        foreach (var childItem in item.ChildItems)
        {
            if (childItem.IsDirectory)
            {
                childItem.DirectorySize = GetTotalBytes(childItem);
                total += childItem.DirectorySize;
            }
            else
            {
                total += childItem.ItemSize;
            }
        }
        return total;
    }

    private int GetTotalByteFromDirectoriesUnderThreshold(FileSystemItem item, int threshold)
    {
        var total = 0;
        if (!item.IsDirectory)
        {
            return total;
        }
        foreach (var childItem in item.ChildItems)
        {
            if (childItem.DirectorySize <= threshold && childItem.IsDirectory)
            {
                total += childItem.DirectorySize;
            }
            total += GetTotalByteFromDirectoriesUnderThreshold(childItem, threshold);
        }

        return total;
    }

    private List<int> GetDirectoriesOverThreshold(FileSystemItem item, int threshold)
    {
        var list = new List<int>();
        foreach (var childItem in item.ChildItems)
        {
            if (childItem.DirectorySize >= threshold && childItem.IsDirectory)
            {
                list.Add(childItem.DirectorySize);
            }
            list.AddRange(GetDirectoriesOverThreshold(childItem, threshold));
        }
        return list;
    }

}