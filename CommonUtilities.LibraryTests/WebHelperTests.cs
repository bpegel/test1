using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommonUtilities.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities.Library.Tests
{
    [TestClass()]
    public class WebHelperTests
    {
        [TestMethod()]
        public void GetWebDataTest()
        {
            string baseUrl = "https://www.google.com/";
            string reqUri = "intl/en/about/";
            WebResponseInfo resInfo = WebHelper.GetWebData(baseUrl, reqUri, "", "", WebTool.REST_SHARP);
            Assert.AreEqual(resInfo.HttpStatus, System.Net.HttpStatusCode.OK);
        }
    }
}