using System.Collections.Generic;

namespace ScanToCloud.WinUI.DTO
{
    internal class Document
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Keywords { get; set; }

        public string TempPath { get; set; }
        public string FileName { get; set; }

        public List<Page> Pages { get; set; }

        public Document()
        {
            Pages = new List<Page>();

            Title = string.Empty;
            Author = string.Empty;
            Subject = string.Empty;
            Keywords = string.Empty;
        }

        public bool IsSaved()
        {
            return (!string.IsNullOrEmpty(FileName));
        }
    }
}
