using System.Collections.Generic;

namespace ScanToCloud.WinUI.DTO.Cloud
{
    internal class DocumentStore
    {
        public List<StoredDocument> Documents { get; set; }

        public DocumentStore()
        {
            Documents = new List<StoredDocument>();
        }
    }
}
