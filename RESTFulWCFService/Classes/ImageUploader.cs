using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using AjaxControlToolkit;

/// <summary>
/// Summary description for ImageUploader
/// </summary>
public class ImageUploader
{
    public ImageUploader()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string UploadImage(FileUpload fileUpload, string directory, int height, int width)
        {
        try
        {
            // Find the fileUpload control
            string filename = fileUpload.FileName;
            filename = filename.Substring(0, filename.LastIndexOf('.'));
            //if (filename.Length > 10)
            //{
            //    String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
            //    filename = filename.Substring(0, 8) + fileExtention;
            //}
            //else
            //{
            //    String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
            //    filename = filename + fileExtention;
            //}

            string Append = String.Format("{0:yyyy-MM-ddhh-mm-ss}", DateTime.Now);
            String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
            filename = Append + fileExtention;

            // Create a bitmap of the content of the fileUpload control in memory

            Bitmap originalBMP = new Bitmap(fileUpload.FileContent);
            // Calculate the new image dimensions
            int origWidth = originalBMP.Width;
            int origHeight = originalBMP.Height;
            int sngRatio = origWidth / origHeight;
            int newWidth = width; //200;
            int newHeight = height;// newWidth / sngRatio;zz

            // Create a new bitmap which will hold the previous resized bitmap
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);

            // Create a graphic based on the new bitmap
            Graphics oGraphics = Graphics.FromImage(newBMP);
            // Set the properties for the new graphic file
            oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the new graphic based on the resized bitmap
            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);


            // Save the new graphic file to the server
            newBMP.Save(directory + filename);
            string imgname = filename;
            // Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose();
            newBMP.Dispose();
            oGraphics.Dispose();
            return imgname;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //public static string UploadImage_others(FileUpload fileUpload, HttpPostedFile hpf, string FileContent1, string directory, int height, int width)
    //{
    //    try
    //    {
    //        // Find the fileUpload control
    //        string filename = hpf.FileName;
    //        filename = filename.Substring(0, filename.LastIndexOf('.'));
    //        //if (filename.Length > 10)
    //        //{
    //        //    String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
    //        //    filename = filename.Substring(0, 8) + fileExtention;
    //        //}
    //        //else
    //        //{
    //        //    String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
    //        //    filename = filename + fileExtention;
    //        //}

    //        string Append = String.Format("{0:yyyy-MM-ddhh-mm-ss}", DateTime.Now);
    //        String fileExtention = System.IO.Path.GetExtension(hpf.FileName).ToLower();   //check file extension
    //        filename = Append + fileExtention;
    //        fileUpload.SaveAs(directory + Path.GetFileName(hpf.FileName));
    //        // Create a bitmap of the content of the fileUpload control in memory
    //        Bitmap bitmap = new Bitmap(directory + Path.GetFileName(hpf.FileName));

    //        Bitmap originalBMP = new Bitmap(hpf.FileName);
    //     // Bitmap originalBMP = new Bitmap(fileUpload.FileContent);
    //        // Calculate the new image dimensions
    //        int origWidth = originalBMP.Width;
    //        int origHeight = originalBMP.Height;
    //        int sngRatio = origWidth / origHeight;
    //        int newWidth = width; //200;
    //        int newHeight = height;// newWidth / sngRatio;zz

    //        // Create a new bitmap which will hold the previous resized bitmap
    //        Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
          
    //        // Create a graphic based on the new bitmap
    //        Graphics oGraphics = Graphics.FromImage(newBMP);
    //        // Set the properties for the new graphic file
    //        oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

    //        // Draw the new graphic based on the resized bitmap
    //        oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);


    //        // Save the new graphic file to the server
    //        newBMP.Save(directory + filename);
    //        string imgname = filename;
    //        // Once finished with the bitmap objects, we deallocate them.
    //        originalBMP.Dispose();
    //        newBMP.Dispose();
    //        oGraphics.Dispose();
    //        return imgname;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public static string AjaxUploadImage(AsyncFileUpload fileUpload, string directory, int height, int width)
    {
        try
        {
            // Find the fileUpload control
            string filename = fileUpload.FileName;
            filename = filename.Substring(0, filename.LastIndexOf('.'));
            //if (filename.Length > 10)
            //{
            //    String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
            //    filename = filename.Substring(0, 8) + fileExtention;
            //}
            //else
            //{
            //    String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
            //    filename = filename + fileExtention;
            //}

            string Append = String.Format("{0:yyyy-MM-ddhh-mm-ss}", DateTime.Now);
            String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
            filename = Append + fileExtention;

            // Create a bitmap of the content of the fileUpload control in memory
            Bitmap originalBMP = new Bitmap(fileUpload.FileContent);
            // Calculate the new image dimensions
            int origWidth = originalBMP.Width;
            int origHeight = originalBMP.Height;
            int sngRatio = origWidth / origHeight;
            int newWidth = width; //200;
            int newHeight = height;// newWidth / sngRatio;zz

            // Create a new bitmap which will hold the previous resized bitmap
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);

            // Create a graphic based on the new bitmap
            Graphics oGraphics = Graphics.FromImage(newBMP);
            // Set the properties for the new graphic file
            oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the new graphic based on the resized bitmap
            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);


            // Save the new graphic file to the server
            newBMP.Save(directory + filename);
            string imgname = filename;
            // Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose();
            newBMP.Dispose();
            oGraphics.Dispose();
            return imgname;
        }
        catch (Exception ex)
        {
            throw ex;

        }


    }
    public static string UploadImageWithName(string fileUpload, string directory, int height, int width)
    {
        try
        {
            // Find the fileUpload control
            string filename = fileUpload;
            filename = filename.Substring(0, filename.LastIndexOf('.'));
            //if (filename.Length > 10)
            //{
            //    String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
            //    filename = filename.Substring(0, 8) + fileExtention;
            //}
            //else
            //{
            //    String fileExtention = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();   //check file extension
            //    filename = filename + fileExtention;
            //}

            string Append = String.Format("{0:yyyy-MM-ddhh-mm-ss}", DateTime.Now);
            String fileExtention = System.IO.Path.GetExtension(fileUpload).ToLower();   //check file extension
            filename = Append + fileExtention;

            // Create a bitmap of the content of the fileUpload control in memory
            Bitmap originalBMP = new Bitmap(fileUpload);
            // Calculate the new image dimensions
            int origWidth = originalBMP.Width;
            int origHeight = originalBMP.Height;
            int sngRatio = origWidth / origHeight;
            int newWidth = width; //200;
            int newHeight = height;// newWidth / sngRatio;zz

            // Create a new bitmap which will hold the previous resized bitmap
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);

            // Create a graphic based on the new bitmap
            Graphics oGraphics = Graphics.FromImage(newBMP);
            // Set the properties for the new graphic file
            oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the new graphic based on the resized bitmap
            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);


            // Save the new graphic file to the server
            newBMP.Save(directory + filename);
            string imgname = filename;
            // Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose();
            newBMP.Dispose();
            oGraphics.Dispose();
            return imgname;
        }
        catch (Exception ex)
        {
            throw ex;

        }


    }
 
}
