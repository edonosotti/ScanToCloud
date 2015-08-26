using System;

using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace ScanToCloud.WinUI.Helpers
{
    internal class Pdf
    {
        // Logger instance
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool Render(DTO.Document source, Common.ImageSizeMode sizeMode)
        {
            try
            {
                Document document = new Document();

                document.Info.Title = source.Title;
                document.Info.Comment = "";

                document.DefaultPageSetup.TopMargin = 0;
                document.DefaultPageSetup.BottomMargin = 0;
                document.DefaultPageSetup.LeftMargin = 0;
                document.DefaultPageSetup.RightMargin = 0;

                for (int i = 0; i < source.Pages.Count; i++)
                {
                    Section section = document.AddSection();

                    var image = section.AddImage(source.Pages[i].Path);
                    
                    if (sizeMode == Common.ImageSizeMode.Fit)
                    {
                        double fRatioWidth = (double)document.DefaultPageSetup.PageWidth / (double)source.Pages[i].Width;
                        double fRatioHeight = (double)document.DefaultPageSetup.PageHeight / (double)source.Pages[i].Height;
                        double fRatio = (fRatioWidth < fRatioHeight) ? fRatioWidth : fRatioHeight;
                        int iWidth = (int)(source.Pages[i].Width * fRatio);
                        int iHeight = (int)(source.Pages[i].Height * fRatio);
                        int iTop = (document.DefaultPageSetup.PageHeight > iHeight) ? (int)((document.DefaultPageSetup.PageHeight - iHeight) / 2) : 0;
                        int iLeft = (document.DefaultPageSetup.PageWidth > iWidth) ? (int)((document.DefaultPageSetup.PageWidth - iWidth) / 2) : 0;

                        image.Top = iTop;
                        image.Left = iLeft;
                        image.Width = iWidth;
                        image.Height = iHeight;
                    }
                    else
                    {
                        image.Top = 0;
                        image.Left = 0;
                        image.Width = document.DefaultPageSetup.PageWidth;
                        image.Height = document.DefaultPageSetup.PageHeight;
                    }
                }

                document.UseCmykColor = true;

                bool unicode = false;

                PdfFontEmbedding embedding = PdfFontEmbedding.None;

                PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode, embedding);

                pdfRenderer.Document = document;

                pdfRenderer.RenderDocument();

                const string filename = "C:\\stc.pdf";
                pdfRenderer.PdfDocument.Save(filename);

                return true;
            }
            catch (Exception error)
            {
                Log.Error(error);
            }

            return false;
        }
    }
}
