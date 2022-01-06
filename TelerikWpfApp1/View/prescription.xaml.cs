using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Meiday.ViewModel;
using Meiday.Properties;
using Meiday.Model;
using System.IO;
using System.Globalization;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using PdfSharp.Pdf.Security;
using static Meiday.LoginViewModel;
using PdfSharp.Pdf.IO;

namespace Meiday.View
{
    /// <summary>
    /// prescription.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class prescription : UserControl
    {
        public prescription()
        {
            Log.Debug("prescription");
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "prescription");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< Updated upstream
            try
            {

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)grStamp.ActualWidth, (int)grStamp.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(grStamp);
                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));
                MemoryStream stream = new MemoryStream();
                png.Save(stream);

                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                string stampFileName = @"C:\Users\user\Desktop\savefile\" + patient_id + "전자처방전.png";
                image.Save(stampFileName);
                //이미지로 저장

            
                PdfDocument document = new PdfDocument();

                // Create an empty page
                PdfPage page = document.AddPage();
                XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
                /*
=======
            #region 이미지로 저장
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)grStamp.ActualWidth, (int)grStamp.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(grStamp);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            MemoryStream stream = new MemoryStream();
            png.Save(stream);

            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            string stampFileName = @"C:\Users\user\Desktop\savefile\" + patient_id + "전자처방전.png";
            image.Save(stampFileName);
            //이미지로 저장

            #endregion

            #region 기존 코드
            /*
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();
            XFont font = new XFont("Verdana", 100, XFontStyle.Bold);
            
>>>>>>> Stashed changes

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);

<<<<<<< Updated upstream
                // Create a font
                XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

                XImage im = XImage.FromFile(@"C:\Users\user\Desktop\savefile\"+ patient_id+"전자처방전.png");

                gfx.DrawImage(im, 30, 30, 550, 700);
                */
=======
            XImage im = XImage.FromFile(@"C:\Users\user\Desktop\savefile\"+ patient_id+"전자처방전.png");

            gfx.DrawImage(im, 30, 30, 550, 700);


            PdfSecuritySettings securitySettings = document.SecuritySettings;

            securitySettings.UserPassword = patient_id;

            securitySettings.OwnerPassword = "meiday";

            // Restrict some rights.
            securitySettings.PermitAccessibilityExtractContent = false;
            securitySettings.PermitAnnotations = false;
            securitySettings.PermitAssembleDocument = false;
            securitySettings.PermitExtractContent = false;
            securitySettings.PermitFormsFill = true;
            securitySettings.PermitFullQualityPrint = false;
            securitySettings.PermitModifyDocument = true;
            securitySettings.PermitPrint = false;

            // Save the document...
            string filename = @"C:\Users\user\Desktop\savefile\" + patient_id + "전자처방전.pdf";
            document.Save(filename);
            // ...and start a viewer.
            //Process.Start(filename);
            */
            #endregion
>>>>>>> Stashed changes

            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();
            XFont font = new XFont("Verdana", 100, XFontStyle.Bold);


            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

<<<<<<< Updated upstream
                XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

                // Get the size (in point) of the text
                XSize size = gfx.MeasureString("watermark", font);
=======
            XImage im = XImage.FromFile(@"C:\Users\user\Desktop\savefile\" + patient_id + "전자처방전.png");

            gfx.DrawImage(im, 30, 30, 550, 700);



            string watermark = "Meiday";

            string filename = @"C:\Users\user\Desktop\savefile\" + patient_id + "전자처방전.pdf";
            //page = document.AddPage();

            document.Save(filename);
            page = document.Pages[0];

            //gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Prepend);
            gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

            // Get the size (in point) of the text
            XSize size = gfx.MeasureString(watermark, font);
>>>>>>> Stashed changes

                // Define a rotation transformation at the center of the page
                gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
                gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

<<<<<<< Updated upstream
                // Create a graphical path
                XGraphicsPath path = new XGraphicsPath();

                // Add the text to the path
                path.AddString("watermark", font.FontFamily, XFontStyle.BoldItalic, 150,
                  new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2),
                  XStringFormat.Default);

                // Create a dimmed red pen and brush
                XPen pen = new XPen(XColor.FromArgb(50, 75, 0, 130), 3);
                XBrush brush = new XSolidBrush(XColor.FromArgb(50, 106, 90, 205));

                // Stroke the outline of the path
                gfx.DrawPath(pen, brush, path);
=======
            // Create a string format
            XStringFormat format = new XStringFormat();
            format.Alignment = XStringAlignment.Near;
            format.LineAlignment = XLineAlignment.Near;

            // Create a dimmed red brush
            XBrush brush = new XSolidBrush(XColor.FromArgb(128, 255, 0, 0));
>>>>>>> Stashed changes

            // Draw the string
            gfx.DrawString(watermark, font, brush,
              new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2),
              format);

                PdfSecuritySettings securitySettings = document.SecuritySettings;

                securitySettings.UserPassword = patient_id;

                securitySettings.OwnerPassword = "meiday";

                // Restrict some rights.
                securitySettings.PermitAccessibilityExtractContent = false;
                securitySettings.PermitAnnotations = false;
                securitySettings.PermitAssembleDocument = false;
                securitySettings.PermitExtractContent = false;
                securitySettings.PermitFormsFill = true;
                securitySettings.PermitFullQualityPrint = false;
                securitySettings.PermitModifyDocument = true;
                securitySettings.PermitPrint = false;

<<<<<<< Updated upstream
                // Save the document...
                string filename = @"C:\Users\user\Desktop\savefile\" + patient_id + "전자처방전.pdf";
                document.Save(filename);
                // ...and start a viewer.
                //Process.Start(filename);
                Log.Debug("Button_Click");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Button_Click");
            }
=======

            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
>>>>>>> Stashed changes

        }





    }
    
}
