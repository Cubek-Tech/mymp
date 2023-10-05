using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RESTFulWCFService
{
    public class WaterMarkNOptimize
    {
        public string optimizeNwatermark(FileUpload FileUpload1, string Path, string waterMarkText)
        {
            if (FileUpload1.HasFile)
            {
                // Adding watermark to the image and saving it into the specified folder!!!!
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Far;
                sf.Alignment = StringAlignment.Far;
                System.Drawing.Image image = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                int width = image.Width;
                int height = image.Height;
                Bitmap bmp = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage((System.Drawing.Image)bmp);
                //System.Drawing.Image img_water = System.Drawing.Image.FromFile("~/Images/ayurveda4health.png");
                graphics.FillRectangle(new SolidBrush(Color.Transparent),
                    new Rectangle(0, 0, width, height));
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(image, 0, 0, width, height);
                Font font = new Font("Arial", 12, FontStyle.Bold);
                SolidBrush brush = new SolidBrush(Color.FromArgb(0, 153, 51));

                Rectangle rect = new Rectangle(5, 5, width - 10, height - 10);
                //graphics.RotateTransform(45f);
                graphics.DrawString("Ayurveda4Health.com", font, brush, rect, sf); // string, font style, brush used, x and y position for the string to be written
                //graphics.DrawIcon(img_water, 10,20, 50, 60); 
                //changing Name
                string Append = String.Format("{0:yyyy-MM-ddhh-mm-ss}", DateTime.Now);
                String fileExtention = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                Random rnd = new Random();
                int random = rnd.Next(100);
                Append = Append + (random).ToString();
                string image_name = Append + fileExtention;
                //Ends Method
                System.Drawing.Image newImage = (System.Drawing.Image)bmp;

                //newImage.Save(Server.MapPath("~/img/" + image_name));
                graphics.Dispose();
                System.Drawing.Image X = newImage;
                return image_name;

            }
            else
            {
                return "Null";
            }
        }
    }
}