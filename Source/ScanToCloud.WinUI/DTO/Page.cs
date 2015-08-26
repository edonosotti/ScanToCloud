namespace ScanToCloud.WinUI.DTO
{
    internal class Page
    {
        public string Id { get; set; }

        public string Path { get; set; }
        public bool IsTemporaryFile { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
