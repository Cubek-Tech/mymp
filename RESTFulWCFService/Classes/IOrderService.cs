using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace RESTFulWCFService
{


    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        List<country> GetCustomers();
        
      

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<SearchResult> Search_Result(List<SearchParameter> Search);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<SearchResult> Search_Result_ByHome(List<SearchParameter> Search);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        response SendMail(List<Email> mail);


        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<promotion> get_promotion(List<promotionpara> promo);

                      
        
        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<Calander> CalendarTable(List<Calander_Req_Before> calander);


        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
         response SlotBooking (List<Calander_Req> req);



        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<login> get_login(List<loginpara> lgn);


        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<country> get_conutry();

        


        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<massage_type> get_massagetype();

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<state> get_state(List<state_para> state);


        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<city> get_city(List<city_para> city);


        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        response registeration(List<registration_para> regist);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<Calander> SeekerOldBookings(List<Calander_Req_Before> calander);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        response DeleteAllBooking(string slot_booking_sk);


        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<ReviewResponse> Get_Review(List<Review> Review);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<get_geo_loctn> get_geo_location(List<get_geo_loctn_para> loc);


        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<area> get_area(List<area_para> area);


        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<ProviderQuestions> ProviderQuestions(string provider_sk);



        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<viewtime> ViewTiming(string provider_sk, string search_date);


        [OperationContract]
        [WebInvoke(Method = "GET",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<customerrecords> get_customerRecords();


        

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<customerrecords> update_customerRecords(List<customerrecords> customer_records);

        [OperationContract]
        [WebInvoke(Method = "GET",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<customerrecords> updaterecord(string arr);

        [OperationContract]
        [WebInvoke(Method = "POST",
         BodyStyle = WebMessageBodyStyle.Wrapped,
         ResponseFormat = WebMessageFormat.Json)]
        List<regulardiscount> getRegulardiscount(int qty);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        ResponseFormat = WebMessageFormat.Json)]
        void SendMessage(int userId, int otherUserId, string message);

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        void SendTypingSignal(int userId, int otherUserId);



        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        Update GetUpdates(int userId);


        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        AjaxDictionary<string, string> GetUserInfo(int userId, string userName);
      
    }
}
