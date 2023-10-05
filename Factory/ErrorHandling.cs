using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Data;
using System.Web;
namespace Factory
{
    public class ErrorHandling
    {

        public string reqUrl { get; set; }
        public string filePath { get; set; }
        public string lastErrorTypeName { get; set; }
        public string lastErrorMessage { get; set; }
        public string lastErrorStackTrace { get; set; }
        public string methodname { get; set; }

        public void writefile()
        {
            string MyreqUrl = reqUrl;
            string MyfilePath = filePath;
            string Mymethodname = methodname;
            string MylastErrorTypeName = lastErrorTypeName;
            string MylastErrorMessage = lastErrorMessage;
            string MylastErrorStackTrace = lastErrorStackTrace;


            if (File.Exists(MyfilePath))
            {
                using (StreamWriter w = new StreamWriter(MyfilePath, true))
                {
                    w.WriteLine("-------------------START-------------" + DateTime.Now);

                    w.WriteLine("\nURL: " + MyreqUrl + Environment.NewLine + Environment.NewLine
                                          + "\nMethodName: " + methodname + Environment.NewLine + Environment.NewLine
                                          + "\nException Type: " + MylastErrorTypeName + Environment.NewLine + Environment.NewLine
                                          + "\nMessage: " + MylastErrorMessage + Environment.NewLine + Environment.NewLine
                                          + "\nStack Trace: " + MylastErrorStackTrace

                               );
                    w.WriteLine("-------------------------------------------------------------------------------------------------------------");
                    w.Flush();
                    w.Close();

                }

            }
            else
            {
                StreamWriter w = File.CreateText(MyfilePath);
                w.WriteLine("-------------------START-------------" + DateTime.Now);
                w.WriteLine("\nURL: " + MyreqUrl + Environment.NewLine + Environment.NewLine
                                         + "\nMethodName: " + methodname + Environment.NewLine + Environment.NewLine
                                         + "\nException Type: " + MylastErrorTypeName + Environment.NewLine + Environment.NewLine
                                         + "\nMessage: " + MylastErrorMessage + Environment.NewLine + Environment.NewLine
                                         + "\nStack Trace: " + MylastErrorStackTrace

                           );
                w.WriteLine("-------------------------------------------------------------------------------------------------------------");
                w.Flush();
                w.Close();
            }           


        }
    }
}
