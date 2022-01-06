﻿using System;
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

namespace Meiday.View
{
    /// <summary>
    /// prescription.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class prescription : UserControl
    {
        public prescription()
        {
            Log.Debug("prescription Start!");
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "prescription Error!!");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
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

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            XImage im = XImage.FromFile(@"C:\Users\user\Desktop\savefile\"+ patient_id+"전자처방전.png");

            gfx.DrawImage(im, 30, 30, 550, 700);
            /*
            // Variation 3: Draw watermark as transparent graphical path above text
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            XImage im = XImage.FromFile(@"C:\Users\user\Desktop\savefile\" + patient_id + "전자처방전.png");

            gfx.DrawImage(im, 30, 30, 550, 700);



            // Get the size (in point) of the text
            XSize size = gfx.MeasureString(watermark, font);

            // Define a rotation transformation at the center of the page
            gfx.TranslateTransform(page.Width / 2, page.Height / 2);
            gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

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
            */

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

            Log.Debug("prescription 접수");

        }
    }
}
