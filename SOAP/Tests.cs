using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace WebServices.SOAP
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void CheckMultiplyOperation()
        {
            int x1 = 2;
            int x2 = 6;
            var result = Requests.RequestForMultiplyingNumbers(x1, x2);
            Assert.AreEqual((x1 * x2).ToString(), result);
        }

        [TestMethod]
        public void RequestForFarenheitByCelsius()
        {
            double celsius = 5.5;
            double expectedFarenheit = 41.9;
            var actualFarenheit = Requests.RequestForFarenheitByCelsius(celsius);
            Assert.AreEqual(expectedFarenheit.ToString(), actualFarenheit.Replace('.', ','));
        }
    }
}