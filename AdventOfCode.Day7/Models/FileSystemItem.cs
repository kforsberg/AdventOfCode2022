namespace Day7.Models
{
    public class FileSystemItem
    {
        public string Name { get; set; } = string.Empty;
        public List<FileSystemItem> ChildItems { get; set; } = new List<FileSystemItem>();
        public bool IsDirectory { get; set; }
        public int ItemSize { get; set; }
        public int DirectorySize {get; set;}
    }
}