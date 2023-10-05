using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BussinessEntity
{
  public class HomeOrderBooking
    {
        #region Method

        public int Home_Delivery_Order_sk { get; set; }
        public DateTime order_datetime { get; set; }
        public DateTime delivery_date { get; set; }
        public string delivery_time { get; set; }
        public int service_provider_sk { get; set; }
        public int service_seeker_sk { get; set; }
        public int menu_sk { get; set; }
        public int item_sk { get; set; }
        public int country_sk { get; set; }
        public int state_sk { get; set; }
        public int city_sk { get; set; }
        public string address { get; set; }
        public string postal_code { get; set; }
        public int qty { get; set; }
        public string special_request { get; set; }
        public DataTable UdttOrderDetails { get; set; }
        public string home_delivery_template { get; set; }
        public string mode { get; set; }

        public int notification_type_sk { get; set; }
        public int notification_method_sk { get; set; }

        #endregion



    }
}
