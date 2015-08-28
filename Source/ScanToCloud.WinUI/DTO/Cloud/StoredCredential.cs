using System;

namespace ScanToCloud.WinUI.DTO.Cloud
{
    internal class StoredCredential
    {
        public Type Type { get; set; }
        public object Credential { get; set; }
    }
}
