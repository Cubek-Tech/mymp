using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;

namespace BussinessEntity
{
    public class Prop_ServiceSeeker
    {
        private DataSet _dsReco;
        public DataSet dsReco
        {
            get
            {
                return _dsReco;
            }
            set
            {
                _dsReco = value;
            }
        }

        private int _effectedRows;
        public int EffectedRows
        {
            get
            {
                return _effectedRows;
            }
            set
            {
                _effectedRows = value;
            }
        }

        private string _fname;
        public string Fname
        {
            get
            {
                return _fname;
            }
            set
            {
                _fname = value;
            }
        }

        private string _mname;
        public string Mname
        {
            get
            {
                return _mname;
            }
            set
            {
                _mname = value;
            }
        }
        private string _lname;
        public string Lname
        {
            get
            {
                return _lname;
            }
            set
            {
                _lname = value;
            }
        }

        private string _emailID;
        public string EmailID
        {
            get
            {
                return _emailID;
            }
            set
            {
                _emailID = value;
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        private Int16 _secret_question_sk;
        public Int16 Secret_question_sk
        {
            get
            {
                return _secret_question_sk;
            }
            set
            {
                _secret_question_sk = value;
            }
        }

        private string _secret_question_answer;
        public string Secret_question_answer
        {
            get
            {
                return _secret_question_answer;
            }
            set
            {
                _secret_question_answer = value;
            }
        }



        private string _mobile_no;
        public string Mobile_no
        {
            get
            {
                return _mobile_no;
            }
            set
            {
                _mobile_no = value;
            }
        }


        private string _country_telecom_code;
        public string Country_telecom_code
        {
            get
            {
                return _country_telecom_code;
            }
            set
            {
                _country_telecom_code = value;
            }
        }



        private Int32 _city_sk;
        public Int32 City_sk
        {
            get
            {
                return _city_sk;
            }
            set
            {
                _city_sk = value;
            }
        }


        private Int32 _state_sk;
        public Int32 State_sk
        {
            get
            {
                return _state_sk;
            }
            set
            {
                _state_sk = value;
            }
        }


        private Int32 _country_sk;
        public Int32 Country_sk
        {
            get
            {
                return _country_sk;
            }
            set
            {
                _country_sk = value;
            }
        }


        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        private string _othercity;
        public string Othercity
        {
            get
            {
                return _othercity;
            }
            set
            {
                _othercity = value;
            }
        }

        private string _otherstate;
        public string Otherstate
        {
            get
            {
                return _otherstate;
            }
            set
            {
                _otherstate = value;
            }
        }


        private string _postalcode;
        public string Postalcode
        {
            get
            {
                return _postalcode;
            }
            set
            {
                _postalcode = value;
            }
        }

        private char _is_sms_notification;
        public char Is_sms_notification
        {
            get
            {
                return _is_sms_notification;
            }
            set
            {
                _is_sms_notification = value;
            }
        }
    }
}
