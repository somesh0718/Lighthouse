using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Igmite.Lighthouse.BAL.Providers
{
    public interface ISmsServiceProvider
    {
        bool SendSMSToMobile(string destination, string message, MessageTemplate messageTemplate);
    }

    public class SmsServiceProvider
    {
        public string BaseSMSUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SMSSource { get; set; }

        public SmsServiceProvider()
        {
            //this.BaseSMSUrl = Convert.ToString(ConfigurationManager.AppSettings["SMSBaseUrl"]);
            //this.SMSSource = Convert.ToString(ConfigurationManager.AppSettings["SMSSourceKey"]);
            //this.UserName = Convert.ToString(ConfigurationManager.AppSettings["SMSUserName"]);
            //this.Password = Convert.ToString(ConfigurationManager.AppSettings["SMSPassword"]);
        }

        private string GetSMSServiceUrl()
        {
            //http://103.16.101.52:8080/bulksms/bulksms?username=ints-wemdco&password=1471478p&type=0&dlr=1&source=WEMDCO&destination=919322826712&message=Test

            return string.Format("http://{0}/bulksms/bulksms?username={1}&password={2}&type=0&dlr=1&source={3}&destination={{0}}&message={{1}}", this.BaseSMSUrl, this.UserName, this.Password, this.SMSSource);
        }

        public bool SendSMSToMobile(string destination, string message)
        {
            try
            {
                string smsBaseUrl = GetSMSServiceUrl();
                string smsServiceUrl = string.Format(smsBaseUrl, destination, message);

                HttpWebRequest smsRequest = (HttpWebRequest)WebRequest.Create(smsServiceUrl);

                smsRequest.AllowAutoRedirect = false;

                smsRequest.Method = "POST";
                smsRequest.ContentType = "application/x-www-form-urlencoded";

                smsRequest.ContentLength = message.Length;
                StreamWriter writer = new StreamWriter(smsRequest.GetRequestStream(), Encoding.ASCII);
                writer.Write(message);
                writer.Close();

                HttpWebResponse smsResponse = (HttpWebResponse)smsRequest.GetResponse();

                smsResponse.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        public bool SendSMSFromMSG91<T>(string destination, T sms, MessageTemplate messageTemplate)
        {
            try
            {
                //Prepare you post parameters
                StringBuilder sbPostData = new StringBuilder();
                List<string> messageVariables = new List<string>();

                sbPostData.Append("{");
                sbPostData.AppendFormat("\"sender\":\"{0}\",", "LAHIAW");
                sbPostData.AppendFormat("\"short_url\":\"{0}\",", "0");
                sbPostData.AppendFormat("\"mobiles\":\"91{0}\",", destination);

                if (sms.GetType() == typeof(OTPRequest))
                {
                    OTPRequest otpRequest = sms as OTPRequest;
                    sbPostData.AppendFormat("\"flow_id\":\"{0}\",", "62ba78563c72720b6d2f83e2");
                    sbPostData.AppendFormat("\"OTP\":\"{0}\",", otpRequest.OTPNumber);
                    sbPostData.AppendFormat("\"otptime\":\"{0}\"", otpRequest.OTPDateTime);
                }
                else if (sms.GetType() == typeof(VTRequest))
                {
                    VTRequest vtRequest = sms as VTRequest;
                    sbPostData.AppendFormat("\"flow_id\":\"{0}\",", messageTemplate.TemplateFlowId);

                    foreach (string fieldName in messageTemplate.MessageFields.Split(','))
                    {
                        if (string.Equals(fieldName, "VTN"))
                            messageVariables.Add(string.Format("\"VTNAME\":\"{0}\"", vtRequest.VTName));
                        else if (string.Equals(fieldName, "EML"))
                            messageVariables.Add(string.Format("\"VTEMAILID\":\"{0}\"", vtRequest.VTEmailId));
                        else if (string.Equals(fieldName, "RD"))
                            messageVariables.Add(string.Format("\"REPORTINGDATE\":\"{0}\"", vtRequest.ReportingDate));
                    }
                }
                else if (sms.GetType() == typeof(GLRequest))
                {
                    GLRequest glRequest = sms as GLRequest;
                    sbPostData.AppendFormat("\"flow_id\":\"{0}\",", messageTemplate.TemplateFlowId);

                    foreach (string fieldName in messageTemplate.MessageFields.Split(','))
                    {
                        if (string.Equals(fieldName, "VTN"))
                            messageVariables.Add(string.Format("\"VTNAME\":\"{0}\"", glRequest.VTName));
                        else if (string.Equals(fieldName, "EML"))
                            messageVariables.Add(string.Format("\"VTEMAILID\":\"{0}\"", glRequest.VTEmailId));
                        else if (string.Equals(fieldName, "RD"))
                            messageVariables.Add(string.Format("\"REPORTINGDATE\":\"{0}\"", glRequest.ReportingDate));
                    }
                }
                else if (sms.GetType() == typeof(FVRequest))
                {
                    FVRequest fvRequest = sms as FVRequest;
                    sbPostData.AppendFormat("\"flow_id\":\"{0}\",", messageTemplate.TemplateFlowId);

                    foreach (string fieldName in messageTemplate.MessageFields.Split(','))
                    {
                        if (string.Equals(fieldName, "VTN"))
                            messageVariables.Add(string.Format("\"VTNAME\":\"{0}\"", fvRequest.VTName));
                        else if (string.Equals(fieldName, "EML"))
                            messageVariables.Add(string.Format("\"VTEMAILID\":\"{0}\"", fvRequest.VTEmailId));
                        else if (string.Equals(fieldName, "RD"))
                            messageVariables.Add(string.Format("\"REPORTINGDATE\":\"{0}\"", fvRequest.ReportingDate));
                    }
                }

                sbPostData.Append(string.Join(",", messageVariables));
                sbPostData.Append("}");

                //Call Send SMS API
                string sendSMSUri = "https://api.msg91.com/api/v5/flow";

                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                httpWReq.Headers.Add("authkey", "365809A3OKYDXHba611a0e16P1");
                httpWReq.Headers.Add("content-type", "application/json");

                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());

                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse smsResponse = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(smsResponse.GetResponseStream());

                string responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                smsResponse.Close();
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }
    }
}