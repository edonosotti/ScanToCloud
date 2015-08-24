using System.Collections.Generic;

namespace ScanToCloud.WinUI.DTO
{
    internal class Document
    {
        public string Title { get; set; }
        public string TempPath { get; set; }
        public string FileName { get; set; }

        public List<Page> Pages { get; set; }

        public Document()
        {
            Pages = new List<Page>();
        }

        public bool IsSaved()
        {
            return (!string.IsNullOrEmpty(FileName));
        }
    }
}
