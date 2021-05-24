using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;


namespace upbit_console
{
    public class WebSocketTest
    {
        static string ticker_url = "https://api.upbit.com/v1/ticker";

        static string CoinInquiry(string coinName)
        {
            StringBuilder datatparam = new StringBuilder();
            datatparam.Append("markets=" + coinName + "&type=json");

            WebRequest request = (WebRequest)WebRequest.Create(ticker_url + "?" + datatparam);
            request.Method = "GET";
            request.ContentType = "application/json";
            WebResponse response = request.GetResponse();
            Stream datastream = response.GetResponseStream();
            StreamReader reader = new StreamReader(datastream);
            Console.WriteLine(reader.GetType().ToString());
            string strresult = reader.ReadToEnd();
            reader.Close();
            datastream.Close();
            response.Close();
            return strresult;
        }

        static void Main(string[] args)
        {
            string strresult = CoinInquiry("KRW-ETH");
            StringBuilder strr = new StringBuilder();
            strr.Append("{\"count\":1,\"list\":" + strresult + "}");
            
            var obj = JObject.Parse(strr.ToString());
            var list = obj["list"];
            
            Console.WriteLine(obj);

            string stmp;
            var item = list[0];

            Console.WriteLine(item["market"]);
            
            stmp = item["timestamp"].ToString();
            DateTime tt = new DateTime(1970,1,1,9,0,0,0);
            double tmp = Convert.ToDouble(stmp);
            tt = tt.AddMilliseconds(tmp);
            Console.WriteLine(Convert.ToString(tt));

            


        }
    }
}