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
using System.Web.Compilation;
using System.Web.Routing;

public interface IRoutablePage
{
    RequestContext RequestContext { set; }
}

//public class CustomRouteHandler : IRouteHandler
//{
//    //public CustomRouteHandler(string virtualPath)
//    //{
//    //    this.VirtualPath = virtualPath;
//    //}

//    //public string VirtualPath { get; private set; }

//    //public IHttpHandler GetHttpHandler(RequestContext
//    //      requestContext)
//    //{
//    //    var page = BuildManager.CreateInstanceFromVirtualPath
//    //         (VirtualPath, typeof(Page)) as IHttpHandler;

//    //    if (page != null)
//    //    {
//    //        var routablePage = page as IRoutablePage;

//    //        if (routablePage != null) routablePage.RequestContext = requestContext;
//    //    }

//    //    return page;
//    //}
//}


