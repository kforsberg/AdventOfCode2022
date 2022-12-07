namespace Day7.Models
{
    public class FileSystemItem
    {
        public string Name { get; set; } = string.Empty;
        public List<FileSystemItem> ChildItems { get; set; } = new List<FileSystemItem>();
        public bool IsDirectory { get; set; }
        public int ItemSize { get; set; }

        // public override bool Equals(object? obj)
        // {
        //     return obj is FileSystemItem item &&
        //            Name == item.Name &&
        //            IsDirectory == item.IsDirectory;
        // }

        public int DirectorySize 
        {
            get
            {
                return ChildItems.SelectMany(c => c.ChildItems).Sum(c => c.ItemSize);
            }
        }
    }
}