using ScanToCloud.WinUI.DTO;
using ScanToCloud.WinUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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

        private TWAINCSToolkit TwainToolkit;
        private List<Source> Sources = new List<Source>();
        private Document CurrentDocument;

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
            btnAddScan.Enabled = false;

            if (Sources.Any() && Sources[idx] != null)
            {
                var identity = Sources[idx].Identity;
                var status = "";

                TwainToolkit.Send("DG_CONTROL", "DAT_IDENTITY", "MSG_SET", ref identity, ref status);

                if (TwainToolkit.Send("DG_CONTROL", "DAT_IDENTITY", "MSG_OPENDS", ref identity, ref status) != TWAINCSToolkit.STS.SUCCESS)
                {
                    HandleError(Resources.ErrorCannotConnectToSource);
                    return;
                }

                var capabilities = "ICAP_XFERMECH,TWON_ONEVALUE,TWTY_UINT16,2";
                if (TwainToolkit.Send("DG_CONTROL", "DAT_CAPABILITY", "MSG_SET", ref capabilities, ref status) != TWAINCSToolkit.STS.SUCCESS)
                {
                    HandleError(Resources.ErrorCannotSetSourceCapabilities);
                    return;
                }

                capabilities = "CAP_INDICATORS,TWON_ONEVALUE,TWTY_UINT16," + (Settings.Default.ShowDriverMessages ? "1" : "0");
                if (TwainToolkit.Send("DG_CONTROL", "DAT_CAPABILITY", "MSG_SET", ref capabilities, ref status) != TWAINCSToolkit.STS.SUCCESS)
                {
                    HandleError(Resources.ErrorCannotSetSourceCapabilities);
                    return;
                }

                btnAddScan.Enabled = true;
            }
        }

        private void FlushTemporaryData()
        {
            if (CurrentDocument != null && CurrentDocument.Pages.Any())
            {
                foreach (var page in CurrentDocument.Pages)
                {
                    if (page.IsTemporaryFile)
                    {
                        try { File.Delete(page.Path); } catch { }
                    }
                }

                Directory.Delete(CurrentDocument.TempPath);
            }
        }

        private void CreateNewDocument()
        {
            FlushTemporaryData();
            CurrentDocument = new Document() { TempPath = GetTempFolder() };
        }

        private string GetTempFolder()
        {
            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(path);

            return path;
        }

        private void AddPage(Bitmap source)
        {
            var page = new Page()
            {
                Path = Path.Combine(CurrentDocument.TempPath, string.Format("{0}.jpg", Guid.NewGuid())),
                IsTemporaryFile = true
            };
            
            //  Set the quality
            EncoderParameters parameters = new EncoderParameters(1);

            // Quality: 100
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

            source.Save(page.Path, GetEncoderInfo("image/jpeg"), parameters);

            CurrentDocument.Pages.Add(page);

            UpdateDocumentPreview();
        }

        private void UpdateDocumentPreview()
        {
            foreach (var page in CurrentDocument.Pages)
            {
                Image source = Image.FromFile(page.Path);
                PictureBox box = new PictureBox() { Width = 100, Height = 120, SizeMode = PictureBoxSizeMode.CenterImage };
                box.MouseMove += new System.Windows.Forms.MouseEventHandler(OnMouseMove);
                Bitmap bitmap = new Bitmap(box.Width, box.Height, PixelFormat.Format32bppPArgb);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics = Graphics.FromImage(bitmap);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Low;
                graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                SolidBrush brush = new SolidBrush(Color.White);
                Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                double fRatioWidth = (double)bitmap.Size.Width / (double)source.Width;
                double fRatioHeight = (double)bitmap.Size.Height / (double)source.Height;
                double fRatio = (fRatioWidth < fRatioHeight) ? fRatioWidth : fRatioHeight;
                int iWidth = (int)(source.Width * fRatio);
                int iHeight = (int)(source.Height * fRatio);

                graphics.FillRectangle(brush, rectangle);
                graphics.DrawImage(source, new Rectangle(((int)bitmap.Width - iWidth) / 2, ((int)bitmap.Height - iHeight) / 2, iWidth, iHeight));
                box.Image = bitmap;
                box.Update();

                flpContentItems.Controls.Add(box);
            }
        }

        private bool IsCustomDsDataSupported()
        {
            return true;
        }

        // ==================================================
        // Helpers
        // ==================================================

        // Taken from https://msdn.microsoft.com/it-it/library/ytz20d80%28v=VS.110%29.aspx
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        // ==================================================
        // Application Event handlers
        // ==================================================

        private void HandleError(string message, Exception error = null)
        {
            MessageBox.Show(message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ==================================================
        // TWAIN Event handlers
        // Taken and adapted from the twaincsscan project,
        // original comments included
        // ==================================================

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

        // ==================================================
        // UI Event handlers
        // ==================================================

        private void Main_Load(object sender, EventArgs e)
        {
            InitTwain();
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

        private void btnAddScan_Click(object sender, EventArgs e)
        {
            string szTwmemref;
            string szStatus = "";
            TWAINCSToolkit.STS sts;

            // Silently start scanning if we detect that customdsdata is supported,
            // otherwise bring up the driver GUI so the user can change settings
            if (IsCustomDsDataSupported())
            {
                szTwmemref = "0,0";
            }
            else
            {
                szTwmemref = "1,0";
            }

            sts = TwainToolkit.Send("DG_CONTROL", "DAT_USERINTERFACE", "MSG_ENABLEDS", ref szTwmemref, ref szStatus);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control control = sender as Control;
                control.DoDragDrop(control, DragDropEffects.Move);
            }
        }
    }
}
