using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace WebServices.SOAP
{
    public static class Requests
    {
        public static HttpWebRequest CreateSoapRequestForMultiplying()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.dneonline.com/calculator.asmx");
            request.Headers.Add("SOAPAction:http://tempuri.org/Multiply");
            request.ContentType = "text/xml; charset=utf-8";
            request.Method = "POST";
            return request;
        }

        public static string RequestForMultiplyingNumbers(int x1, int x2)
        {
            var request = CreateSoapRequestForMultiplying();
            XmlDocument body = new XmlDocument();
            var bodyXml = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" +
                @"<soap:Body>" +
                @"<Multiply xmlns=""http://tempuri.org/"">" +
                $@"<intA>{x1}</intA>" +
                $@"<intB>{x2}</intB>" +
                @"</Multiply>" +
                @"</soap:Body>" +
                @"</soap:Envelope>";
            body.LoadXml(bodyXml);

            using(Stream stream = request.GetRequestStream())
            {
                body.Save(stream);
            }

            using(WebResponse response = request.GetResponse())
            {
                using(StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var serviceResult = reader.ReadToEnd();
                    var document = XDocument.Parse(serviceResult);
                    XNamespace nspace = "http://tempuri.org/";
                    var result = document.Root.Descendants(nspace + "MultiplyResponse").Elements(nspace + "MultiplyResult").FirstOrDefault();
                    return result.Value;
                }
            }
        }

        public static HttpWebRequest CreateSoapRequestForTemperatureConversion()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.w3schools.com/xml/tempconvert.asmx");
            request.ContentType = "application/soap+xml; charset=utf-8";
            request.Method = "POST";
            return request;
        }

        public static string RequestForFarenheitByCelsius(double celsius)
        {
            var request = CreateSoapRequestForTemperatureConversion();
            XmlDocument body = new XmlDocument();
            var bodyXml = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                @"<soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">" +
                @"<soap12:Body>" +
                @"<CelsiusToFahrenheit xmlns=""https://www.w3schools.com/xml/"">" +
                $@"<Celsius>{celsius}</Celsius>" +
                @"</CelsiusToFahrenheit>" +
                @"</soap12:Body>" +
                @"</soap12:Envelope>";
            body.LoadXml(bodyXml);

            using (Stream stream = request.GetRequestStream())
            {
                body.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var serviceResult = reader.ReadToEnd();
                    var document = XDocument.Parse(serviceResult);
                    XNamespace nspace = "https://www.w3schools.com/xml/";
                    var result = document.Root.Descendants(nspace + "CelsiusToFahrenheitResponse").Elements(nspace + "CelsiusToFahrenheitResult").FirstOrDefault();
                    return result.Value;
                }
            }
        }
    }
}