using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace GS.SISGEGS.Web.Helpers
{
    public class GSbase
    {
        public static string host = ConfigurationManager.AppSettings["host"].ToString();

        public static string POSTResult(string method, int action, List<object> parametros = null)
        {
            string args = "";
            foreach (var p in parametros)
                args = args + "\"" + p + "\",";
            if (args.Length > 0)
                args = args.Substring(0, args.Length - 1);
            string postData =
            "{" +
                "\"action\": " + action + "," +
                "\"parametros\": [" + args + "]" +
            "}";

            WebRequest request = WebRequest.Create(host + "/wsMaster.svc/" + method);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

        private static string GetResult(string url)
        {
            WebRequest request = WebRequest.Create("http://" + url);
            request.Method = "GET";
            request.ContentType = "application/json";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}