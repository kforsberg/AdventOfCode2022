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
        // Console.WriteLine(fileSystemItem.ChildItems[0].DirectorySize);
        var x = 1;
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

}