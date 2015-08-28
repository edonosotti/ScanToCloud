using System;
using System.Collections.Generic;

namespace ScanToCloud.WinUI.DTO.Cloud
{
    internal class StoredDocument
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }

        public List<string> Tags { get; set; }

        public string FileName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Revision { get; set; }

        public StoredDocument()
        {
            Tags = new List<string>();

            CreatedAt = DateTime.Now.ToUniversalTime();
            UpdatedAt = DateTime.Now.ToUniversalTime();
            Revision = 0;
        }
    }
}
