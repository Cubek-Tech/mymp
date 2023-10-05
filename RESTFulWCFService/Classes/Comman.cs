using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for _
/// </summary>

public class User
{
    public string SessionId { get; set; }
    public string Login_Sk { get; set; }
    public string User_Type { get; set; }
    public string User_F_Name { get; set; }
    public string User_L_Name { get; set; }
    public string Email_Id { get; set; }
    public string Status { get; set; }
    public string ErrorCode { get; set; }

}


public class SearchResult
{
    public string service_provider_name { get; set; }
    public string service_provider_sk { get; set; }
    public string address_text { get; set; }
    public string avg_review_rating { get; set; }
    public string state_sk { get; set; }
    public string city_sk { get; set; }
    public string logo_image { get; set; }
    public string country_name { get; set; }
    public string state_name { get; set; }
    public string city_name { get; set; }
    public string viewtiming { get; set; }
    public string is_calendar { get; set; }
    public string postal_code { get; set; }
    public string phone_no { get; set; }
    public string mobile_no { get; set; }
    public string distance { get; set; }
    public string promotion { get; set; }
    public string promotion_text { get; set; }
    public string provider_email_id { get; set; }
    public string keyword_match_count { get; set; }
    public string provider_lat { get; set; }
    public string provider_long { get; set; }
    public string plr_url { get; set; }
    public string plr_msg_type { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }
}

public class SearchParameter
{

    public string search_date { get; set; }
    public string search_time { get; set; }
    public string parlour_type_sk { get; set; }
    public string parlour_sub_type_sk { get; set; }
    public string country_sk { get; set; }
    public string state_sk { get; set; }
    public string city_sk { get; set; }
    public string area_sk { get; set; }
    public string keyword { get; set; }
    public string sort_by { get; set; }
    public string service_seeker_sk { get; set; }
    public string is_out_call { get; set; }

}


public class Review
{

    public string service_provider_sk { get; set; }
}

public class ReviewResponse
{
    public string slot_booking_sk { get; set; }
    public string review_rating { get; set; }
    public string avg_reviewrating { get; set; }
    public string service_provider_sk { get; set; }
    public string provider_name { get; set; }
    public string seeker_sk { get; set; }
    public string seeker_name { get; set; }
    public string review_text { get; set; }
    public string review_date { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }
}

public class Email
{
    public string sender { get; set; }
    public string reciver { get; set; }
    public string body { get; set; }
    public string subject { get; set; }

}
public class response
{

    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }
}

public class Calander
{
    public string slot_sk { get; set; }
    public string service_provider_sk { get; set; }
    public string table_sk { get; set; }
    public string slot_date { get; set; }
    public string slot_start_time { get; set; }
    public string slot_end_time { get; set; }
    public string slot_status { get; set; }
    public string slot_booking_sk { get; set; }
    public string table_no { get; set; }
    public string sStatus { get; set; }

    public string slothdiff { get; set; }
    public string sFT { get; set; }
    public string eFT { get; set; }

    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }

}

public class Calander_Req
{
    public string slot_booking_Sk { get; set; }
    public string seeker_Sk { get; set; }
    public string comment { get; set; }
    public string noPeople { get; set; }
    public string[] qusAns { get; set; }
    public string[] s_slot { get; set; }
}

public class Calander_Req_Before
{
    public string prlr_sk { get; set; }
    public string seeker_sk { get; set; }
    public string date { get; set; }
}


public class promotion
{
    public string promotion_img { get; set; }
    public string promotion_url { get; set; }
    public string promotion_text { get; set; }
    public string promotion_title { get; set; }
    public string promotion_start_date { get; set; }
    public string promotion_end_date { get; set; }
    public string day_of_week { get; set; }
    public string start_time { get; set; }
    public string end_time { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }
}

public class promotionpara
{
    public string provider_sk { get; set; }
    public string serch_date { get; set; }
    public string time { get; set; }

}

public class loginpara
{
    public string username { get; set; }
    public string password { get; set; }


}

public class login
{
    public string seeker_sk { get; set; }
    public string seeker_name { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }

}


public class country
{
    public string country_sk { get; set; }
    public string currency_sk { get; set; }
    public string country_name { get; set; }
    public string country_desc { get; set; }
    public string country_telecom_code { get; set; }
    public string date_format { get; set; }
    public string country_code { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }

}


public class massage_type
{
    public string massage_type_sk { get; set; }
    public string massage_type_name { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }

}


public class state
{
    public string country_sk { get; set; }
    public string state_sk { get; set; }
    public string state_name { get; set; }
    public string country_telecom_code { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }

}

public class state_para
{
    public string country_sk { get; set; }

}


public class city_para
{
    public string country_sk { get; set; }
    public string state_sk { get; set; }
}


public class city
{


    public string city_sk { get; set; }
    public string city_name { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }
}


public class registration_para
{
    public string email_id { get; set; }
    public string password { get; set; }
    public string service_seeker_first_name { get; set; }
    public string service_seeker_last_name { get; set; }
    public string country_sk { get; set; }
    public string state_sk { get; set; }
    public string city_sk { get; set; }
    public string mobile_no { get; set; }

    public string is_sms { get; set; }
    public string Country_code { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }

}


public class get_geo_loctn_para
{
    public string cityname { get; set; }
    public string statename { get; set; }
    public string contryname { get; set; }


}

public class get_geo_loctn
{
    public string city_sk { get; set; }
    public string state_sk { get; set; }
    public string contry_sk { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }
}



public class area_para
{
    public string country_sk { get; set; }
    public string state_sk { get; set; }
    public string city_sk { get; set; }
}


public class area
{
    public string area_sk { get; set; }
    public string area_name { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }
}
public class ProviderQuestions
{

    public string question_sk { get; set; }
    public string question_text { get; set; }
    public string is_mandatory { get; set; }
    public string answer { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }

}

public class viewtime
{
    public string service_provider_sk { get; set; }
    public string service_provider_name { get; set; }
    public string service_provider_desc { get; set; }
    public string mobile_no { get; set; }
    public string address_text { get; set; }
    public string openinghrs { get; set; }
    public string country_name { get; set; }
    public string state_name { get; set; }
    public string city_name { get; set; }
    public string from_Time { get; set; }
    public string To_Time { get; set; }
    public string day { get; set; }
    public string day_of_week { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }

}


public class customerrecords
{
    public int id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string address { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }

}



public class regulardiscount
{
    public int discount_sk { get; set; }
    public string discount_title { get; set; }
    public string discount_desc { get; set; }
    public string discount_type { get; set; }
    public string discount_percent { get; set; }
    public string discount_start_qry { get; set; }
    public string discount_end_qty { get; set; }
    public string ErrorStatus { get; set; }
    public string ErrorCode { get; set; }
}