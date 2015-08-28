using DropNet;
using ScanToCloud.WinUI.DTO.Cloud;
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
            AutoLogin();
        }

        public bool Authenticate()
        {
            try
            {
                var url = Client.GetTokenAndBuildUrl();
                var web = new Forms.WebAuthentication(Common.CloudType.Dropbox, url);
                var result = web.ShowDialog();
                if (web.Success)
                {
                    var accessToken = Client.GetAccessToken();
                    CloudFactory.SaveCredential(new DropboxCredential() { UserToken = Client.UserLogin.Token, UserSecret = Client.UserLogin.Secret });
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

        private void AutoLogin()
        {
            var credential = CloudFactory.GetStoredCredential<DropboxCredential>();

            if (credential != null)
            {
                Client.UserLogin = new DropNet.Models.UserLogin() { Token = credential.UserToken, Secret = credential.UserSecret };
            }
        }
    }
}
