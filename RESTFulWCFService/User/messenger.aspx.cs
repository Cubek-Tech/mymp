using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;

namespace RESTFulWCFService.User
{
    public partial class messenger : System.Web.UI.Page
    {
        BusinessMPartener objbusiness = new BusinessMPartener();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
           
            if (!IsPostBack)
            {
                hdnWebMthodUrl.Value = Constants__.WEB_ROOT + "/User/ajaxMethods.asmx/fetch_message_db";
                hdnWebMthodUrl1.Value = Constants__.WEB_ROOT + "/User/ajaxMethods.asmx/insert_message_db";
                if (Session["UserId"] != null)
                {
                    //chat_provider.Visible = true;
                    if (OnlineUser.userObj.Where(item => item.sessionId == HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value.ToString()).Count() > 0)

                        OnlineUser.userObj.Remove(OnlineUser.userObj.Where(item => item.sessionId == HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value.ToString()).FirstOrDefault());
                    //if (hdnpartnersubscribed.Value != null && hdnpartnersubscribed.Value != "" && hdnpartnersubscribed.Value != "N")
                    //{
                        OnlineUser.AddOnlineUser("", HttpContext.Current.Session["UserName"].ToString(), HttpContext.Current.Session["UserId"].ToString(), HttpContext.Current.Request.Cookies["ASP.NET_SessionId"].Value.ToString());
                    //}
                    hdnUserId.Value = Session["UserId"].ToString();
                    hdnUserName.Value = Session["UserName"].ToString();
                }
                get_chat_users();
            }
        }

        #region methods
        private void get_chat_users()
        {
            dtChatUserList.DataSource = objbusiness.get_chat_users();
            dtChatUserList.DataBind();
        }
        #endregion
        [WebMethod]
        public static string insert_message_db(int from_id,int group_id,string message)
        {
            BusinessMPartener objmp = new BusinessMPartener();
            int i = objmp.insert_messages(from_id, group_id, message);
            return "1";
        }

        protected void dtChatUserList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            HiddenField hdnmassage_sk = e.Item.FindControl("hdnmassage_sk") as HiddenField;
            Control objDiv = e.Item.FindControl("user_div");
            //if (OnlineUser.userObj.Where(item => item.userId == hdnmassage_sk.Value).Count() > 0)
            //{
            if (hdnmassage_sk.Value == Session["massage_partner_sk"].ToString())
            {
                objDiv.Visible = false;
            }
            //}
            //else
            //{
            //    objDiv.Visible = false;
            //}
        }
    }
}