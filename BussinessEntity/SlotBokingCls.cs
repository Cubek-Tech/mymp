using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BussinessEntity
{
    public class SlotBokingCls
    {

        private Int64? _slotbookingsk;
        private Int32? _ServiceSeekersk;
        private String _ServiceSeekerComment;
        private String _ServiceProviderComment;
        private Int32 _Noofpeople;
        private DataTable _UdttSlotDetails;
        private DataTable _UdttQuestionsAnswer;
        private String _Action;
        private DataTable _UdttMenuOrder;

        private static DataTable _UdttSplReqTable = null;


        public Int64? Slotbookingsk
        {
            get { return _slotbookingsk; }
            set { _slotbookingsk = value; }

        }
        public Int32? ServiceSeekersk
        {
            get { return _ServiceSeekersk; }
            set { _ServiceSeekersk = value; }
        }
        public String ServiceSeekerComment
        {
            get { return _ServiceSeekerComment; }
            set { _ServiceSeekerComment = value; }
        }
        public String ServiceProviderComment
        {
            get { return _ServiceProviderComment; }
            set { _ServiceProviderComment = value; }
        }
        public Int32 Noofpeople
        {
            get { return _Noofpeople; }
            set { _Noofpeople = value; }
        }
        public DataTable UdttSlotDetails
        {
            get { return _UdttSlotDetails; }
            set { _UdttSlotDetails = value; }
        }
        public DataTable UdttQuestionsAnswer
        {
            get { return _UdttQuestionsAnswer; }
            set { _UdttQuestionsAnswer = value; }
        }
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }

        }
        public DataTable UdttMenuOrder
        {
            get { return _UdttMenuOrder; }
            set { _UdttMenuOrder = value; }
        }

        public  DataTable UdttSplReqTable
        {
            get { return _UdttSplReqTable; }
            set { _UdttSplReqTable = value; }
        }
    }

    public static class ConfimBookingTables
    {
        private static DataTable _bookingtables = null;
        private static DataTable _SerarchdateSeekerId = null;
        private static DataTable _OldbookingTableNoTime = null;
        private static DataTable _NewbookingTableNoTime = null;
       

        public static DataTable bookingtables
        {
            get { return _bookingtables; }
            set { _bookingtables = value; }

        }
        public static DataTable SerarchdateSeekerId
        {
            get { return _SerarchdateSeekerId; }
            set { _SerarchdateSeekerId = value; }

        }
        public static DataTable OldbookingTableNoTime
        {
            get { return _OldbookingTableNoTime; }
            set { _OldbookingTableNoTime = value; }

        }

        public static DataTable NewbookingTableNoTime
        {
            get { return _NewbookingTableNoTime; }
            set { _NewbookingTableNoTime = value; }

        }

       

    }
}
