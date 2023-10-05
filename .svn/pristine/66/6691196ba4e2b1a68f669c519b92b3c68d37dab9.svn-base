using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
namespace UtilityProject
{
    /// <summary>
    /// Narendra
    /// 06/12/2011
    /// </summary>
    public class IpTrack
    {



        public string ReturnData(string IP)
        {
            System.Uri objUrl = new System.Uri(IP);
            System.Net.WebRequest objWebReq;
            System.Net.WebResponse objResp;
            System.IO.StreamReader sReader;
            string strReturn = string.Empty;

            //Try to connect to the server and retrieve data. 
            try
            {
                objWebReq = System.Net.WebRequest.Create(objUrl);
                objResp = objWebReq.GetResponse();

                //Get the data and store in a return string. 
                sReader = new System.IO.StreamReader(objResp.GetResponseStream());
                strReturn = sReader.ReadToEnd();

                //Close the objects. 
                sReader.Close();
                objResp.Close();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objWebReq = null;
            }

            return strReturn;

        }








        #region Methods
        public IpLocator GetData()
        {
            IpLocator _IpLocator = new IpLocator();
            try
            {


                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.find-ip-address.org/ip-location-lookup-module.php");

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse(); // Insert code that uses the response object. HttpWResp.Close();

                Stream receiveStream = myHttpWebResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, encode);
                Char[] read = new Char[256];
                // Reads 256 characters at a time.    
                int count = readStream.Read(read, 0, 256);
                string strHtml = "";
                while (count > 0)
                {
                    // Dumps the 256 characters on a string and displays the string to the console.
                    String str = new String(read, 0, count);
                    strHtml = strHtml + " " + str;
                    count = readStream.Read(read, 0, 256);
                }
                // Releases the resources of the response.
                myHttpWebResponse.Close();
                // Releases the resources of the Stream.
                readStream.Close();


                string[] strArray = RemoveHtml(strHtml);
                if (strArray.Count() > 1)
                {
                    _IpLocator.City= strArray[3];
                    _IpLocator.State = strArray[4];
                    _IpLocator.CountryName = strArray[6];

                }
              
            }
            catch
            {

            }
            return _IpLocator;
        }


    
        //Remove the html and other text from string
        public string[] RemoveHtml(string text)
        {
            string result = "";
            result = Regex.Replace(text, @"<(.|\n td tr table b )*?>", string.Empty);
            string[] resultArray = result.Split(';');
            string[] temp = resultArray;
            if (resultArray.Count() > 0)
            {
                for (int i = 0; i < resultArray.Count() - 1; i++)
                {
                    int sindex = resultArray[i].IndexOf(':') + 1;
                    int eindex = resultArray[i].IndexOf(')') - 1;
                    if (sindex >= 0 && eindex > 1)
                    {
                        eindex = eindex - sindex;
                        temp[i] = resultArray[i].Substring(sindex, eindex);
                    }
                }

            }
            return temp;

        }

        //FillDetail
        public IpLocator FillData(string[] resultArray)
        {
            IpLocator _IpLocator = new IpLocator();
            if (resultArray.Count() > 0)
            {
                _IpLocator.IP = resultArray[1];              
                _IpLocator.City = resultArray[3];
                _IpLocator.State = resultArray[4];
                _IpLocator.CountryName = resultArray[6];
                _IpLocator.Currency = resultArray[12].Substring(0, resultArray[12].Length-3);


            }
            return _IpLocator;
        }

        #endregion
    }

    #region Oldcode
    //"http://www.find-ip-address.org/ip-location-lookup-module.php";// for any web site
    // "http://www.iplocationtools.com/iplocationtools.js?key=7d716e636633766660653f727a7a";//for table2book
    //  string url = "http://www.iplocationtools.com/iplocationtools.js?key=7d716e636633666c7c6b6367703962667d"; //table2reserve
    //  string htmlString = GetUrl(url);
    //  string[] resultArray = RemoveHtml(htmlString);
    //_IpLocator=  FillData(resultArray);

    //-------------------------------------------
    //HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.iplocationtools.com/iplocationtools.js?key=7d716e636633666c7c6b6367703962667d");

    //HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse(); // Insert code that uses the response object. HttpWResp.Close();

    //Stream receiveStream = myHttpWebResponse.GetResponseStream();
    //Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
    //// Pipes the stream to a higher level stream reader with the required encoding format. 
    //StreamReader readStream = new StreamReader(receiveStream, encode);             
    //Char[] read = new Char[256];
    //// Reads 256 characters at a time.    
    //int count = readStream.Read(read, 0, 256);

    //while (count > 0)
    //{
    //    // Dumps the 256 characters on a string and displays the string to the console.
    //    String str = new String(read, 0, count);
    //    _IpLocator.IP = _IpLocator.IP + " " + str;
    //    count = readStream.Read(read, 0, 256);
    //}

    //// Releases the resources of the response.
    //myHttpWebResponse.Close();
    //// Releases the resources of the Stream.
    //readStream.Close();
    ////------------------------------


        ///// Retrieve a Url via WebClient
        //private string GetUrl(string url)
        //{
        //    string result = string.Empty;
        //    System.Net.WebClient Client = new WebClient();
        //    using (Stream strm = Client.OpenRead(url))
        //    {
        //        StreamReader sr = new StreamReader(strm);
        //        result = sr.ReadToEnd();
        //    }

        //    return result;
        //}

    #endregion
}

