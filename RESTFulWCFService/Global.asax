<%@ Application Language="C#" %>
<%@ Import Namespace=" System.Web.Routing" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="Factory" %>
<script RunAt="server">
  
    void Application_Start(object sender, EventArgs e)
    {

        RegisterRoutes(System.Web.Routing.RouteTable.Routes);

    }
    void Application_BeginRequest(object sender, EventArgs e)
    {
        //string requestedUrl = HttpContext.Current.Request.Url.ToString().ToLower();
        //string requestedDomain = "https://mymassagepartner.com/";
        //string correctDomain = "https://www.mymassagepartner.com/";

        //if (requestedUrl.Contains(requestedDomain))
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", requestedUrl.Replace(requestedDomain, correctDomain));
        //}


        //string requestedUrl2 = HttpContext.Current.Request.Url.ToString().ToLower();
        //string requestedDomain2 = "mymassagepartner.com";
        //string correctDomain2 = "mymassagepartner.com";

        //if (requestedUrl2.Contains(requestedDomain2))
        //{
        //    HttpContext.Current.Response.Status = "301 Moved Permanently";
        //    HttpContext.Current.Response.AddHeader("Location", requestedUrl2.Replace(requestedDomain2, correctDomain2));
        //}

    }
    public static void RegisterRoutes(RouteCollection routes)
    {
        // Route existing files (default will shortcut routing if physical file exists)
        RouteTable.Routes.RouteExistingFiles = true;
        // Add StopRoutingHandler for .axd and .asmx requests
        routes.Add(new Route("{resource}.axd/{*pathInfo}", new StopRoutingHandler()));
        routes.Add(new Route("{service}.asmx/{*pathInfo}", new StopRoutingHandler()));
        routes.Add(new Route("loc_files/", new StopRoutingHandler()));
        // Add existing routes
        //////////////////////////////////////////////equipment///////////////////////////////////////////////
        //search result for ChiropractorOne.com//

  //-----------------------public url for massage-partner--------------------------------------//
        routes.MapPageRoute(
              "massage-partner",                                            // Route name
              "massage-partner/{country}/{state}/{city}/{area}/{gender}/{mtypes}/{looking}",     // Route URL - note the NodeID bit
              "~/SearchPartener.aspx",                                      // Web page to handle route
              true,                                                       // Check for physical access
              new System.Web.Routing.RouteValueDictionary 
        { 
            { "country", "z" },        // Default Node ID
            { "state", "s" },         // Default addtional variable value
            { "city", "a"}  ,
            { "area", "All-Area"},
            { "gender", "any"},
            { "mtypes", "all-types"},
            { "looking", "all"} // Default addtional variable value
            
            // Default test variable value
        }
          );
        routes.MapPageRoute(
            "body-massage-partner",                                            // Route name
            "body-massage-partner/{country}/{state}/{city}/{area}/{partnerType}/{mtypes}/{outcall}",     // Route URL - note the NodeID bit
            "~/SearchPartener.aspx",                                      // Web page to handle route
            true,                                                       // Check for physical access
            new System.Web.Routing.RouteValueDictionary 
        { 
            { "country", "z" },        // Default Node ID
            { "state", "s" },         // Default addtional variable value
            { "city", "a"}  ,
            { "area", "All-Area"},
            { "partnerType", "any"},
            { "mtypes", "all"},
            { "outcall", ""},// Default addtional variable value
            
            // Default test variable value
        }
        );
        
        routes.MapPageRoute("Home", "Home", "~/index.aspx");
        
        //routes.MapPageRoute("massage-partner", "massage-partner", "~/index.aspx");
        routes.MapPageRoute("404", "404", "~/404Error.aspx");
        routes.MapPageRoute("login", "login", "~/signin.aspx");
        routes.MapPageRoute("partner-subscription", "partner-subscription", "~/User/multipayment.aspx");
        routes.MapPageRoute("partner-subscription_", "partner-subscription_", "~/User/multipayment_partner.aspx");
        routes.MapPageRoute("Admin", "Admin", "~/Admin/login.aspx");
        routes.MapPageRoute("signup", "signup", "~/signup.aspx");
        routes.MapPageRoute("terms", "terms", "~/TermsNDconditions.aspx");
        routes.MapPageRoute("refund", "refund-policy", "~/refundPolicy.aspx");
        routes.MapPageRoute("search-partner", "massage-partner", "~/SearchPartener.aspx");
        routes.MapPageRoute("messages", "messages", "~/User/All_messages.aspx");
        routes.MapPageRoute("user-profile", "user-profile/{name}", "~/User/profile.aspx");
        routes.MapPageRoute("Fb-Search", "Fb-Search", "~/User/fbSearch.aspx");
        routes.MapPageRoute("favourite-partner", "favourite-partner", "~/User/Favourite_Partner.aspx");
        routes.MapPageRoute("edit-profile", "edit-profile", "~/User/EditDetails.aspx");
        routes.MapPageRoute("faq", "faq", "~/faq.aspx");
        routes.MapPageRoute("privacy", "privacy", "~/Privacy.aspx");
        routes.MapPageRoute("Massage-PartnerSubscription", "Massage-PartnerSubscription", "~/User/service_subscription.aspx");
        routes.MapPageRoute("membership", "membership", "~/User/_membership.aspx");
 //-Ends----------------------public url for massage-partner--------------------------------------//
    }
    void Application_Error(object sender, EventArgs e)
    {
        /////old code
        /////**************
        //// Code that runs when an unhandled error occurs
        //Exception exception = Server.GetLastError();
        //// ... log error here
        //var httpEx = exception as HttpException;
        ////if (httpEx != null && httpEx.GetHttpCode() == 404)
        ////{
        ////    Response.Redirect(Constants__.WEB_ROOT + "/NotFound", true);
        ////    return;K
        ////}
        /////new code
        /////**********************
        /////

        ////string requestedUrl = HttpContext.Current.Request.Url.ToString().ToLower();
        ////if (requestedUrl.Contains("/parlor/"))
        ////{
        ////    requestedUrl.Replace("./", "-/");
        //// //   Server.Transfer(requestedUrl);
        ////    HttpContext.Current.Response.Redirect(requestedUrl);
        ////}



    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        Session.Abandon();
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    
</script>
