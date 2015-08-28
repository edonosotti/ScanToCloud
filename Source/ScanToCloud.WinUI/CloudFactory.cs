using Newtonsoft.Json;
using ScanToCloud.WinUI.Clients;
using ScanToCloud.WinUI.DTO.Cloud;
using ScanToCloud.WinUI.Interfaces;
using ScanToCloud.WinUI.Properties;
using System.Linq;

namespace ScanToCloud.WinUI
{
    internal class CloudFactory
    {
        public static ICloudStorageClient GetClient(Common.CloudType cloudType)
        {
            switch (cloudType)
            {
                case Common.CloudType.Dropbox:
                    return new DropboxClient();
                default:
                    return new DropboxClient();
            }
        }

        public static T GetStoredCredential<T>()
        {
            var credentials = GetCredentialStore();
            var query = credentials.Credentials.Where(w => w.Type.Equals(typeof(T)));
            if (query.Any())
            {
                return (T)query.FirstOrDefault().Credential;
            }

            return default(T);
        }

        public static void SaveCredential<T>(T credential)
        {
            var credentials = GetCredentialStore();

            var query = credentials.Credentials.Where(w => w.Type.Equals(typeof(T)));
            if (query.Any())
            {
                foreach (var row in query)
                {
                    credentials.Credentials.Remove(row);
                }
            }

            credentials.Credentials.Add(new StoredCredential()
            {
                Type = typeof(T),
                Credential = credential
            });

            SetCredentialStore(credentials);
        }

        private static void SetCredentialStore(CredentialStore credentialStore)
        {
            Settings.Default.StoredCloudCredentials = JsonConvert.SerializeObject(credentialStore);
            Settings.Default.Save();
        }

        private static CredentialStore GetCredentialStore()
        {
            if (!string.IsNullOrEmpty(Settings.Default.StoredCloudCredentials))
            {
                return JsonConvert.DeserializeObject<CredentialStore>(Settings.Default.StoredCloudCredentials);
            }

            return new CredentialStore();
        }
    }
}
