using DropNet;
using ScanToCloud.WinUI.Interfaces;
using System;

namespace ScanToCloud.WinUI.Clients
{
    internal class DropboxClient : ICloudStorageClient
    {
        // Logger instance
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DropNetClient Client;

        public DropboxClient()
        {
            Client = new DropNetClient(Common.DropNet.API_KEY, Common.DropNet.API_SECRET);
            // ToDo: load credentials
        }

        public bool Authenticate()
        {
            try
            {
                var url = Client.GetTokenAndBuildUrl();
                var web = new Forms.WebAuthentication(Common.StorageType.Dropbox, url);
                var result = web.ShowDialog();
                if (web.Success)
                {
                    var accessToken = Client.GetAccessToken();
                    // ToDo: save credentials
                    return true;
                }
            }
            catch (Exception error)
            {
                Log.Error(error);
            }

            return false;
        }

        public bool IsAuthenticated()
        {
            return (Client.UserLogin != null);
        }

        public bool Upload(string fileName)
        {
            // Client.UploadFile();
            return true;
        }
    }
}
