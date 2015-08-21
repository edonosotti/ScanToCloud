using ScanToCloud.WinUI.DTO;
using ScanToCloud.WinUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TWAINWorkingGroupToolkit;

namespace ScanToCloud.WinUI.Forms
{
    public partial class Main : Form
    {
        // ==================================================
        // Class properties
        // ==================================================

        TWAINCSToolkit TwainToolkit;
        List<Source> Sources = new List<Source>();

        // ==================================================
        // Constructors
        // ==================================================

        public Main()
        {
            InitializeComponent();
        }

        // ==================================================
        // Methods
        // ==================================================

        private void InitTwain()
        {
            try
            {
                TwainToolkit = new TWAINCSToolkit
                                                (
                                                    Handle,
                                                    WriteOutput,
                                                    ReportImage,
                                                    null,
                                                    "TWAIN Working Group",
                                                    "TWAIN Sharp",
                                                    "TWAIN Sharp Scan App",
                                                    2,
                                                    3,
                                                    new string[] { "DF_APP2", "DG_CONTROL", "DG_IMAGE" },
                                                    "USA",
                                                    "testing...",
                                                    "ENGLISH_USA",
                                                    1,
                                                    0,
                                                    false,
                                                    true
                                                );

                var defaultSource = string.Empty;
                var sources = TwainToolkit.GetDrivers(ref defaultSource);

                if (!sources.Any())
                {
                    throw new Exception("TWAIN GetDrivers returned an empty list.");
                }

                ParseSources(sources);
            }
            catch (Exception error)
            {
                HandleError(Resources.ErrorCannotInitTwain, error);
                return;
            }
        }

        private void ParseSources(string[] sources)
        {
            Sources = new List<Source>();
            cmbSources.Items.Clear();

            foreach (var source in sources)
            {
                var info = TWAINCSToolkit.CsvParse(source);
                Sources.Add(new Source()
                {
                    Identity = source,
                    Manufacturer = info[9],
                    ProductFamily = info[10],
                    ProductName = info[11]
                });
            }

            cmbSources.Items.Add(Sources[0].ProductName);
            cmbSources.SelectedIndex = 0;
        }

        private void SetSelectedSource(int idx)
        {
            if (Sources.Any() && Sources[idx] != null)
            {
                var identity = Sources[idx].Identity;
                var status = "";
                TwainToolkit.Send("DG_CONTROL", "DAT_IDENTITY", "MSG_SET", ref identity, ref status);
                if (TwainToolkit.Send("DG_CONTROL", "DAT_IDENTITY", "MSG_OPENDS", ref identity, ref status) != TWAINCSToolkit.STS.SUCCESS)
                {
                    HandleError(Resources.ErrorCannotConnectToSource);
                }
            }
        }

        private void HandleError(string message, Exception error = null)
        {
            MessageBox.Show(message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ==================================================
        // Event handlers
        // ==================================================

        private void Main_Load(object sender, EventArgs e)
        {
            InitTwain();
        }

        /// <summary>
        /// Debugging output that we can monitor, this is just a place
        /// holder for this particular application...
        /// </summary>
        /// <param name="a_szOutput"></param>
        private void WriteOutput(string a_szOutput)
        {
            return;
        }

        /// <summary>
        /// Handle an image.  a_bitmap is passed by reference so that this function can
        /// dispose and null it out to gain access to the file that's backing it.  The
        /// calling toolkit function will never perform any action with a_bitmap after
        /// this function returns.
        /// </summary>
        /// <param name="a_szDg">Data group that preceeded this call</param>
        /// <param name="a_szDat">Data argument type that preceeded this call</param>
        /// <param name="a_szMsg">Message that preceeded this call</param>
        /// <param name="a_sts">Current status</param>
        /// <param name="a_bitmap">C# bitmap of the image</param>
        /// <param name="a_szFile">File name, if doing a file transfer</param>
        /// <param name="a_szTwimageinfo">data collected for us</param>
        /// <param name="a_abImage">a byte array of the image</param>
        /// <param name="a_iImageOffset">byte offset where the image data begins</param>
        private TWAINCSToolkit.MSG ReportImage
        (
            string a_szTag,
            string a_szDg,
            string a_szDat,
            string a_szMsg,
            TWAINCSToolkit.STS a_sts,
            Bitmap a_bitmap,
            string a_szFile,
            string a_szTwimageinfo,
            byte[] a_abImage,
            int a_iImageOffset
        )
        {
            // Let us be called from any thread...
            if (this.InvokeRequired)
            {
                // We need a copy of the bitmap, because we're not going to wait
                // for the thread to return.  Be careful when using EndInvoke.
                // It's possible to create a deadlock situation with the Stop
                // button press.
                BeginInvoke(new MethodInvoker(delegate () { ReportImage(a_szTag, a_szDg, a_szDat, a_szMsg, a_sts, a_bitmap, a_szFile, a_szTwimageinfo, a_abImage, a_iImageOffset); }));
                return (TWAINCSToolkit.MSG.ENDXFER);
            }

            // We're processing end of scan...
            if (a_bitmap == null)
            {
                // Report errors, but only if the driver's indicators have
                // been turned off, otherwise we'll hit the user with multiple
                // dialogs for the same error...
                //if (!m_blIndicators && (a_sts != TWAINCSToolkit.STS.SUCCESS))
                //{
                //    MessageBox.Show("End of session status: " + a_sts);
                //}

                // Get ready for the next scan...
                //SetButtons(EBUTTONSTATE.OPEN);
                //return (TWAINCSToolkit.MSG.ENDXFER);
            }

            // Display the image...
            // LoadImage(a_bitmap);

            // All done...
            return (TWAINCSToolkit.MSG.ENDXFER);
        }

        private void cmbSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedSource(((ComboBox)sender).SelectedIndex);
        }

        private void btnSetupSource_Click(object sender, EventArgs e)
        {
            string szTwmemref = "1,0";
            string szStatus = "";
            TwainToolkit.Send("DG_CONTROL", "DAT_USERINTERFACE", "MSG_ENABLEDSUIONLY", ref szTwmemref, ref szStatus);
        }
    }
}
