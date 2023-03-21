using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ZeitPlan.Utils
{
    public class NotificationHelper
    {
        public static void SendMessage(string notification)
        {
            string serverKey = "AAAA_zQCin4:APA91bHC7Hmpjtr0R5WiSiWFWQ74WtDyQzonly1bq5J35bkMO9vHzQLRsNB6eJOdEg0E8R13qOL06V_L0Ayyuxk65OIAuAF-ar2CzN7vJ-Ixj3ymKtuPCBuMViTCKAZlaGaiQGc0vOY2";
            var notificationInputDto = new
            {
                to = "cUnvjmwbTY-paV9imz718E:APA91bH8tsGoBzZcvRfmIkzNkjA9epmnSh-9nrmfu1x9oWVCSd8MKZuI3E8V3_qLKxg4QOMBtxfVZrmird_HOwUf62wbcutfa0c4iposdcPWrqrEQIWNUcaDtZPT83rj7MGiLzScKRko",
                notification = new
                {
                    body = notification,
                    title = "Zeit Plan",
                    icon = "",
                    type = "Test Notification"
                },
                data = new
                {
                    key1 = "value1",
                    key2 = "value2"
                }
            };
            try
            {
                var result = "";
                var webAddr = "https://fcm.googleapis.com/fcm/send";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(JsonConvert.SerializeObject(notificationInputDto));
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();

                }


            }
            catch (Exception ex)
            {
                throw;
                
               
            }
        }

    }
}
