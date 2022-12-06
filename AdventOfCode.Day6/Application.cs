class Application
{
    public void Run()
    {
        FindFirstMarker();
        FindStartOfMessageMarker();
    }

    private void FindFirstMarker()
    {
        var recievedPackets = "";
        var input = File.ReadAllText("input.txt");
        for (int i = 0; i < input.Length; i++)
        {
            recievedPackets += input[i];
            if (TestForFirstMarker(recievedPackets, 4))
            {
                Console.WriteLine($"First Marker Index: {i + 1}");
                break;
            }
        }
    }

    private void FindStartOfMessageMarker()
    {
        var recievedPackets = "";
        var input = File.ReadAllText("input.txt");
        for (int i = 0; i < input.Length; i++)
        {
            recievedPackets += input[i];
            if (TestForFirstMarker(recievedPackets, 14))
            {
                Console.WriteLine($"Start of Message Marker Index: {i + 1}");
                break;
            }
        }
    }

    private bool TestForFirstMarker(string packets, int uniqueCount)
    {
        if (packets.Length >= 4)
        {
            var lastFourCharacters = packets.Reverse().Take(uniqueCount);
            if (lastFourCharacters.ToHashSet<char>().Count() == uniqueCount)
            {
                return true;
            }
        }
        return false;
    }

}