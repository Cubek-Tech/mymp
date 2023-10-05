using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace RESTFulWCFService.Admin
{
    public partial class updateQR : System.Web.UI.Page
    {
        Business.BusinessMPAdmin objAdmin = new Business.BusinessMPAdmin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (fluUpload.HasFile)
            {
                save_image();
                string file_name = fluUpload.FileName;
                objAdmin.insert_paytm(ViewState["Image_Name"].ToString(), "QR");
                lblResult.Text = "Successfully uploaded...";
            }
            else
            {
                lblResult.Text = "Please Select QR Image";
                lblResult.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnpaytm_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                save_image_paytm();
                string file_name = FileUpload1.FileName;
                objAdmin.insert_paytm(ViewState["Image_Name"].ToString(), "Paytm");
                Label2.Text = "Successfully uploaded...";
            }
            else
            {
                Label2.Text = "Please Select Paytm Image";
                Label2.ForeColor = System.Drawing.Color.Red;
            }
        }
        #region method
        void save_image() {

            if (fluUpload.HasFile)
            {
                string imgContentType = fluUpload.PostedFile.ContentType;
                HttpFileCollection hfc = Request.Files;
                HttpPostedFile file =fluUpload.PostedFile;
                string Append = String.Format("{0:yyyy-MM-ddhh-mm-ss}", DateTime.Now);
                String fileExtention = System.IO.Path.GetExtension(file.FileName).ToLower();
                Random rnd = new Random();
                int random = rnd.Next(100);
                Append = Append + (random).ToString();
                string image_name = Append + fileExtention;
                string directory = Context.Server.MapPath("~/image/Paytm/" + image_name);
                ViewState["Image_Name"] = image_name;
                file.SaveAs(directory);
                System.Threading.Thread.Sleep(800);
           }
        }

        void save_image_paytm()
        {

            if (FileUpload1.HasFile)
            {
                string imgContentType = FileUpload1.PostedFile.ContentType;
                HttpFileCollection hfc = Request.Files;
                HttpPostedFile file = FileUpload1.PostedFile;
                string Append = String.Format("{0:yyyy-MM-ddhh-mm-ss}", DateTime.Now);
                String fileExtention = System.IO.Path.GetExtension(file.FileName).ToLower();
                Random rnd = new Random();
                int random = rnd.Next(100);
                Append = Append + (random).ToString();
                string image_name = Append + fileExtention;
                string directory = Context.Server.MapPath("~/image/Paytm/" + image_name);
                ViewState["Image_Name"] = image_name;
                file.SaveAs(directory);
                System.Threading.Thread.Sleep(800);
            }
        }
        #endregion

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "makeDefault")
        //    {
        //        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //        Label lblID = (Label)row.FindControl("Label3");
        //        objAdmin.update_paytm_default(Convert.ToInt32(lblID.Text));
        //        GridView1.DataBind();


        //    }
        //}
    }
}