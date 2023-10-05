using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace Factory
{
    public class FactoryPaypal
    {
        public int is_paypal()
        {
            try
            {
                return Convert.ToInt32(SqlHelper.ExecuteScalar(Config.Crebas, "p_g_is_paypal"));
            }
            catch 
            {
                return -1;
            }
        }

        public string Get_Currency_Code(int Country_sk)
        {
            try
            {

                return Convert.ToString(SqlHelper.ExecuteScalar(Config.Crebas, "[dbo].[f_g_currency_short_name]", Country_sk));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
