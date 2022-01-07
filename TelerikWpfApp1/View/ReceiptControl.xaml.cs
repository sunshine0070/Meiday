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
using System.Windows.Navigation;
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

using CoolSms;

using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models.Impl;

using System.Threading;


namespace Meiday.View
{
    /// <summary>
    /// ReceiptControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReceiptControl : UserControl
    {
        public ReceiptControl()
        {
            Log.Debug("Receipt");
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Receipt");
            }
        }

        // sms api 생성
        SmsApi api = new SmsApi(new SmsApiOptions
        {
            ApiKey = "NCSQUDCGXE7HMOIM",
            ApiSecret = "QGKPD0IRIJ9CHEH0PPGTGRUBMIP6OJYP",
            DefaultSenderId = "01089677551" // 문자 보내는 사람 번호, coolsms 홈페이지에서 발신자 등록한 번호 필수

        });

        // 이미지 업로드 주소용
        string ImageURL = "";
        // Imgur API 키
        string ImgurAPIKey = "e2263752244a51b";
        // Imgur Secret 키
        string ImgurAPISecretKey = "16b28af075c7cdbcc950493768ba8a4b4245a37f";

        private void Button_Click1(object sender, RoutedEventArgs e)
        {

            try
            {
            LoginViewModel loginViewModel = new LoginViewModel();

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)receipt.ActualWidth, 650, 96, 96, PixelFormats.Pbgra32);
            //RenderTargetBitmap rtb = new RenderTargetBitmap((int)receipt.Width, (int)receipt.Height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(receipt);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            MemoryStream stream = new MemoryStream();
            png.Save(stream);

            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            string stampFileName = @"C:\Users\user\Desktop\savefile\" + loginViewModel.PatientName + "_영수증.png";
            image.Save(stampFileName);
            //이미지로 저장

          
            
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

                // Create a font
                XFont font = new XFont("Verdana", 100, XFontStyle.Bold);

                XImage im = XImage.FromFile(@"C:\Users\user\Desktop\savefile\" + loginViewModel.PatientName + "_영수증.png");

            gfx.DrawImage(im, 30, 30, 550, 700);



                string watermark = "Meiday";

                string filename = @"C:\Users\user\Desktop\savefile\" + loginViewModel.PatientName + "_영수증.pdf";
                //page = document.AddPage();

                document.Save(filename);
                page = document.Pages[0];

                //gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Prepend);
                gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

                // Get the size (in point) of the text
                XSize size = gfx.MeasureString(watermark, font);

                // Define a rotation transformation at the center of the page
                gfx.TranslateTransform(page.Width / 2, page.Height / 2);
                gfx.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
                gfx.TranslateTransform(-page.Width / 2, -page.Height / 2);

                // Create a string format
                XStringFormat format = new XStringFormat();
                format.Alignment = XStringAlignment.Near;
                format.LineAlignment = XLineAlignment.Near;

                // Create a dimmed red brush
                //XBrush brush = new XSolidBrush(XColor.FromArgb(128, 255, 0, 0));
                XBrush brush = new XSolidBrush(XColor.FromArgb(50, 106, 90, 205));


                // Draw the string
                gfx.DrawString(watermark, font, brush,
              new XPoint((page.Width - size.Width) / 2, (page.Height - size.Height) / 2),
              format);


                PdfSecuritySettings securitySettings = document.SecuritySettings;

            securitySettings.UserPassword = loginViewModel.PtRegnum;

            securitySettings.OwnerPassword = "owner";

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
            document.Save(filename);
                // ...and start a viewer.
                //Process.Start(filename);



            Log.Debug("receipt_Button_Click");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "receipt_Button_Click");
            }



        }

        private void Button_Click2(object sender, RoutedEventArgs e)

        {
            try
            {

                //UploadImage(@"C:\Users\user\Desktop\savefile\"+patient_id+"영수증.png");

                Log.Debug("receipt_Button2_Click");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "receipt_Button2_Click");
            }
        }



            public async Task UploadImage(string imgloc)
        {
            // 클라이언트 생성(키,시크릿키)
            var client = new ImgurClient(ImgurAPIKey, ImgurAPISecretKey);

            // 이미지 endpoint
            var endpoint = new ImageEndpoint(client);

            // 업로드할 이미지
            Imgur.API.Models.IImage img;

            // 업로드 할 이미지 파일 불러오기 (640x480 jpg)
            using (var fs = new FileStream(imgloc, FileMode.Open))
            {
                img = await endpoint.UploadImageStreamAsync(fs);
            }
            // MessageBox.Show("업로드 완료 : " + img.Link);            
            // 업로드 된 이미지 주소값 가져오기
            ImageURL = img.Link;

            var request = new SendMessageRequest("01089677551", "메이데이 결제 영수증 \r\n" + ImageURL); // 이미지 링크가 포함된 문자                                                      // 문자 보낼 전화번호 , 보낼 메세지(한글 40자)
            var result = api.SendMessageAsync(request);


        }

    }
}
