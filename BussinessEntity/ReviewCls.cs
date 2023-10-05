using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessEntity
{
    public class ReviewCls
    {

        #region Property

        public int slot_booking_sk { get; set; }
        public int ReviewSK { get; set; }
        public int? ServiceProviderSK { get; set; }
        public int? ServiceSeekerSK { get; set; }
        public int ReviewRating { get; set; }
        public string ReviewText { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmailid { get; set; }
        public char is_approved { get; set; }
        #endregion
    }
}
