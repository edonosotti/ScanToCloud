using System;
using System.Windows.Forms;

namespace ScanToCloud.WinUI.Forms
{
    public partial class WebAuthentication : Form
    {
        // Logger instance
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string Url = "";
        private Common.CloudType CloudType;

        public bool Success { get; private set; }

        public WebAuthentication(Common.CloudType cloudType, string url)
        {
            InitializeComponent();

            Success = false;

            Url = url;
            CloudType = cloudType;

            SetupUI();
        }

        private void SetupUI()
        {
            if (CloudType == Common.CloudType.Dropbox)
            {
                chkSaveLogin.Enabled = true;
            }
            else
            {
                chkSaveLogin.Enabled = false;
            }

            btnContinue.Enabled = false;
        }

        private void WebAuthentication_Load(object sender, EventArgs e)
        {
            wbrMain.Navigate(Url);
        }

        private void wbrMain_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Log.DebugFormat("WebAuthenticator is navigating: {0}", e.Url);
        }

        private void wbrMain_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (
                CloudType == Common.CloudType.Dropbox && 
                !string.IsNullOrEmpty(e.Url.ToString()) && 
                e.Url.ToString().Contains("/authorize_submit")
                )
            {
                Success = true;
                btnContinue.Enabled = true;
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
