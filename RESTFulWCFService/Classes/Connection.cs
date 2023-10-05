using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public static class Connection
{
	public static string  GetConnectionString()
	{
        return (Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["Crebas"]));
	}
}