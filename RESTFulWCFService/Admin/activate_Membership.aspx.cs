using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using System.Data;
using System.Configuration;

namespace RESTFulWCFService.Admin
{
    public partial class activate_Membership : System.Web.UI.Page
    {
        BusinessMPAdmin objadmin = new BusinessMPAdmin();
        BusinessMPartener objmpartner = new BusinessMPartener();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            string security_key = ConfigurationManager.AppSettings["Activate_membership"].ToString();
            if (security_key == txtSecurity.Text.Trim())
            {
                DataSet ds_isExist = new DataSet();
                ds_isExist = objmpartner.get_sk_by_email_id(txtEmail.Text.Trim());
                if (ds_isExist.Tables.Count > 0)
                {
                    if (ds_isExist.Tables[0].Rows.Count > 0)
                    {
                        if (ds_isExist.Tables[0].Rows[0]["massage_partner_sk"].ToString() != "" && ds_isExist.Tables[0].Rows[0]["massage_partner_sk"].ToString() != "0")
                        {
                            DataSet partner_subs = objadmin.check_membership(txtEmail.Text.Trim().ToString());
                            if (partner_subs.Tables.Count > 0)
                            {
                                if (partner_subs.Tables[0].Rows.Count > 0)
                                {
                                    objadmin.Activate_membership(txtEmail.Text.Trim(),body(ddlPayThru.SelectedValue.Trim()));
                                    lblResult.ForeColor = System.Drawing.Color.Green;
                                    lblResult.Text = "Membership activated successfully!";
                                    txtEmail.Text = "";
                                    txtYear.Text = "";

                                }
                                else
                                {
                                    objadmin.insert_membership(txtEmail.Text.Trim(), Convert.ToInt32(txtYear.Text.Trim()));
                                    objadmin.Activate_membership(txtEmail.Text.Trim(), body(ddlPayThru.SelectedValue.Trim()));
                                    lblResult.ForeColor = System.Drawing.Color.Green;
                                    lblResult.Text = "Membership activated successfully!";
                                    txtEmail.Text = "";
                                    txtYear.Text = "";
                                }
                            }
                            else
                            {
                                objadmin.insert_membership(txtEmail.Text.Trim(), Convert.ToInt32(txtYear.Text.Trim()));
                                objadmin.Activate_membership(txtEmail.Text.Trim(), body(ddlPayThru.SelectedValue.Trim()));
                                lblResult.ForeColor = System.Drawing.Color.Green;
                                lblResult.Text = "Membership activated successfully!";
                                txtEmail.Text = "";
                                txtYear.Text = "";
                            }
                        }
                        else
                        {
                            lblResult.ForeColor = System.Drawing.Color.Red;
                            lblResult.Text = "Email id not registered!";
                        }
                    }
                    else
                    {
                        lblResult.ForeColor = System.Drawing.Color.Red;
                        lblResult.Text = "Email id not registered!";
                    }
                }
                else
                {
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    lblResult.Text = "Email id not registered!";
                }
            }
            else
            {
                lblResult.ForeColor = System.Drawing.Color.Red;
                lblResult.Text = "Incorrect security key!";
            }
        }

        private string body(string type)
        {
            string message = "";
            if (type == "P")
                message = "Dear User,<br/><br/>Congratulations!<br/><br/>Your MyMassagePartner membership account has been activated now.<br/><br/><span style='color:red !important'><b>Please note:</b></span> You have made payment to our company account <b>" + ConfigurationManager.AppSettings["paypal_account_name"].ToString() + "</b>.<br/><br/> Please do login at https://www.mymassagepartner.com/login and find your massage partner for your desired location.<br/><br/>In case of any refund/dispute issue just reply to this email with 'refund reason' and we will try to resolve the issue or refund immediately.<br/><br/>Best Regards<br/>MyMassagePartner Team";
            else if (type == "S")
                message = "Dear User,<br/><br/>Congratulations!<br/><br/>Your MyMassagePartner membership account has been activated now.<br/><br/><span style='color:red !important'><b>Please note:</b></span> You have made payment to our company account <b>" + ConfigurationManager.AppSettings["stripe_account_name"].ToString() + "</b>.<br/><br/> Please do login at https://www.mymassagepartner.com/login and find your massage partner for your desired location.<br/><br/>In case of any refund/dispute issue just reply to this email with 'refund reason' and we will try to resolve the issue or refund immediately.<br/><br/>Best Regards<br/>MyMassagePartner Team";
            else
                message = "Dear User,<br/><br/>Congratulations!<br/><br/>Your MyMassagePartner membership account has been activated now.<br/><br/><span style='color:red !important'><b>Please note:</b></span> You have made payment to our company account <b>CubeK Technologies</b>.<br/><br/> Please do login at https://www.mymassagepartner.com/login and find your massage partner for your desired location.<br/><br/>In case of any refund/dispute issue just reply to this email with 'refund reason' and we will try to resolve the issue or refund immediately.<br/><br/>Best Regards<br/>MyMassagePartner Team";

            return message;
        }
    }
}