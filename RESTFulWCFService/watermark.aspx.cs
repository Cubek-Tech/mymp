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
    public partial class watermark : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void add_mark() {
            String csName = "ButtonClickScript";
            Type csType = this.GetType();


            
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    // Adding watermark to the image and saving it into the specified folder!!!!
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    string path = "~/img/" + FileUpload1.PostedFile.FileName;
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

                    Rectangle rect = new Rectangle(5, 5,width - 10, height - 10);
                    //graphics.RotateTransform(45f);
                    graphics.DrawString("Ayurveda4Health.com", font, brush, rect,sf); // string, font style, brush used, x and y position for the string to be written
                    //graphics.DrawIcon(img_water, 10,20, 50, 60); 
                    System.Drawing.Image newImage = (System.Drawing.Image)bmp;

                    //changing Name
                    string Append = String.Format("{0:yyyy-MM-ddhh-mm-ss}", DateTime.Now);
                    String fileExtention = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                    Random rnd = new Random();
                    int random = rnd.Next(100);
                    Append = Append + (random).ToString();
                    string image_name = Append + fileExtention;
                   //Ends Method
                    newImage.Save(Server.MapPath("~/img/" + image_name));
                    graphics.Dispose();

                    // Inserting the image and its data in the Database Table AboutUsImages...
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
        private void optimize(Bitmap original_image)
        {
            Bitmap final_image = null;
            Graphics graphic = null;
            int reqW = Int32.Parse("300");
            int reqH = Int32.Parse("300");
            final_image = new Bitmap(reqW, reqH);
            graphic = Graphics.FromImage(final_image);
            graphic.FillRectangle(new SolidBrush(Color.Transparent),
                new Rectangle(0, 0, reqW, reqH));
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic; /* new way */
            graphic.DrawImage(original_image, 0, 0, reqW, reqH);
            final_image.Save(Server.MapPath("~/img/" + FileUpload1.PostedFile.FileName));
            //final_image.Save(MapPath("~/Resultant Images/" + tmpName +
            //    Path.GetExtension(fpImage.FileName)));
            //imgResult.ImageUrl = "~/Resultant Images/" + tmpName +
            //    Path.GetExtension(fpImage.FileName);
            //lblRIW.Text = final_image.Width.ToString();
            //lblRIH.Text = final_image.Height.ToString();
            //FileInfo nfi = new FileInfo(MapPath("~/Resultant Images/" +
            //    tmpName + Path.GetExtension(fpImage.FileName)));
            //lblRIS.Text = nfi.Length.ToString();

            //lnkSave1.NavigateUrl = "~/Original Images/" + tmpName +
            //    Path.GetExtension(fpImage.FileName);
            //lnkSave2.NavigateUrl = "~/Resultant Images/" + tmpName +
            //    Path.GetExtension(fpImage.FileName);

            if (graphic != null) graphic.Dispose();
            if (original_image != null) original_image.Dispose();
            if (final_image != null) final_image.Dispose();
        
        }
    }
}