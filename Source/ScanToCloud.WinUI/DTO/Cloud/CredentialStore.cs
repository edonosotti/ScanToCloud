using System.Collections.Generic;

namespace ScanToCloud.WinUI.DTO.Cloud
{
    internal class CredentialStore
    {
        public List<StoredCredential> Credentials { get; set; }

        public CredentialStore()
        {
            Credentials = new List<StoredCredential>();
        }
    }
}
